using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HBS.Entities.Utilities;
using System.Data;

namespace HBS.Entities
{
    public class ProfessionalSchedule:BaseEntity
    {

        public int ProfessionalScheduleId { get; set; }
        public int ProfessionalId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int TimeIntervalMinutes { get; set; }

        public KendoEntity kendo;
        public ProfessionalSchedule()
        { }

        public ProfessionalSchedule(IDataReader dbReader)
            : this()
        {

            if (dbReader.HasColumn("ProfessionalScheduleId") && dbReader["ProfessionalScheduleId"] != DBNull.Value)
                ProfessionalScheduleId = (int)dbReader["ProfessionalScheduleId"];
            
            if (dbReader.HasColumn("ProfessionalId") && dbReader["ProfessionalId"] != DBNull.Value)
                ProfessionalId = (int)dbReader["ProfessionalId"];
            
            if (dbReader.HasColumn("StartTime") && dbReader["StartTime"] != DBNull.Value)
                StartTime = (DateTime)dbReader["StartTime"];
            
            if (dbReader.HasColumn("EndTime") && dbReader["EndTime"] != DBNull.Value)
                EndTime = (DateTime)dbReader["EndTime"];

            if (dbReader.HasColumn("TimeIntervalMinutes") && dbReader["TimeIntervalMinutes"] != DBNull.Value)
                TimeIntervalMinutes = Convert.ToInt16(dbReader["TimeIntervalMinutes"]);        
            
            if (dbReader.HasColumn("CreatedBy") && dbReader["CreatedBy"] != DBNull.Value)
                base.CreatedBy = (int)dbReader["CreatedBy"];
            
            if (dbReader.HasColumn("UpdatedBy") && dbReader["UpdatedBy"] != DBNull.Value)
                base.UpdatedBy = (int)dbReader["UpdatedBy"];
            
            if (dbReader.HasColumn("DateCreated") && dbReader["DateCreated"] != DBNull.Value)
                base.DateCreated = (DateTime)dbReader["DateCreated"];
            
            if (dbReader.HasColumn("DateUpdated") && dbReader["DateUpdated"] != DBNull.Value)
                base.DateUpdated = (DateTime)dbReader["DateUpdated"];
        }
        public ProfessionalSchedule(IDataReader dbReader,string type)
            : this()
        {
            kendo = new KendoEntity();
            try
            {
                if (dbReader.HasColumn("OwnerID") && dbReader["OwnerID"] != DBNull.Value)
                    kendo.OwnerID = dbReader["OwnerID"].ToString();

                if (dbReader.HasColumn("IsAllday") && dbReader["IsAllday"] != DBNull.Value)
                    kendo.IsAllDay = false;

                if (dbReader.HasColumn("Title") && dbReader["Title"] != DBNull.Value)
                    kendo.Title = dbReader["Title"].ToString();

                if (dbReader.HasColumn("Description") && dbReader["Description"] != DBNull.Value)
                    kendo.Description = dbReader["Description"].ToString();

                if (dbReader.HasColumn("Start") && dbReader["Start"] != DBNull.Value)
                    kendo.Start = dbReader["Start"].ToString();

                if (dbReader.HasColumn("End") && dbReader["End"] != DBNull.Value)
                    kendo.End = dbReader["End"].ToString();

                if (dbReader.HasColumn("TaskId") && dbReader["TaskId"] != DBNull.Value)
                    kendo.TaskID = dbReader["TaskId"].ToString();
            }
            catch (Exception ex)
            {
                string exc = ex.Message;
            }

        }

    }
}
