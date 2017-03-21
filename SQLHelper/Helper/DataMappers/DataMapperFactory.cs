using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLHelper.Helper.DataMappers
{
    /// <summary>
    /// Ref : http://aapl.codeplex.com/ and http://rlacovara.blogspot.co.id/2010/01/agile-adonet-persistence-layer-part-1.html
    /// </summary>
    class DataMapperFactory
    {
        public IDataMapper GetMapper(Type dtoType)
        {
            return new GenericMapper(dtoType);

            //switch (dtoType.Name)
            //{
            //    case "BlogPost":
            //        return new BlogPostMapper();
            //    default:
            //        return new GenericMapper(dtoType);
            //}       
        }

    }

}
