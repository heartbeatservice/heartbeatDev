using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HBS.Data.Abstract;
using HBS.Data.Concrete;
using HBS.Entities;

namespace HBS.WebApi.Controllers
{
    public class ProfessionalScheduleController : ApiController
    {


         public IProfessionalRepository professionalRepository;

         public ProfessionalScheduleController(IProfessionalRepository repo)
        {
            professionalRepository = repo;
        }
             
      
        
        [HttpGet]
        public List<KendoEntity> GetProfessionalScheduleById(int id,int month,int year)
        {
            return professionalRepository.GetProfessionalMonthlyAppointments(id, month, year);
           // return new List<KendoEntity>{new KendoEntity{OwnerID="2", IsAllDay=false,Title="My Meeting",Description="Testing 123",Start=DateTime.Now.ToString(),End=DateTime.Now.AddMinutes(30).ToString(),TaskID="4"}};
        }

        public bool PostProfessionalSchedule([FromBody] KendoEntity k)
        {
            KendoEntity ku = k;
           return professionalRepository.AddProfessionalSchedule(k);
            //return professionalRepository.AddProfessionalSchedule(proSchedule);

        }

        //TODO:Get confirmation. 
        //User the same method to soft delete dont need have another method to softdelete, this method does have IsActive param. 
        public bool PutProfessionalSchedule([FromBody] KendoEntity k)
        {

            KendoEntity ku = k;

            
            return professionalRepository.UpdateProfessionalSchedule(k);
        }

        //public List<ProfessionalSchedule> GetProfessionalsScheduleList(int id)
        //{
        //    return professionalRepository.GetProfessionalScheduleListByScheduleId(id);
        //}

        public List<ProfessionalSchedule> GetProfessionalScheduleListByDate(DateTime date)
        {
            return professionalRepository.GetProfessionalScheduleByScheduleDate(date);
        }

        [AcceptVerbs("OPTIONS")]
        public HttpResponseMessage Options()
        {
            var resp = new HttpResponseMessage(HttpStatusCode.OK);

            return resp;
        }
    }
}
