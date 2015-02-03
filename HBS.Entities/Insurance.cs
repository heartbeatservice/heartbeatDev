using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HBS.Entities.Utilities;

namespace HBS.Entities
{
    public class Insurance : BaseEntity
    {

        public int InsuranceId { get; set; }
        public int CompanyId { get; set; }
        public string InsuranceName { get; set; }
        public string InsuranceAddress { get; set; }
        public string InsurancePhone { get; set; }
        public string InsuranceWebsite { get; set; }
       


        public Insurance()
        { }


        public Insurance(IDataReader dbReader)
            : this()
        {
            if (dbReader.HasColumn("InsuranceId") && dbReader["InsuranceId"] != DBNull.Value)
                InsuranceId = (int)dbReader["InsuranceId"];
            
            if (dbReader.HasColumn("CompanyId") && dbReader["CompanyId"] != DBNull.Value)
                CompanyId = (int)dbReader["CompanyId"];
            
            if (dbReader.HasColumn("InsuranceAddress") && dbReader["InsuranceAddress"] != DBNull.Value)
                InsuranceAddress = (string)dbReader["InsuranceAddress"];

            if (dbReader.HasColumn("InsuranceWebsite") && dbReader["InsuranceWebsite"] != DBNull.Value)
                InsuranceWebsite = (string)dbReader["InsuranceWebsite"];
            
            if (dbReader.HasColumn("InsurancePhone") && dbReader["InsurancePhone"] != DBNull.Value)
                InsurancePhone = (string)dbReader["InsurancePhone"];

            if (dbReader.HasColumn("InsuranceName") && dbReader["InsuranceName"] != DBNull.Value)
                InsuranceName = (string)dbReader["InsuranceName"];
            
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


