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
    public class CustomerController : ApiController
    {
        private ICustomerRepository repository;
        public CustomerController(ICustomerRepository repo)
        {
            repository = repo;
        }

        public int PostCustomer([FromBody] Customer customer)
        {
            return repository.AddCustomer(customer);
        }

        public Customer GetCustomer(int customerId)
        {
            return repository.GetCustomer(customerId);
        }

        public IList<Customer> GetCustomers(int companyId, string customerName) //
        {
            return repository.GetCustomers(companyId, customerName);
        }

        public IList<Customer> GetCustomers(int companyId) //
        {
            return repository.GetCustomers(companyId,null);
        }

        public IList<Customer> GetCustomers(int companyId, string customerName, DateTime dob)
        {
           
            return repository.GetCustomers(companyId, customerName, dob);
        }

        public IList<Customer> GetCustomers(int companyId, DateTime dob)
        {
            return repository.GetCustomers(companyId, null, dob);
        }
        

        [HttpPut]
        public bool PutCustomerUpdate(string id,[FromBody] Customer customer)
        {
            return repository.UpdateCustomer(customer);
        }


        [AcceptVerbs("OPTIONS")]
        public HttpResponseMessage Options()
        {
            var resp = new HttpResponseMessage(HttpStatusCode.OK);

            return resp;
        }
            // Todo: THE SP WAS MESSED UP AND THIS METHOD IS NOT WORKING AT THE MOMENT 4/18/2014
        public bool DeleteCustomer(int customerId, int userId)
        {
            return repository.RemoveCustomer(customerId, userId);
        }
    }
}
