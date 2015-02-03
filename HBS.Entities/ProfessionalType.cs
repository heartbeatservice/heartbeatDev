using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HBS.Entities.Utilities;

namespace HBS.Entities
{
    class ProfessionalType:BaseEntity
    {
        public int ProfessionalTypeId { get; set; }
        public string ProfessionalTypeDesc { get; set; }
        public int CompanyId { get; set; }

        public ProfessionalType()
        { }

        public ProfessionalType(IDataReader dbReader)
            : this()
        {
            if (dbReader.HasColumn("ProfessionalTypeId") && dbReader["ProfessionalTypeId"] != DBNull.Value)
                ProfessionalTypeId = (int)dbReader["ProfessionalTypeId"];
            
            if (dbReader.HasColumn("ProfessionalTypeDesc") && dbReader["ProfessionalTypeDesc"] != DBNull.Value)
                ProfessionalTypeDesc = (string)dbReader["ProfessionalTypeDesc"];
            
            if (dbReader.HasColumn("CompanyId") && dbReader["CompanyId"] != DBNull.Value)
                CompanyId = (int)dbReader["CompanyId"];
            
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
