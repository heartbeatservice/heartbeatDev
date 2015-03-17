using System;
using System.Data;
using HBS.Entities.Utilities;

namespace HBS.Entities
{
    public class KendoDDL
    {
        public int value { get; set; }
        public string text { get; set; }

        public KendoDDL()
        {

        }

        public KendoDDL(int userId, string userName)
            : this()
        {

        }

        public KendoDDL(IDataReader dbReader, string type)
            : this()
        {
            if (type == "User")
            {
                if (dbReader["UserId"] != DBNull.Value) value = (int)dbReader["UserId"];
                if (dbReader["UserName"] != DBNull.Value) text = (string)dbReader["UserName"];
            }

            if (type == "Status")
            {
                if (dbReader["WorkflowStatusID"] != DBNull.Value) value = (int)dbReader["WorkflowStatusID"];
                if (dbReader["StatusName"] != DBNull.Value) text = (string)dbReader["StatusName"];
            }
        }
    }
}
