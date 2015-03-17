using System;
using System.Data;
using HBS.Entities.Utilities;

namespace HBS.Entities
{
    public class WorkflowHistory
    {
        public int WorkflowHistoryID { get; set; }
        public int WorkflowID { get; set; }
        public string Payload { get; set; }
        public DateTime DateCreated { get; set; }

        public WorkflowHistory()
        {

        }

        public WorkflowHistory(int workflowHistoryID, int workflowID, string payload, DateTime dateCreated)
            : this()
        {

        }

        public WorkflowHistory(IDataReader dbReader)
            : this()
        {
            if (dbReader["WorkflowHistoryID"] != DBNull.Value) WorkflowHistoryID = (int)dbReader["WorkflowHistoryID"];
            if (dbReader["WorkflowID"] != DBNull.Value) WorkflowID = (int)dbReader["WorkflowID"];
            if (dbReader["Payload"] != DBNull.Value) Payload = (string)dbReader["Payload"];
            if (dbReader["DateCreated"] != DBNull.Value) DateCreated = (DateTime)dbReader["DateCreated"];
        }
    }
}
