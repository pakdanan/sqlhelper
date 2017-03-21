using SQLHelper.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLHelper.Entities
{
    public class BaseEntity
    {
        public int ID { get; set; } = 0;
        public String Code { get; set; } = "";
        public String Name { get; set; } = "";
        public String Description { get; set; } = "";

        public Guid CreatedById { get; set; } = default(Guid);

        public string CreatedByName { get; set; } = "";

        public DateTime CreatedOn { get; set; } = DateTime.Now;

        public Guid LastModifiedById { get; set; } = default(Guid);

        public string LastModifiedByName { get; set; } = "";

        public DateTime LastModifiedOn { get; set; } = DateTime.Now;

        public int Status { get; set; } = 1;



    }
}
