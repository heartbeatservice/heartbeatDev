using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HBS.Data.Abstract;
using HBS.Data.Concrete;
using HBS.Entities;
using System.Web;

namespace HBS.WebApi.Controllers
{
    public class AppointmentController : ApiController
    {

        IAppointmentRepository repository;
        public AppointmentController(IAppointmentRepository repo)
        {
            repository = repo;
        }

        public int PostAddAppointment([FromBody]Appointment appointment)
        {
            return repository.AddAppointment(appointment);
        }

        public IList<Appointment> GetCustomerAppointment(int customerId)
        {
            return repository.GetCustomerAppointments(customerId);
        }

        public bool PutUpdateAppointment([FromBody]Appointment appointment)
        {
            return repository.UpdateAppointment(appointment);
        }
    }
}
