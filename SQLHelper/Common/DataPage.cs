using System.Collections.Generic;

namespace SQLHelper.Common
{
    public class DataPage<T>
    {
        public List<T> Data = new List<T>();
        public int RecordCount = 0;
        public int PageSize = 20;
        public int PageIndex = 0;
        public int PageCount { get{ return (RecordCount ==0 || PageSize==0) ? 0 : (RecordCount + PageSize - 1) / PageSize;}}
    }
}
