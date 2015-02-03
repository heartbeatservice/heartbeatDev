using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HBS.Data.Abstract;
using HBS.Entities;
using System.Data.SqlClient;
using System.Data;

namespace HBS.Data.Concrete
{
    public class AppointmentRepository : BaseRepository, IAppointmentRepository
    {
       
        
        private const string AddAppointmentSp = "AddAppointment";
        private const string UpdateAppointmentSp = "UpdateAppointment";
        private const string GetProfessionalAppointmentsSp = "GetProfessionalAppointments";
        private const string GetCustomerAppointmentsSp = "GetCustomerAppointments";


        public int AddAppointment(Appointment appointment)
        {
            using (var conn = new SqlConnection(PrescienceRxConnectionString))
            {
                conn.Open();
             
                using (var cmd = new SqlCommand(AddAppointmentSp, conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                   
                    cmd.Parameters.Add("@ProfessionalId", System.Data.SqlDbType.Int);
                    cmd.Parameters["@ProfessionalId"].Value = appointment.ProfessionalId;

                    cmd.Parameters.Add("@CustomerId", System.Data.SqlDbType.Int);
                    cmd.Parameters["@CustomerId"].Value = appointment.CustomerId;

                    cmd.Parameters.Add("@AppointmentDate", System.Data.SqlDbType.DateTime);
                    cmd.Parameters["@AppointmentDate"].Value = appointment.AppointmentDate;

                    cmd.Parameters.Add("@AppointmentStartTime", System.Data.SqlDbType.VarChar);
                    cmd.Parameters["@AppointmentStartTime"].Value = appointment.AppointmentStartTime;

                    cmd.Parameters.Add("@StatusId", System.Data.SqlDbType.Int);
                    cmd.Parameters["@StatusId"].Value = appointment.StatusId;

                 
                    cmd.Parameters.Add("@CreatedBy", System.Data.SqlDbType.Int);
                    cmd.Parameters["@CreatedBy"].Value = appointment.CreatedBy;

                    cmd.Parameters.Add("@CreatedDate", System.Data.SqlDbType.DateTime);
                    cmd.Parameters["@CreatedDate"].Value = DateTime.UtcNow;

                    return Convert.ToInt16(cmd.ExecuteScalar());

                }
            }
        }

        public bool UpdateAppointment(Appointment appointment)
        {
            using (var conn = new SqlConnection(PrescienceRxConnectionString))
            {
                conn.Open();
               
                using (var cmd = new SqlCommand(UpdateAppointmentSp, conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.Add("@AppointmentId", System.Data.SqlDbType.Int);
                    cmd.Parameters["@AppointmentId"].Value = appointment.AppointmentId;

                    cmd.Parameters.Add("@ProfessionalId", System.Data.SqlDbType.Int);
                    cmd.Parameters["@ProfessionalId"].Value = appointment.ProfessionalId;

                    cmd.Parameters.Add("@CustomerId", System.Data.SqlDbType.Int);
                    cmd.Parameters["@CustomerId"].Value = appointment.CustomerId;

                    cmd.Parameters.Add("@AppointmentDate", System.Data.SqlDbType.DateTime);
                    cmd.Parameters["@AppointmentDate"].Value = appointment.AppointmentDate;

                    cmd.Parameters.Add("@AppointmentStartTime", System.Data.SqlDbType.VarChar);
                    cmd.Parameters["@AppointmentStartTime"].Value = appointment.AppointmentStartTime;

                    cmd.Parameters.Add("@StatusId", System.Data.SqlDbType.Int);
                    cmd.Parameters["@StatusId"].Value = appointment.StatusId;

                    cmd.Parameters.Add("@StatusId", System.Data.SqlDbType.VarChar);
                    cmd.Parameters["@StatusId"].Value = appointment.Comments;
                   

                    cmd.Parameters.Add("@UpdatedBy", System.Data.SqlDbType.Int);
                    cmd.Parameters["@UpdatedBy"].Value = appointment.UpdatedBy;

                    cmd.Parameters.Add("@UpdatedDate", System.Data.SqlDbType.DateTime);
                    cmd.Parameters["@UpdatedDate"].Value = DateTime.UtcNow;

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }


       public List<Appointment> GetProfessionalAppointments(int professionalId, DateTime appointmentDate)
        {

            var appointment = new List<Appointment>();
            using (var conn = new SqlConnection(PrescienceRxConnectionString))
            {
                conn.Open();
                using (var cmd = new SqlCommand(GetProfessionalAppointmentsSp, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ProfessionalId", SqlDbType.Int);
                    cmd.Parameters["@ProfessionalId"].Value = professionalId;
                    cmd.Parameters.Add("@AppointmentDate", SqlDbType.DateTime);
                    cmd.Parameters["@AppointmentDate"].Value = appointmentDate;

                    using (var myReader = cmd.ExecuteReader())
                    {
                        try
                        {
                            while (myReader.Read())
                            {
                                appointment.Add(new Appointment(myReader));
                            }
                        }
                        catch (Exception ex)
                        {
                            // TODO Logg Error here
                        }
                    }
                }
            }
            return appointment;

            
           
        }


       public List<Appointment> GetCustomerAppointments(int customerId)
        {
            var appointment = new List<Appointment>();
            using (var conn = new SqlConnection(PrescienceRxConnectionString))
            {
                conn.Open();
                using (var cmd = new SqlCommand(GetCustomerAppointmentsSp, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@CustomerId", SqlDbType.Int);
                    cmd.Parameters["@CustomerId"].Value = customerId;
                 

                    using (var myReader = cmd.ExecuteReader())
                    {
                        try
                        {
                            while (myReader.Read())
                            {
                                appointment.Add(new Appointment(myReader));
                            }
                        }
                        catch (Exception ex)
                        {
                            // TODO Logg Error here
                        }
                    }
                }
            }
            return appointment;
           
        }
    }
}
