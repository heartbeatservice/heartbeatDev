using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HBS.Entities.Utilities;
using System.Data;

namespace HBS.Entities
{
    public class Company : BaseEntity
    {
        public int ComapanyId { get; set; }
        public string CompanyName { get; set; }
        public string Description { get; set; }

        public Company()
        {

        }

        public Company(IDataReader dbReader)
            : this()
        {
            if (dbReader.HasColumn("ComapanyId") && dbReader["ComapanyId"] != DBNull.Value)
                ComapanyId = (int)dbReader["ComapanyId"];
            
            if (dbReader.HasColumn("CompanyName") && dbReader["CompanyName"] != DBNull.Value)
                CompanyName = (string)dbReader["CompanyName"];
            
            if (dbReader.HasColumn("Description") && dbReader["Description"] != DBNull.Value)
                Description = (string)dbReader["Description"];
            
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

