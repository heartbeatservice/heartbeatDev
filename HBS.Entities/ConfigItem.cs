using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HBS.Entities.Utilities;

namespace HBS.Entities
{
    public class ControlConfig
    {
        public int ConfigId { get; set; }
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string ControlKey { get; set; }
        public string ControlValue { get; set; }
        public float ControlOrder { get; set; }
        public List<ControlConfigItem> ControlConfigItemList { get; set; }
        public string OtherText { get; set; }

        public ControlConfig()
        {
            ControlConfigItemList = new List<ControlConfigItem>();
        }

        public ControlConfig(IDataReader dbReader)
            : base()
        {
            if (dbReader.HasColumn("ConfigId") && dbReader["ConfigId"] != DBNull.Value)
                ConfigId = (int)dbReader["ConfigId"];

            if (dbReader.HasColumn("CompanyId") && dbReader["CompanyId"] != DBNull.Value)
                CompanyId = (int)dbReader["CompanyId"];

            if (dbReader.HasColumn("CompanyName") && dbReader["CompanyName"] != DBNull.Value)
                CompanyName = (string)dbReader["CompanyName"];

            if (dbReader.HasColumn("ControlKey") && dbReader["ControlKey"] != DBNull.Value)
                ControlKey = (string)dbReader["ControlKey"];

            if (dbReader.HasColumn("ControlValue") && dbReader["ControlValue"] != DBNull.Value)
                ControlValue = (string)dbReader["ControlValue"];

            if (dbReader.HasColumn("ControlOrder") && dbReader["ControlOrder"] != DBNull.Value)
                ControlOrder = (float)dbReader["ControlOrder"];

        }
    }
}
