using System;
using System.Data;
using System.Linq;
using System.Reflection;

namespace SQLHelper.Helper.DataMappers
{
    /// <summary>
    /// Modification from http://aapl.codeplex.com/ and http://rlacovara.blogspot.co.id/2010/01/agile-adonet-persistence-layer-part-1.html
    /// </summary>
    class GenericMapper : IDataMapper
    {
        public System.Type DtoType { get; set; }
        const string SQLSeparator = "_";

        public GenericMapper(System.Type type)
        {
            DtoType = type;
        }

        private Object SetObjectValue(Type type, IDataReader reader, string prefix = "")
        {
            object obj = Activator.CreateInstance(type);
            PropertyInfo[] props = type.GetProperties();
            foreach (PropertyInfo p in props)
            {
                /// ! Property yang langsung bisa diambil nilainya primitive (int,bool,dll), string,datetime, guid, byte[] :
                // Type.IsPrimitive: Boolean, Byte, SByte, Int16, UInt16, Int32, UInt32, Int64, UInt64, IntPtr, UIntPtr, Char, Double, and Single.
                if (p.PropertyType.IsPrimitive || p.PropertyType == typeof(string) || p.PropertyType == typeof(decimal) || p.PropertyType == typeof(DateTime)
                    || p.PropertyType == typeof(Guid) || p.PropertyType.IsArray)
                {
                    var fieldNames = Enumerable.Range(0, reader.FieldCount).Select(i => reader.GetName(i)).ToArray();
                    if (fieldNames.Contains(prefix + p.Name))
                    {
                        int colIndex = reader.GetOrdinal(prefix + p.Name);
                        if (!reader.IsDBNull(colIndex))
                        {
                            p.SetValue(obj, reader.GetValue(colIndex), null);
                        }
                    }
                }
                else
                {
                    Object oNested = SetObjectValue(p.PropertyType, reader,p.Name+SQLSeparator);
                    p.SetValue(obj, oNested, null);
                }
            }
            return obj;
        }

        public Object GetData(IDataReader reader)
        {
            object dto = SetObjectValue(DtoType, reader);
            return dto;
        }

        public int GetRecordCount(IDataReader reader)
        {
            Object count = reader["RecordCount"];
            return count == null ? 0 : Convert.ToInt32(count);
        }
    }

}
