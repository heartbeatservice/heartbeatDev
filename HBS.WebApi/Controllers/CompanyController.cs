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
    public class CompanyController : ApiController
    {
        ICommonRepository repository;

        public CompanyController(ICommonRepository repo)
        {
            repository= repo;
        }

        public int PostAddCustomer([FromBody] Company company)
        {
            return repository.AddCompany(company);
        }

        public Company GetCompany(int companyId)
        {
            return repository.GetCompnay(companyId);

        }

        public bool PutUpdateCompany([FromBody] Company company)
        {
            return repository.UpdateCompany(company);
        }

        public bool DeleteInactiveCompany(int companyId, int updatedBy)
        {
            return repository.RemoveCompany(companyId, updatedBy);
        }

        public IList<Company> GetAllCompanies()
        {
            return repository.GetAllCompanies();
        }

        public IList<Company> GetAllCompanies(string companyName)
        {
            return repository.GetCompanies(companyName);
        }



    }
}
