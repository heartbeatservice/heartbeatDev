using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBS.Entities
{
    public class BaseEntity
    {
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
    }
}
