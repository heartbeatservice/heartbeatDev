using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HBS.Entities.Utilities;

namespace HBS.Entities
{
    class MenuRole:BaseEntity
    {
        public int MenuId { get; set; }
        public int RoleId { get; set; }

        public MenuRole()
        { }

        public MenuRole(IDataReader dbReader)
            : this()
        {
            if (dbReader.HasColumn("MenuId") && dbReader["MenuId"] != DBNull.Value)
                MenuId = (int)dbReader["MenuId"];
            
            if (dbReader.HasColumn("RoleId") && dbReader["RoleId"] != DBNull.Value)
                RoleId = (int)dbReader["RoleId"];
            
            if (dbReader.HasColumn("CreatedBy") && dbReader["CreatedBy"] != DBNull.Value)
                base.CreatedBy = (int)dbReader["CreatedBy"];
            
            if (dbReader.HasColumn("UpdatedBy") && dbReader["UpdatedBy"] != DBNull.Value)
                base.UpdatedBy = (int)dbReader["UpdatedBy"];
            
            if (dbReader.HasColumn("DateCreated") && dbReader["DateCreated"] != DBNull.Value)
                base.DateCreated = (DateTime)dbReader["DateCreated"];
            
            if (dbReader.HasColumn("DateUpdated") && dbReader["DateUpdated"] != DBNull.Value)
                base.DateUpdated = (DateTime)dbReader["DateUpdated"];
        }
    }
}
