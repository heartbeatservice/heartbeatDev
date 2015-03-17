using System;
using System.Data;
using HBS.Entities.Utilities;

namespace HBS.Entities
{
    public class WorkflowNotification
    {
        public int WorkflowNotificationID { get; set; }
        public int WorkflowID { get; set; }
        public string Payload { get; set; }
        public string NotificationType { get; set; }
        public DateTime DateCreated { get; set; }

        public WorkflowNotification()
        {

        }

        public WorkflowNotification(int WorkflowNotificationID, int workflowID, string payload, string notificationType, DateTime dateCreated)
            : this()
        {

        }

        public WorkflowNotification(IDataReader dbReader)
            : this()
        {
            if (dbReader["WorkflowNotificationID"] != DBNull.Value) WorkflowNotificationID = (int)dbReader["WorkflowNotificationID"];
            if (dbReader["WorkflowID"] != DBNull.Value) WorkflowID = (int)dbReader["WorkflowID"];
            if (dbReader["Payload"] != DBNull.Value) Payload = (string)dbReader["Payload"];
            if (dbReader["NotificationType"] != DBNull.Value) NotificationType = (string)dbReader["NotificationType"];
            if (dbReader["DateCreated"] != DBNull.Value) DateCreated = (DateTime)dbReader["DateCreated"];
        }
    }
}
