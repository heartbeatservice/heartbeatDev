using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HBS.Entities.Utilities;
using System.Data;


namespace HBS.Entities
{
    public class Role:BaseEntity
    {
        public int RoleId { get; set; }
        public int CompanyId { get; set; }
        public string RoleName { get; set; }

        public Role()
        {}

        public Role(IDataReader dbReader)
            : this()
        {
            if (dbReader.HasColumn("RoleId") && dbReader["RoleId"] != DBNull.Value)
                RoleId = (int)dbReader["RoleId"];
            
            if (dbReader.HasColumn("CompanyId") && dbReader["CompanyId"] != DBNull.Value)
                CompanyId = (int)dbReader["ProfessionalId"];
            
            if (dbReader.HasColumn("RoleName") && dbReader["RoleName"] != DBNull.Value)
                RoleName = (string)dbReader["RoleName"];

        }

    }
}
