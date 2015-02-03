using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HBS.Entities;

namespace HBS.Data.Abstract
{
    public interface IAppointmentRepository
    {


      int AddAppointment(Appointment appointment);

       bool UpdateAppointment(Appointment appointment);


        List<Appointment> GetProfessionalAppointments(int professionalId, DateTime appointmentDate);


      List<Appointment> GetCustomerAppointments(int customerId);
        


    }
}
