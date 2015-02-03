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
    public class InsuranceController : ApiController
    {

        ICommonRepository repository;

        public InsuranceController(ICommonRepository repo)
        {
            repository= repo;
        }

        public bool PostAddInsurnace([FromBody] Insurance insurance)
        {
            return repository.AddInsurance(insurance);
        }

        public Insurance GetInsurance(int insuranceId)
        {
            return repository.GetInsurance(insuranceId);

        }

        public bool PutUpdateInsurance([FromBody] Insurance insurance)
        {
            return repository.UpdateInsurance(insurance);
        }

        public bool DeleteInactiveInsurance(int insuranceId, int updatedBy)
        {
            return repository.RemoveInsurance(insuranceId, updatedBy);
        }
        public IList<Insurance> GetInsurances(int companyId, string insuranceName)
        {
            return repository.GetInsurances(companyId, insuranceName);
        }

        [AcceptVerbs("OPTIONS")]
        public HttpResponseMessage Options()
        {
            var resp = new HttpResponseMessage(HttpStatusCode.OK);

            return resp;
        }
    }
}
