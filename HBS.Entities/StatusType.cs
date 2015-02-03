using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HBS.Entities.Utilities;
using System.Data;

namespace HBS.Entities
{
    class StatusType  
    {
        public int StatusId { get; set; }
        public string StatusDesc { get; set; }
        public int CompanyId { get; set; }

        public StatusType()
        { }

        public StatusType(IDataReader dbReader)
            : this()
        {

            if (dbReader.HasColumn("StatusId") && dbReader["StatusId"] != DBNull.Value)
                StatusId = (int)dbReader["StatusId"];
            
            if (dbReader.HasColumn("StatusDesc") && dbReader["StatusDesc"] != DBNull.Value)
                StatusDesc = (string)dbReader["StatusDesc"];
            
            if (dbReader.HasColumn("CompanyId") && dbReader["CompanyId"] != DBNull.Value)
                CompanyId = (int)dbReader["CompanyId"];
        }

    }
}
