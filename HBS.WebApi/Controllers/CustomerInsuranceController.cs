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
    public class CustomerInsuranceController : ApiController
    {
     ICustomerRepository repository;

     public CustomerInsuranceController(ICustomerRepository repo)
        {
            repository = repo;
        }


     public bool PostAddCustomerInsurance([FromBody]CustomerInsurance customerInsurance)
     {
         return repository.AddCustomerInsurance(customerInsurance);

     }

     public List<CustomerInsurance>  GetCustomerInsurance(int customerInsuranceId)
     {
         return repository.GetCustomerInsurance(customerInsuranceId);
     }


     public bool PutUpdateCustomerInsurance([FromBody]CustomerInsurance customerInsurance)
     {
         return repository.UpdateCustomerInsurance(customerInsurance);
     }

     public bool DeleteInActiveCustomerInsurance(int customerInsuranceId)
     {
         return repository.RemoveCustomerInsurance(customerInsuranceId);
     }


     [AcceptVerbs("OPTIONS")]
     public HttpResponseMessage Options()
     {
         var resp = new HttpResponseMessage(HttpStatusCode.OK);

         return resp;
     }  

    }
}
