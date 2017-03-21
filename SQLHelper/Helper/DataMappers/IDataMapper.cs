using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLHelper.Helper.DataMappers
{
    /// <summary>
    /// Ref : http://aapl.codeplex.com/ and http://rlacovara.blogspot.co.id/2010/01/agile-adonet-persistence-layer-part-1.html
    /// </summary>

    interface IDataMapper
    {
        // Main method that populates dto with data
        Object GetData(IDataReader reader);
        // Gets the num results returned. Needed for data paging.
        int GetRecordCount(IDataReader reader);
    }

}
