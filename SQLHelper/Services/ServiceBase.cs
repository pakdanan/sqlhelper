using System;
using System.Text;
using System.Collections.Generic;
using SQLHelper.Helper;

namespace SQLHelper.Services
{
    public abstract class ServiceBase
    {

        // SharedSqlDao
        private SqlDao _sharedSqlDao;
        public SqlDao SharedSqlDao
        { 
            get
            {
                if (_sharedSqlDao ==null){_sharedSqlDao = new SqlDao();}
                return _sharedSqlDao;
            }
            set 
            {
                _sharedSqlDao = value;
            }
        }


        public string GetSqlCsv(List<Guid> list)
        {
            StringBuilder sb = new StringBuilder();
            foreach (Guid value in list)
            {
                sb.AppendFormat("'{0}',", value);
            }
            string csv = sb.ToString();
            return csv.Remove(csv.Length - 1);
        }
    }
}
