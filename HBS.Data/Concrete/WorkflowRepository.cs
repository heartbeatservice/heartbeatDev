using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HBS.Data.Abstract;
using HBS.Entities;
using System.Data.SqlClient;
using System.Data;
using System.Web.Script.Serialization;

namespace HBS.Data.Concrete
{
    public class WorkflowRepository : BaseRepository, IWorkflowRepository
    {
        public WorkflowRepository()
        {
        }

        public IQueryable<Workflow> GetAllWorkflow()
        {
            IList<Workflow> workflowList = new List<Workflow>();

            using (var connection = new SqlConnection(PrescienceRxConnectionString))
            {
                connection.Open();

                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = @"SELECT WorkflowID, CategoryID, OwnerID, WorkerID, WorkflowTitle, WorkflowNote, DueDate, StatusID, DateCreated, DateUpdated FROM dbo.Workflow";

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            workflowList.Add(new Workflow(reader));
                        }
                    }
                }

                connection.Dispose();
            }

            return workflowList.AsQueryable();
        }

        public bool AddWorkflow(Workflow toInsert)
        {
            int rowAffected = 0;
            bool notification = false;
            bool history = false;

            if (toInsert.CategoryID == 0) toInsert.CategoryID = 1;
            if (toInsert.OwnerID == 0) toInsert.OwnerID = 1;
            if (toInsert.WorkerID == 0) toInsert.WorkerID = 1;
            if (toInsert.StatusID == 0) toInsert.StatusID = 1;

            using (var connection = new SqlConnection(PrescienceRxConnectionString))
            {
                connection.Open();

                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = @"INSERT INTO dbo.Workflow VALUES(@CategoryID, @OwnerID, @WorkerID, @WorkflowTitle, @WorkflowNote, @DueDate, @StatusID, @DateCreated, @DateUpdated); SELECT CAST(SCOPE_IDENTITY() AS INT);";

                    command.Parameters.AddWithValue("@CategoryID", toInsert.CategoryID);
                    command.Parameters.AddWithValue("@OwnerID", toInsert.OwnerID);
                    command.Parameters.AddWithValue("@WorkerID", toInsert.WorkerID);
                    command.Parameters.AddWithValue("@WorkflowTitle", toInsert.WorkflowTitle);
                    command.Parameters.AddWithValue("@WorkflowNote", toInsert.WorkflowNote);
                    command.Parameters.AddWithValue("@DueDate", toInsert.DueDate);
                    command.Parameters.AddWithValue("@StatusID", toInsert.StatusID);
                    command.Parameters.AddWithValue("@DateCreated", toInsert.DateCreated);
                    command.Parameters.AddWithValue("@DateUpdated", toInsert.DateUpdated);

                    rowAffected = (int)command.ExecuteScalar();
                }

                connection.Dispose();
            }

            // add workflow history and notification for the newly added workflow
            if (rowAffected >= 1)
            {
                // set the workflow ID for the workflow object in memeory to the newly created ID
                toInsert.WorkflowID = rowAffected;

                // add workflow notification and history
                notification = AddWorkflowNotification(null, toInsert);
                history = AddWorkflowHistory(toInsert);

                return true;
            }

            return false;
        }

        public bool EditWorkflow(Workflow toUpdate)
        {
            int rowAffected = 0;
            bool notification = false;
            bool history = false;

            if (toUpdate.CategoryID == 0) toUpdate.CategoryID = 1;
            if (toUpdate.OwnerID == 0) toUpdate.OwnerID = 1;
            if (toUpdate.WorkerID == 0) toUpdate.WorkerID = 1;
            if (toUpdate.StatusID == 0) toUpdate.StatusID = 1;

            // get workflow object before update
            Workflow beforeUpdate = GetWorkflowByID(toUpdate.WorkflowID);

            using (var connection = new SqlConnection(PrescienceRxConnectionString))
            {
                connection.Open();

                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = @"UPDATE dbo.Workflow SET CategoryID = @CategoryID, OwnerID = @OwnerID, WorkerID = @WorkerID, WorkflowTitle = @WorkflowTitle, WorkflowNote = @WorkflowNote, DueDate = @DueDate, StatusID = @StatusID, DateCreated = @DateCreated, DateUpdated = @DateUpdated WHERE WorkflowID = @WorkflowID";

                    command.Parameters.AddWithValue("@WorkflowID", toUpdate.WorkflowID);
                    command.Parameters.AddWithValue("@CategoryID", toUpdate.CategoryID);
                    command.Parameters.AddWithValue("@OwnerID", toUpdate.OwnerID);
                    command.Parameters.AddWithValue("@WorkerID", toUpdate.WorkerID);
                    command.Parameters.AddWithValue("@WorkflowTitle", toUpdate.WorkflowTitle);
                    command.Parameters.AddWithValue("@WorkflowNote", toUpdate.WorkflowNote);
                    command.Parameters.AddWithValue("@DueDate", toUpdate.DueDate);
                    command.Parameters.AddWithValue("@StatusID", toUpdate.StatusID);
                    command.Parameters.AddWithValue("@DateCreated", toUpdate.DateCreated);
                    command.Parameters.AddWithValue("@DateUpdated", toUpdate.DateUpdated);

                    rowAffected = command.ExecuteNonQuery();
                }

                connection.Dispose();
            }

            // add workflow history and notification for the newly added workflow
            if (rowAffected >= 1)
            {
                // add workflow notification and history
                notification = AddWorkflowNotification(beforeUpdate, toUpdate);
                history = AddWorkflowHistory(toUpdate);

                return true;
            }

            return false;
        }

        public bool DeleteWorkflow(int id)
        {
            int rowAffected = 0;

            using (var connection = new SqlConnection(PrescienceRxConnectionString))
            {
                connection.Open();

                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = @"DELETE FROM dbo.Workflow WHERE WorkflowID = @WorkflowID";
                    command.Parameters.AddWithValue("@WorkflowID", id);

                    rowAffected = command.ExecuteNonQuery();
                }

                connection.Dispose();
            }

            return rowAffected == 1 ? true : false;
        }

        private bool AddWorkflowHistory(Workflow toInsert)
        {
            int rowAffected = 0;

            using (var connection = new SqlConnection(PrescienceRxConnectionString))
            {
                connection.Open();

                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = @"INSERT INTO dbo.WorkflowHistory VALUES(@WorkflowID, @Payload, @DateCreated)";

                    command.Parameters.AddWithValue("@WorkflowID", toInsert.WorkflowID);
                    command.Parameters.AddWithValue("@Payload", ConvertObjectToJSON<Workflow>(toInsert));
                    command.Parameters.AddWithValue("@DateCreated", DateTime.Now);

                    rowAffected = command.ExecuteNonQuery();
                }

                connection.Dispose();
            }

            return rowAffected == 1 ? true : false;
        }

        private bool AddWorkflowNotification(Workflow before, Workflow after)
        {
            int rowAffected = 0;
            string payloadMessage = "";
            string notificationType = "";

            // new workflow
            if (before == null)
            {
                payloadMessage = "{'Message':'There is a new task assigned to you!','WorkflowName':'" + after.WorkflowTitle + "'," + "'DueDate':'" + after.DueDate + "'}";
                notificationType = "NewWorkflow";
            }

            // existing workflow
            if (before != null)
            {
                if (before.WorkerID != after.WorkerID && before.DueDate != after.DueDate)
                {
                    payloadMessage = "{'Message': 'There is an existing task reassigned to you!','WorkflowName':'" + after.WorkflowTitle + "'," + "'DueDate':'" + after.DueDate + "'}";
                    notificationType = "UpdateWorkerAndDueDate";
                }

                if (before.WorkerID == after.WorkerID && before.DueDate != after.DueDate)
                {
                    payloadMessage = "{'Message': 'The due date for an existing task is changed!','WorkflowName':'" + after.WorkflowTitle + "'," + "'DueDate':'" + after.DueDate + "'}";
                    notificationType = "UpdateDueDate";
                }

                if (before.WorkerID != after.WorkerID && before.DueDate == after.DueDate)
                {
                    payloadMessage = "{'Message': 'There is an existing task reassigned to you!','WorkflowName':'" + after.WorkflowTitle + "'," + "'DueDate':'" + after.DueDate + "'}";
                    notificationType = "UpdateWorker";
                }
            }

            using (var connection = new SqlConnection(PrescienceRxConnectionString))
            {
                connection.Open();

                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = @"INSERT INTO dbo.WorkflowNotification VALUES(@WorkflowID, @Payload, @NotificationType, @DateCreated)";

                    command.Parameters.AddWithValue("@WorkflowID", after.WorkflowID);
                    command.Parameters.AddWithValue("@Payload", payloadMessage);
                    command.Parameters.AddWithValue("@NotificationType", notificationType);
                    command.Parameters.AddWithValue("@DateCreated", DateTime.Now);

                    rowAffected = command.ExecuteNonQuery();
                }

                connection.Dispose();
            }

            return rowAffected == 1 ? true : false;
        }

        private Workflow GetWorkflowByID(int workflowID)
        {
            IList<Workflow> workflowList = new List<Workflow>();

            using (var connection = new SqlConnection(PrescienceRxConnectionString))
            {
                connection.Open();

                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = @"SELECT WorkflowID, CategoryID, OwnerID, WorkerID, WorkflowTitle, WorkflowNote, DueDate, StatusID, DateCreated, DateUpdated FROM dbo.Workflow WHERE WorkflowID = @WorkflowID";
                    command.Parameters.AddWithValue("@WorkflowID", workflowID);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            workflowList.Add(new Workflow(reader));
                        }
                    }
                }

                connection.Dispose();
            }

            return workflowList.First();
        }

        private string ConvertObjectToJSON<T>(T theObject)
        {
            var javaScriptSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            string jsonString = javaScriptSerializer.Serialize(theObject);
            return jsonString;
        }
    }
}
