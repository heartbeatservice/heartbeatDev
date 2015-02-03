using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HBS.Entities.Utilities;
using System.Data;

namespace HBS.Entities
{
    class ConfigValues : BaseEntity
    {
        public string ControlItemKey { get; set; }
        public string ControlItemValue { get; set; }
        public string ControlItemText { get; set; }
        public float ControlItemOrder { get; set; }
        public string OtherText { get; set; }

        public ConfigValues()
        { }

        public ConfigValues(IDataReader dbReader)
            : this()
        {
            if (dbReader.HasColumn("ControlItemKey") && dbReader["ControlItemKey"] != DBNull.Value)
                ControlItemKey = (string)dbReader["ControlItemKey"];

            if (dbReader.HasColumn("ControlItemValue") && dbReader["ControlItemValue"] != DBNull.Value)
                ControlItemValue = (string)dbReader["ControlItemValue"];

            if (dbReader.HasColumn("ControlItemText") && dbReader["ControlItemText"] != DBNull.Value)
                ControlItemText = (string)dbReader["ControlItemText"];

            if (dbReader.HasColumn("ControlItemOrder") && dbReader["ControlItemOrder"] != DBNull.Value)
                ControlItemOrder = (float)dbReader["ControlItemOrder"];

            if (dbReader.HasColumn("OtherText") && dbReader["OtherText"] != DBNull.Value)
                OtherText = (string)dbReader["OtherText"];

        }
    }
}
