using System.Collections.Generic;
using HBS.Entities;

namespace HBS.Data.Abstract
{
    public interface ICommonRepository
    {
        int AddCompany(Company company);
        bool UpdateCompany(Company company);

        List<Company> GetAllCompanies();
        List<Company> GetCompanies(string companyName);
        Company GetCompnay(int companyId);
        bool RemoveCompany(int compnayId, int updatedBy);


        bool AddInsurance(Insurance insurance);
        bool UpdateInsurance(Insurance insurance);
        List<Insurance> GetInsurances(int companyId, string insuranceName);
        Insurance GetInsurance(int insuranceId);
        bool RemoveInsurance(int insuranceId, int updatedBy);




    }
}
