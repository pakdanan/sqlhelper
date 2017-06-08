using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using Melissa.Library.Common;
using System.Configuration;
using Melissa.Library.Helper.DataMappers;
using System.Data.SqlTypes;

/// <summary>
/// Original source : http://aapl.codeplex.com/ and http://rlacovara.blogspot.co.id/2010/01/agile-adonet-persistence-layer-part-1.html
/// Added by pakdanan: - transaction support, - add closing sqldatareader at 'finally'
/// </summary>

namespace Melissa.Library.Helper
{
    public class SqlDao
    {

        #region "Database Helper Methods"

        protected SqlTransaction _trans = null;
        public SqlTransaction Transaction { get { return _trans; } }

        // Connection
        private SqlConnection _sharedConnection;
        public SqlConnection SharedConnection
        {
            get
            {
                if (_sharedConnection == null)
                {
                    _sharedConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Subscription"].ConnectionString);
                }
                return _sharedConnection;
            }
            set
            {
                _sharedConnection = value;
            }
        }


        // Constructors
        public SqlDao() { }
        public SqlDao(SqlConnection connection)
        {
            this.SharedConnection = connection;
        }


        // GetDbSqlCommand
        public SqlCommand GetSqlCommand(string sqlQuery)
        {
            SqlCommand command = new SqlCommand();
            command.Connection = SharedConnection;
            // Associate with current transaction, if any
            if (_trans != null)
                command.Transaction = _trans;
            command.CommandType = CommandType.Text;
            command.CommandText = sqlQuery;
            return command;
        }


        // GetDbSprocCommand
        public SqlCommand GetSprocCommand(string sprocName)
        {
            SqlCommand command = new SqlCommand(sprocName);
            command.Connection = SharedConnection;
            // Associate with current transaction, if any
            if (_trans != null)
                command.Transaction = _trans;
            command.CommandType = CommandType.StoredProcedure;
            return command;
        }


        // CreateNullParameter
        public SqlParameter CreateNullParameter(string name, SqlDbType paramType)
        {
            SqlParameter parameter = new SqlParameter();
            parameter.SqlDbType = paramType;
            parameter.ParameterName = name;
            parameter.Value = null;
            parameter.Direction = ParameterDirection.Input;
            return parameter;
        }


        // CreateNullParameter - with size for nvarchars
        public SqlParameter CreateNullParameter(string name, SqlDbType paramType, int size)
        {
            SqlParameter parameter = new SqlParameter();
            parameter.SqlDbType = paramType;
            parameter.ParameterName = name;
            parameter.Size = size;
            parameter.Value = null;
            parameter.Direction = ParameterDirection.Input;
            return parameter;
        }


        // CreateOutputParameter
        public SqlParameter CreateOutputParameter(string name, SqlDbType paramType)
        {
            SqlParameter parameter = new SqlParameter();
            parameter.SqlDbType = paramType;
            parameter.ParameterName = name;
            parameter.Direction = ParameterDirection.Output;
            return parameter;
        }


        // CreateOuputParameter - with size for nvarchars/varchars
        public SqlParameter CreateOutputParameter(string name, SqlDbType paramType, int size)
        {
            SqlParameter parameter = new SqlParameter();
            parameter.SqlDbType = paramType;
            parameter.Size = size;
            parameter.ParameterName = name;
            parameter.Direction = ParameterDirection.Output;
            return parameter;
        }

        // CreateReturnParameter - with size for nvarchars
        public SqlParameter CreateReturnParameter(string name, SqlDbType paramType, int size)
        {
            SqlParameter parameter = new SqlParameter();
            parameter.SqlDbType = paramType;
            parameter.Size = size;
            parameter.ParameterName = name;
            parameter.Direction = ParameterDirection.ReturnValue;
            return parameter;
        }

        // CreateParameter - uniqueidentifier
        public SqlParameter CreateParameter(string name, Guid value)
        {
            if (value == null)
                value = default(Guid);
            SqlParameter parameter = new SqlParameter();
            parameter.SqlDbType = SqlDbType.UniqueIdentifier;
            parameter.ParameterName = name;
            parameter.Value = value;
            parameter.Direction = ParameterDirection.Input;
            return parameter;
        }


        // CreateParameter - int
        public SqlParameter CreateParameter(string name, int value)
        {
            SqlParameter parameter = new SqlParameter();
            parameter.SqlDbType = SqlDbType.Int;
            parameter.ParameterName = name;
            parameter.Value = value;
            parameter.Direction = ParameterDirection.Input;
            return parameter;
        }


        // CreateParameter - datetime
        public SqlParameter CreateParameter(string name, DateTime value)
        {
            if (value == null)
                value = (DateTime)SqlDateTime.MinValue;
            SqlParameter parameter = new SqlParameter();
            parameter.SqlDbType = SqlDbType.DateTime;
            parameter.ParameterName = name;
            parameter.Value = value;
            parameter.Direction = ParameterDirection.Input;
            return parameter;
        }

        // CreateParameter - Decimal
        public SqlParameter CreateParameter(string name, decimal value)
        {
            SqlParameter parameter = new SqlParameter();
            parameter.SqlDbType = SqlDbType.Decimal;
            parameter.ParameterName = name;
            parameter.Value = value;
            parameter.Direction = ParameterDirection.Input;
            return parameter;
        }

        // CreateParameter - Image
        public SqlParameter CreateParameter(string name, byte[] value)
        {
            if (value == null)
                value = new byte[] { };
            SqlParameter parameter = new SqlParameter();
            parameter.SqlDbType = SqlDbType.Image;
            parameter.ParameterName = name;
            parameter.Value = value;
            parameter.Direction = ParameterDirection.Input;
            return parameter;
        }


        // CreateParameter - varchar
        public SqlParameter CreateParameter(string name, string value, int size)
        {
            if (value == null)
                value = string.Empty;
            SqlParameter parameter = new SqlParameter(); //SqlDbType.NVarChar;
            parameter.SqlDbType = SqlDbType.VarChar;
            parameter.ParameterName = name;
            parameter.Size = size;
            parameter.Value = value;
            parameter.Direction = ParameterDirection.Input;
            return parameter;
        }

        // CreateParameter - Bit/Boolean
        public SqlParameter CreateParameter(string name, bool value)
        {
            SqlParameter parameter = new SqlParameter();
            parameter.SqlDbType = SqlDbType.Bit;
            parameter.ParameterName = name;
            parameter.Value = value;
            parameter.Direction = ParameterDirection.Input;
            return parameter;
        }

        #endregion



        #region "Data Projection Methods"


        // ExecuteNonQuery
        public void ExecuteNonQuery(SqlCommand command)
        {
            try
            {
                if (command.Connection.State != ConnectionState.Open)
                {
                    command.Connection.Open();
                }
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw new Exception("Error executing query", e);
            }
            finally
            {
                if (_trans == null)
                    command.Connection.Close();
            }
        }


        // ExecuteScalar
        public Object ExecuteScalar(SqlCommand command)
        {
            try
            {
                if (command.Connection.State != ConnectionState.Open)
                {
                    command.Connection.Open();
                }
                return command.ExecuteScalar();
            }
            catch (Exception e)
            {
                throw new Exception("Error executing query", e);
            }
            finally
            {
                if (_trans == null)
                    command.Connection.Close();
            }
        }


        // GetSingleValue
        public T GetSingleValue<T>(SqlCommand command)
        {
            SqlDataReader reader = null;
            T returnValue = default(T);
            try
            {
                if (command.Connection.State != ConnectionState.Open)
                {
                    command.Connection.Open();
                }
                reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    if (!reader.IsDBNull(0)) { returnValue = (T)reader[0]; }
                    reader.Close();
                }
            }
            catch (Exception e)
            {
                throw new Exception("Error populating data", e);
            }
            finally
            {
                if (reader != null)
                    reader.Close();
                if (_trans == null)
                    command.Connection.Close();
            }
            return returnValue;
        }


        // GetSingleString
        public Int32 GetSingleInt32(SqlCommand command)
        {
            SqlDataReader reader = null;
            Int32 returnValue = default(int);
            try
            {
                if (command.Connection.State != ConnectionState.Open)
                {
                    command.Connection.Open();
                }
                reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    if (!reader.IsDBNull(0)) { returnValue = reader.GetInt32(0); }
                    reader.Close();
                }
            }
            catch (Exception e)
            {
                throw new Exception("Error populating data", e);
            }
            finally
            {
                if (reader != null)
                    reader.Close();
                if (_trans == null)
                    command.Connection.Close();
            }
            return returnValue;
        }


        // GetSingleString
        public string GetSingleString(SqlCommand command)
        {
            SqlDataReader reader = null;
            string returnValue = null;
            try
            {
                if (command.Connection.State != ConnectionState.Open)
                {
                    command.Connection.Open();
                }
                reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    if (!reader.IsDBNull(0)) { returnValue = reader.GetString(0); }
                    reader.Close();
                }
            }
            catch (Exception e)
            {
                throw new Exception("Error populating data", e);
            }
            finally
            {
                if (reader != null)
                    reader.Close();
                if (_trans == null)
                    command.Connection.Close();
            }
            return returnValue;
        }


        // GetStringList
        public List<string> GetStringList(SqlCommand command)
        {
            List<string> returnList = null;
            SqlDataReader reader = null;
            try
            {
                if (command.Connection.State != ConnectionState.Open)
                {
                    command.Connection.Open();
                }
                reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    returnList = new List<string>();
                    while (reader.Read())
                    {
                        if (!reader.IsDBNull(0)) { returnList.Add(reader.GetString(0)); }
                    }
                    reader.Close();
                }
            }
            catch (Exception e)
            {
                throw new Exception("Error populating data", e);
            }
            finally
            {
                if (reader != null)
                    reader.Close();
                if (_trans == null)
                    command.Connection.Close();
            }
            return returnList;
        }


        // GetSingle
        public T GetSingle<T>(SqlCommand command) where T : class
        {
            T dto = null;
            SqlDataReader reader = null;
            try
            {
                if (command.Connection.State != ConnectionState.Open)
                {
                    command.Connection.Open();
                }
                reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    IDataMapper mapper = new DataMapperFactory().GetMapper(typeof(T));
                    dto = (T)mapper.GetData(reader);
                    reader.Close();
                }
            }
            catch (Exception e)
            {
                throw new Exception("Error populating data", e);
            }
            finally
            {
                if (reader != null)
                    reader.Close();
                if (_trans == null)
                    command.Connection.Close();
            }
            // return the DTO, it's either populated with data or null.
            return dto;
        }


        // GetList
        public List<T> GetList<T>(SqlCommand command) where T : class
        {
            List<T> dtoList = new List<T>();
            SqlDataReader reader = null;
            try
            {
                if (command.Connection.State != ConnectionState.Open)
                {
                    command.Connection.Open();
                }
                reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    IDataMapper mapper = new DataMapperFactory().GetMapper(typeof(T));
                    while (reader.Read())
                    {
                        T dto = null;
                        dto = (T)mapper.GetData(reader);
                        dtoList.Add(dto);
                    }
                    reader.Close();
                }
            }
            catch (Exception e)
            {
                throw new Exception("Error populating data", e);
            }
            finally
            {
                if (reader != null)
                    reader.Close();
                if (_trans == null)
                    command.Connection.Close();
            }
            // We return either the populated list if there was data,
            // or if there was no data we return an empty list.
            return dtoList;
        }




        // GetDataPage
        public DataPage<T> GetDataPage<T>(SqlCommand command, int pageIndex, int pageSize) where T : class
        {
            SqlDataReader reader = null;
            DataPage<T> page = new DataPage<T>();
            page.PageIndex = pageIndex;
            page.PageSize = pageSize;
            try
            {
                if (command.Connection.State != ConnectionState.Open)
                {
                    command.Connection.Open();
                }
                reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    IDataMapper mapper = new DataMapperFactory().GetMapper(typeof(T));
                    while (reader.Read())
                    {
                        // get the data for this row
                        T dto = null;
                        dto = (T)mapper.GetData(reader);
                        page.Data.Add(dto);
                        // If we haven't set the RecordCount yet then set it
                        if (page.RecordCount == 0) { page.RecordCount = mapper.GetRecordCount(reader); }
                    }
                    reader.Close();
                }
            }
            catch (Exception e)
            {
                throw new Exception("Error populating data", e);
            }
            finally
            {
                if (reader != null)
                    reader.Close();
                if (_trans == null)
                    command.Connection.Close();
            }
            return page;
        }


        #endregion


        #region Transaction Members

        /// <summary>
        /// Begins a transaction
        /// </summary>
        /// <returns>The new SqlTransaction object</returns>
        public SqlTransaction BeginTransaction()
        {
            try
            {
                if (SharedConnection.State != ConnectionState.Open)
                {
                    SharedConnection.Open();
                }
                Rollback();
                _trans = SharedConnection.BeginTransaction();
            }
            catch (Exception e)
            {
                throw new Exception("Error begin Transaction", e);
            }

            return _trans;
        }

        /// <summary>
        /// Commits any transaction in effect.
        /// </summary>
        public void Commit()
        {
            if (_trans != null)
            {
                try
                {
                    _trans.Commit();

                }
                catch (Exception e)
                {
                    Rollback();
                    throw new Exception("Error Commit Transaction", e);
                }
                finally
                {
                    if (SharedConnection.State == ConnectionState.Open)
                        SharedConnection.Close();
                }
            }
        }

        /// <summary>
        /// Rolls back any transaction in effect.
        /// </summary>
        public void Rollback()
        {
            if (_trans != null)
            {
                try

                {
                    _trans.Rollback();
                }
                catch
                {
                    // This catch block will handle any errors that may have occurred
                    // on the server that would cause the rollback to fail, such as
                    // a closed connection.
                }
            }
        }

        #endregion


    }
}
