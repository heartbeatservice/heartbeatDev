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
    public class ProfessionalController : ApiController
    {
        public IProfessionalRepository professionalRepository;

       public  ProfessionalController(IProfessionalRepository repo)
        {
            professionalRepository = repo;
        }

       public List<Professional> GetProfessionals(int CompanyId, string ProfessionalName)
       {
           return professionalRepository.GetProfessionals(CompanyId, ProfessionalName);
       }

       public List<Professional> GetProfessionals(int CompanyId)
       {
           return professionalRepository.GetProfessionals(CompanyId,null);
       }
         
        public Professional GetProfessional(int id)
        {
            return professionalRepository.GetProfessional(id);
        }

        public int PostProfessional([FromBody] Professional pro)
        {
           return professionalRepository.AddProfessional(pro);

        }

        public bool PutProfessional([FromBody] Professional pro)
        {
            return professionalRepository.UpdateProfessional(pro);
        }



        //TODO : need to move this in base class and inherit all controls from base class.
        [AcceptVerbs("OPTIONS")]
        public HttpResponseMessage Options()
        {
            var resp = new HttpResponseMessage(HttpStatusCode.OK);

            return resp;
        }
              




    }
}
