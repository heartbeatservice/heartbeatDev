using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using HBS.Data.Abstract;
using HBS.Entities;

namespace HBS.Data.Concrete
{
    public class ProfessionalRepository : BaseRepository, IProfessionalRepository
    {

        private const string GetProfessionalByIdSp = "GetProfessionalById";
        private const string GetProfessionalsSp = "GetProfessionals";
        private const string AddProfessionalSp = "AddProfessional";
        private const string UpdateProfessionalSp = "UpdateProfessional";
        private const string AddProfessionalScheduleSp = "AddUpdateProfessionalAppointment";
        private const string UpdateProfessionalScheduleSp = "AddUpdateProfessionalAppointment";
        private const string GetProfessionalScheduleByIdSp = "GetProfessionalScheduleById";
        private const string GetProfessionalScheduleByDateSp = "GetProfessionalScheduleByDate";
        private const string GetProfessionalMonthlySchedule = "GetProfessionalsMonthlySchedule";

        public int AddProfessional(Professional professional)
        {
            using (var conn = new SqlConnection(PrescienceRxConnectionString))
            {
                conn.Open();

                using (var cmd = new SqlCommand(AddProfessionalSp, conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.Add("@ProfessionalTypeId", SqlDbType.Int).Value = professional.ProfessionalTypeId;
                    cmd.Parameters.Add("@CompanyId", SqlDbType.Int).Value = professional.CompanyId;
                    cmd.Parameters.Add("@FirstName", SqlDbType.VarChar).Value = professional.FirstName;
                    cmd.Parameters.Add("@LastName", SqlDbType.VarChar).Value = professional.LastName;
                    cmd.Parameters.Add("@Phone", SqlDbType.VarChar).Value = professional.Phone;
                    cmd.Parameters.Add("@ProfessionalIdentificationNumber", SqlDbType.VarChar).Value = professional.ProfessionalIdentificationNumber;
                    cmd.Parameters.Add("@CreatedBy", SqlDbType.Int).Value = professional.CreatedBy;
                    //cmd.Parameters.Add("@CreatedDate", SqlDbType.DateTime).Value = DateTime.UtcNow;

                    return Convert.ToInt16(cmd.ExecuteScalar());

                }
            }
        }

        public bool UpdateProfessional(Professional professional)
        {
            using (var conn = new SqlConnection(PrescienceRxConnectionString))
            {
                conn.Open();

                using (var cmd = new SqlCommand(UpdateProfessionalSp, conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.Add("@ProfessionalId", SqlDbType.Int).Value = professional.ProfessionalId;
                    cmd.Parameters.Add("@ProfessionalTypeId", SqlDbType.Int).Value = professional.ProfessionalTypeId;
                    cmd.Parameters.Add("@CompanyId", SqlDbType.Int).Value = professional.CompanyId;
                    cmd.Parameters.Add("@FirstName", SqlDbType.VarChar).Value = professional.FirstName;
                    cmd.Parameters.Add("@LastName", SqlDbType.VarChar).Value = professional.LastName;
                    cmd.Parameters.Add("@Phone", SqlDbType.VarChar).Value = professional.Phone;
                    cmd.Parameters.Add("@ProfessionalIdentificationNumber", SqlDbType.VarChar).Value = professional.ProfessionalIdentificationNumber;
                    cmd.Parameters.Add("@IsActive", SqlDbType.Bit).Value = professional.IsActive;
                    cmd.Parameters.Add("@UpdatedBy", SqlDbType.Int).Value = professional.UpdatedBy;
                    //cmd.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = DateTime.UtcNow;

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public Professional GetProfessional(int professionalId)
        {
            Professional professional = null;
            using (var conn = new SqlConnection(PrescienceRxConnectionString))
            {
                conn.Open();

                using (var cmd = new SqlCommand(GetProfessionalByIdSp, conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ProfessionalId", SqlDbType.Int);
                    cmd.Parameters["@ProfessionalId"].Value = professionalId;

                    using (var myReader = cmd.ExecuteReader())
                    {
                        try
                        {
                            if (myReader.HasRows)
                            {
                                myReader.Read();
                                professional = new Professional(myReader);

                                if (myReader.NextResult())
                                {
                                    professional.ProfessionalSchedules = new List<ProfessionalSchedule>();
                                    while (myReader.Read())
                                    {
                                        professional.ProfessionalSchedules.Add(new ProfessionalSchedule(myReader));
                                    }
                                }

                                if (myReader.NextResult())
                                {
                                    professional.ProfessionalAppointmentses = new List<Appointment>();
                                    while (myReader.Read())
                                    {
                                        professional.ProfessionalAppointmentses.Add(new Appointment(myReader));
                                    }
                                }

                            }

                        }
                        catch (Exception ex)
                        {
                            // TODO Logg Error here
                        }
                    }
                }

            }
            return professional;

        }

        public List<Professional> GetProfessionals(int companyId, string professionalName)
        {

            var professionals = new List<Professional>();
            using (var conn = new SqlConnection(PrescienceRxConnectionString))
            {
                conn.Open();
                using (var cmd = new SqlCommand(GetProfessionalsSp, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@CompanyId", SqlDbType.Int);
                    cmd.Parameters["@CompanyId"].Value = companyId;

                    if (!string.IsNullOrWhiteSpace(professionalName))
                    {
                        cmd.Parameters.Add("@Name", SqlDbType.VarChar);
                        cmd.Parameters["@Name"].Value = professionalName;
                    }

                    using (var myReader = cmd.ExecuteReader())
                    {
                        try
                        {
                            while (myReader.Read())
                            {
                                professionals.Add(new Professional(myReader));
                            }
                        }
                        catch (Exception ex)
                        {
                            // TODO Logg Error here
                        }
                    }
                }
            }
            return professionals;
        }



        public bool RemoveProfessional(int professionalId, int removedBy)
        {
            using (var conn = new SqlConnection(PrescienceRxConnectionString))
            {
                conn.Open();

                using (var cmd = new SqlCommand(UpdateProfessionalSp, conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.Add("@ProfessionalId", SqlDbType.Int).Value = professionalId;
                    cmd.Parameters.Add("@IsActive", SqlDbType.Bit).Value = false;
                    cmd.Parameters.Add("@UpdatedBy", SqlDbType.Int).Value = removedBy;
                    cmd.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = DateTime.UtcNow;

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool AddProfessionalSchedule(KendoEntity professionalSchedule)
        {

 





            using (var conn = new SqlConnection(PrescienceRxConnectionString))
            {
                conn.Open();

                using (var cmd = new SqlCommand(AddProfessionalScheduleSp, conn))
                {
                    //ToDo  : Fix this method

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    TimeZoneInfo easternZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
                    professionalSchedule.Start = TimeZoneInfo.ConvertTimeFromUtc(Convert.ToDateTime(professionalSchedule.Start).ToUniversalTime(), easternZone).ToString();
                    professionalSchedule.End=TimeZoneInfo.ConvertTimeFromUtc(Convert.ToDateTime(professionalSchedule.End).ToUniversalTime(),easternZone).ToString();
                    cmd.Parameters.Add("@ProfessionalId", SqlDbType.Int).Value = professionalSchedule.ProfessionalId;
                    cmd.Parameters.Add("@CustomerId", SqlDbType.Int).Value = professionalSchedule.OwnerID;
                    cmd.Parameters.Add("@Title", SqlDbType.NVarChar).Value = professionalSchedule.Title;
                    cmd.Parameters.Add("@StartTime", SqlDbType.DateTime).Value = professionalSchedule.Start;
                    cmd.Parameters.Add("@EndTime", SqlDbType.DateTime).Value = professionalSchedule.End;
                    cmd.Parameters.Add("@Comments", SqlDbType.NVarChar).Value = professionalSchedule.Description;
                    cmd.Parameters.Add("@CreatedBy ", SqlDbType.NVarChar).Value = professionalSchedule.UserId;
                    cmd.Parameters.Add("@DateCreated", SqlDbType.NVarChar).Value = DateTime.UtcNow;
          
                //TODO: Add this back, these parms missing from sp
                    //cmd.Parameters.Add("@CreatedBy", SqlDbType.VarChar).Value = professionalSchedule.CreatedBy;
                    //cmd.Parameters.Add("@CreatedDate", SqlDbType.DateTime).Value = DateTime.UtcNow;

                    return Convert.ToBoolean(cmd.ExecuteScalar());

                }
            }
        }

        public bool UpdateProfessionalSchedule(KendoEntity professionalSchedule)
        {
            using (var conn = new SqlConnection(PrescienceRxConnectionString))
            {
                conn.Open();

                //TODO: Fix this method

                using (var cmd = new SqlCommand(UpdateProfessionalScheduleSp, conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    TimeZoneInfo easternZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
                    professionalSchedule.Start = TimeZoneInfo.ConvertTimeFromUtc(Convert.ToDateTime(professionalSchedule.Start).ToUniversalTime(), easternZone).ToString();
                    professionalSchedule.End = TimeZoneInfo.ConvertTimeFromUtc(Convert.ToDateTime(professionalSchedule.End).ToUniversalTime(), easternZone).ToString();

                    cmd.Parameters.Add("@AppointmentId", SqlDbType.Int).Value = professionalSchedule.TaskID;
                    cmd.Parameters.Add("@ProfessionalId", SqlDbType.Int).Value = professionalSchedule.ProfessionalId;
                    cmd.Parameters.Add("@CustomerId", SqlDbType.Int).Value = professionalSchedule.OwnerID;
                    cmd.Parameters.Add("@Title", SqlDbType.NVarChar).Value = professionalSchedule.Title;
                    cmd.Parameters.Add("@StartTime", SqlDbType.DateTime).Value = professionalSchedule.Start;
                    cmd.Parameters.Add("@EndTime", SqlDbType.DateTime).Value = professionalSchedule.End;
                    cmd.Parameters.Add("@Comments", SqlDbType.NVarChar).Value = professionalSchedule.Description;
                    cmd.Parameters.Add("@UpdatedBy ", SqlDbType.NVarChar).Value = professionalSchedule.UserId;
                    cmd.Parameters.Add("@DateUpdated", SqlDbType.DateTime).Value = DateTime.UtcNow;

                    //TODO: Add this back, these parms missing from sp
                    //cmd.Parameters.Add("@UpdatedBy", SqlDbType.VarChar).Value = professionalSchedule.UpdatedBy;
                    //cmd.Parameters.Add("@DateUpdated", SqlDbType.DateTime).Value = DateTime.UtcNow;

                    return cmd.ExecuteNonQuery() > 0;

                }
            }
        }

        public ProfessionalSchedule GetProfessionalSchedule(int professionalSchedulreId)
        {
            ProfessionalSchedule professionalSchedule = null;
            using (var conn = new SqlConnection(PrescienceRxConnectionString))
            {

                conn.Open();

                using (var cmd = new SqlCommand(UpdateProfessionalScheduleSp, conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;


                    cmd.Parameters.Add("@ProfessionalScheduleId", SqlDbType.Int);
                    cmd.Parameters["@ProfessionalScheduleId"].Value = professionalSchedulreId;

                    using (var myReader = cmd.ExecuteReader())
                    {
                        try
                        {
                            if (myReader.HasRows)
                            {
                                myReader.Read();
                                professionalSchedule = new ProfessionalSchedule(myReader);
                            }
                        }
                        catch (Exception ex)
                        {
                            // TODO Logg Error here
                        }
                    }
                }

            }
            return professionalSchedule;

        }


        public List<KendoEntity> GetProfessionalMonthlyAppointments(int professionalId,int Month,int Year)
        {
            ProfessionalSchedule professionalSchedule = null;
            List<KendoEntity> lst = new List<KendoEntity>();
            using (var conn = new SqlConnection(PrescienceRxConnectionString))
            {

                conn.Open();
              
                using (var cmd = new SqlCommand(GetProfessionalMonthlySchedule, conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;


                    cmd.Parameters.Add("@ProfessionalId", SqlDbType.Int);
                    cmd.Parameters["@ProfessionalId"].Value = professionalId;
                    cmd.Parameters.Add("@Month", SqlDbType.Int);
                    cmd.Parameters["@Month"].Value = Month;
                    cmd.Parameters.Add("@Year", SqlDbType.Int);
                    cmd.Parameters["@Year"].Value = Year;
                    using (var myReader = cmd.ExecuteReader())
                    {
                        try
                        {
                            if (myReader.HasRows)
                            {
                                while (myReader.Read())
                                {
                                    professionalSchedule = new ProfessionalSchedule(myReader, "Kendo");
                                    lst.Add(professionalSchedule.kendo);
                                }

                            }
                        }
                        catch (Exception ex)
                        {
                            // TODO Logg Error here
                        }
                    }
                }

            }
            return lst;

        }
        /// <summary>
        /// depends on the parameter is passed, this method will return either
        /// 1 professional schedule or list of professionals
        /// </summary>
        /// <param name="professionalId"></param>
        /// <returns></returns>

        public List<ProfessionalSchedule> GetProfessionalScheduleListByScheduleId(int professionalScheduleId)
        {

            var professionalsSchedule = new List<ProfessionalSchedule>();
            using (var conn = new SqlConnection(PrescienceRxConnectionString))
            {
                conn.Open();
                using (var cmd = new SqlCommand(GetProfessionalScheduleByIdSp, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@ProfessionalScheduleId", SqlDbType.Int);
                    cmd.Parameters["@ProfessionalScheduleId"].Value = professionalScheduleId;



                    using (var myReader = cmd.ExecuteReader())
                    {
                        try
                        {
                            while (myReader.Read())
                            {
                                professionalsSchedule.Add(new ProfessionalSchedule(myReader));
                            }
                        }
                        catch (Exception ex)
                        {
                            // TODO Logg Error here
                        }

                        return professionalsSchedule;
                    }

                }
            }
        }







        public List<ProfessionalSchedule> GetProfessionalScheduleByScheduleDate(DateTime scheduleDate)
        {
            var professionalSchedule = new List<ProfessionalSchedule>();
            using (var conn = new SqlConnection(PrescienceRxConnectionString))
            {
                conn.Open();
                using (var cmd = new SqlCommand(GetProfessionalScheduleByDateSp, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@StartDate", SqlDbType.DateTime);
                    cmd.Parameters["@StartDate"].Value = scheduleDate;

                    using (var myReader = cmd.ExecuteReader())
                    {
                        try
                        {
                            while (myReader.Read())
                            {
                                professionalSchedule.Add(new ProfessionalSchedule(myReader));
                            }
                        }
                        catch (Exception ex)
                        {
                            // TODO Logg Error here
                        }
                    }
                }
            }
            return professionalSchedule;
        }

        public bool RemoveProfessionalSchedule(int professionalSchduleId, int removedByUserId)
        {
            using (var conn = new SqlConnection(PrescienceRxConnectionString))
            {
                conn.Open();

                //TODO: This stored procedure is not final till the time I am writing this method. 
                // naveed need to discuss this with Saqib. Make sure the sp is correct. 

                using (var cmd = new SqlCommand(UpdateProfessionalScheduleSp, conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.Add("@ProfessionalSchduleId", SqlDbType.Int).Value = professionalSchduleId;
                    cmd.Parameters.Add("@IsActive", SqlDbType.Bit).Value = false;
                    cmd.Parameters.Add("@UpdatedBy", SqlDbType.Int).Value = removedByUserId;
                    cmd.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = DateTime.UtcNow;

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
    }
}
