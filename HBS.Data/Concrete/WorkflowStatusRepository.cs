using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HBS.Data.Abstract;
using HBS.Entities;

namespace HBS.Data.Concrete
{
    public class WorkflowStatusRepository : BaseRepository, IWorkflowStatusRepository
    {
        public IQueryable<KendoDDL> GetAllWorkflowStatus()
        {
            IList<KendoDDL> statusList = new List<KendoDDL>();

            using (var conn = new SqlConnection(PrescienceRxConnectionString))
            {
                conn.Open();

                using (SqlCommand command = conn.CreateCommand())
                {
                    command.CommandText = @"SELECT WorkflowStatusID, StatusName FROM dbo.WorkflowStatus";

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            statusList.Add(new KendoDDL(reader, "Status"));
                        }
                    }
                }
            }

            return statusList.AsQueryable();
        }
    }
}
