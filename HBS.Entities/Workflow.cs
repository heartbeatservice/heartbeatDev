using System;
using System.Data;
using HBS.Entities.Utilities;

namespace HBS.Entities
{
    public class Workflow
    {
        public int WorkflowID { get; set; }
        public int CategoryID { get; set; }
        public int OwnerID { get; set; }
        public int WorkerID { get; set; }
        public string WorkflowTitle { get; set; }
        public string WorkflowNote { get; set; }
        public DateTime DueDate { get; set; }
        public int StatusID { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }

        public Workflow()
        {

        }

        public Workflow(int workflowID, int categoryDI, int ownerID, int workerID, string workflowTitle, string workflownote, DateTime dueDate, int statusID, DateTime dateCreated, DateTime dateUpdated)
            : this()
        {

        }

        public Workflow(IDataReader dbReader)
            : this()
        {
            if (dbReader["WorkflowID"] != DBNull.Value) WorkflowID = (int)dbReader["WorkflowID"];
            if (dbReader["CategoryID"] != DBNull.Value) CategoryID = (int)dbReader["CategoryID"];
            if (dbReader["OwnerID"] != DBNull.Value) OwnerID = (int)dbReader["OwnerID"];
            if (dbReader["WorkerID"] != DBNull.Value) WorkerID = (int)dbReader["WorkerID"];
            if (dbReader["WorkflowTitle"] != DBNull.Value) WorkflowTitle = (string)dbReader["WorkflowTitle"];
            if (dbReader["WorkflowNote"] != DBNull.Value) WorkflowNote = (string)dbReader["WorkflowNote"];
            if (dbReader["DueDate"] != DBNull.Value) DueDate = (DateTime)dbReader["DueDate"];
            if (dbReader["StatusID"] != DBNull.Value) StatusID = (int)dbReader["StatusID"];
            if (dbReader["DateCreated"] != DBNull.Value) DateCreated = (DateTime)dbReader["DateCreated"];
            if (dbReader["DateUpdated"] != DBNull.Value) DateUpdated = (DateTime)dbReader["DateUpdated"];
        }
    }
}
