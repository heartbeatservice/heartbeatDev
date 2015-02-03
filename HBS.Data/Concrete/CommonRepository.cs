using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HBS.Data.Abstract;
using HBS.Entities;
using System.Data;

namespace HBS.Data.Concrete
{
    public class CommonRepository : BaseRepository, ICommonRepository 
    {
        private const string AddCompanySp = "AddCompany";
        private const string UpdateCompanySp = "UpdateCompany";
        private const string GetCompanyByIdSp = "GetCompanyById";
        private const string UpdateInsuranceSp = "UpdateInsurance";
        private const string GetCompaniesSp = "GetCompanies";
        private const string AddInsuranceSp = "AddInsurance";
        private const string GetInsurancesSp = "GetInsurances";
        private const string GetInsuranceByIdSp = "GetInsuranceById";
       


        public int AddCompany(Company company)//
        {

            using (var conn = new SqlConnection(PrescienceRxConnectionString))
            {
                conn.Open();

                using (var cmd = new SqlCommand(AddCompanySp, conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.Add("@CompanyName", System.Data.SqlDbType.Int);
                    cmd.Parameters["@CompanyName"].Value = company.CompanyName;

                    cmd.Parameters.Add("@Description", System.Data.SqlDbType.VarChar);
                    cmd.Parameters["@Description"].Value = company.Description;

                    cmd.Parameters.Add("@CreatedBy", System.Data.SqlDbType.Int);
                    cmd.Parameters["@CreatedBy"].Value = company.CreatedBy;

                    cmd.Parameters.Add("@CreatedDate", System.Data.SqlDbType.Int);
                    cmd.Parameters["@CreatedDate"].Value = DateTime.UtcNow;

                    return Convert.ToInt16(cmd.ExecuteScalar());
                }

            }
        }

        public bool UpdateCompany(Company company)//
        {
            using (var conn = new SqlConnection(PrescienceRxConnectionString))
            {
                conn.Open();

                using (var cmd = new SqlCommand(UpdateCompanySp, conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.Add("@CompanyName", System.Data.SqlDbType.Int);
                    cmd.Parameters["@CompanyName"].Value = company.CompanyName;

                    cmd.Parameters.Add("@Description", System.Data.SqlDbType.VarChar);
                    cmd.Parameters["@Description"].Value = company.Description;

                    cmd.Parameters.Add("@UpdatedBy", System.Data.SqlDbType.Int);
                    cmd.Parameters["@UpdatedBy"].Value = company.UpdatedBy;

                    cmd.Parameters.Add("@UpdatedDate", System.Data.SqlDbType.Int);
                    cmd.Parameters["@UpdatedDate"].Value = DateTime.UtcNow;

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public List<Company> GetAllCompanies()//
        {

           var company = new List<Company>();
            using (var conn = new SqlConnection(PrescienceRxConnectionString))
            {
                conn.Open();
                using (var cmd = new SqlCommand(GetCompaniesSp, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var myReader = cmd.ExecuteReader())
                    {
                        try
                        {
                            while (myReader.Read())
                            {
                                company.Add(new Company(myReader));
                            }
                        }
                        catch (Exception ex)
                        {
                            // TODO Logg Error here
                        }
                    }
                }
            }
            return company;
        }
            
        

        public List<Company> GetCompanies(string companyName)//
        {
             var company = new List<Company>();
            using (var conn = new SqlConnection(PrescienceRxConnectionString))
            {
                conn.Open();
                using (var cmd = new SqlCommand(GetCompaniesSp, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@CompanyName", SqlDbType.VarChar);
                    cmd.Parameters["@CompanyName"].Value = companyName; 
                    using (var myReader = cmd.ExecuteReader())
                    {
                        try
                        {
                            while (myReader.Read())
                            {
                                company.Add(new Company(myReader));
                            }
                        }
                        catch (Exception ex)
                        {
                            // TODO Logg Error here
                        }
                    }
                }
            }
            return company;
        }

        public Company GetCompnay(int companyId)//
        {
            Company company = null;
            using (var conn = new SqlConnection(PrescienceRxConnectionString))
            {
                conn.Open();

                using (var cmd = new SqlCommand(GetCompanyByIdSp, conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.Add("@CompanyId", System.Data.SqlDbType.Int);
                    cmd.Parameters["@CompanyId"].Value = companyId;


                    using (var myReader = cmd.ExecuteReader())
                    {
                        try
                        {
                            if (myReader.HasRows)
                            {
                                myReader.Read();
                                company = new Company(myReader);
                            }
                        }
                        catch (Exception ex)
                        {
                            // TODO Logg Error here
                        }
                    }
                }

            }
            return company;
        }

        public bool RemoveCompany(int compnayId, int updatedBy)//
        {
            using (var conn = new SqlConnection(PrescienceRxConnectionString))
            {
                conn.Open();

                using (var cmd = new SqlCommand(UpdateInsuranceSp, conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.Add("@CompnayId", SqlDbType.Int).Value = compnayId;
                    cmd.Parameters.Add("@IsActive", SqlDbType.Bit).Value = false;
                    cmd.Parameters.Add("@UpdatedBy", SqlDbType.Int).Value = updatedBy;
                    cmd.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = DateTime.UtcNow;

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool AddInsurance(Insurance insurance)//
        {
            using (var conn = new SqlConnection(PrescienceRxConnectionString))
            {
                conn.Open();

                using (var cmd = new SqlCommand(AddInsuranceSp, conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.Add("@CompanyId", System.Data.SqlDbType.Int);
                    cmd.Parameters["@CompanyId"].Value = insurance.CompanyId;
                    cmd.Parameters.Add("@InsuranceName", System.Data.SqlDbType.VarChar);
                    cmd.Parameters["@InsuranceName"].Value = insurance.InsuranceName;
                    cmd.Parameters.Add("@InsuranceAddress", System.Data.SqlDbType.VarChar);
                    cmd.Parameters["@InsuranceAddress"].Value = insurance.InsuranceAddress;
                    cmd.Parameters.Add("@InsurancePhone", System.Data.SqlDbType.VarChar);
                    cmd.Parameters["@InsurancePhone"].Value = insurance.InsurancePhone;
                    cmd.Parameters.Add("@InsuranceWebsite", System.Data.SqlDbType.VarChar);
                    cmd.Parameters["@InsuranceWebsite"].Value = insurance.InsuranceWebsite;

                    //cmd.Parameters.Add("@CreatedBy", System.Data.SqlDbType.Int);
                    //cmd.Parameters["@CreatedBy"].Value = insurance.CreatedBy;

                    //cmd.Parameters.Add("@CreatedDate", System.Data.SqlDbType.Int);
                    //cmd.Parameters["@CreatedDate"].Value = DateTime.UtcNow;

                    return Convert.ToBoolean(cmd.ExecuteScalar());
                }
            }
        }

        public bool UpdateInsurance(Insurance insurance)
        {
            using (var conn = new SqlConnection(PrescienceRxConnectionString))
            {
                conn.Open();

                using (var cmd = new SqlCommand(UpdateInsuranceSp, conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.Add("@CompanyId", System.Data.SqlDbType.Int);
                    cmd.Parameters["@CompanyId"].Value = insurance.CompanyId;
                    cmd.Parameters.Add("@InsuranceName", System.Data.SqlDbType.VarChar);
                    cmd.Parameters["@InsuranceName"].Value = insurance.InsuranceName;
                    cmd.Parameters.Add("@InsuranceAddress", System.Data.SqlDbType.VarChar);
                    cmd.Parameters["@InsuranceAddress"].Value = insurance.InsuranceAddress;
                    cmd.Parameters.Add("@InsurancePhone", System.Data.SqlDbType.VarChar);
                    cmd.Parameters["@InsurancePhone"].Value = insurance.InsurancePhone;
                    cmd.Parameters.Add("@InsurnaceWebSite", System.Data.SqlDbType.VarChar);
                    cmd.Parameters["@InsurnaceWebSite"].Value = insurance.InsuranceWebsite;

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public List<Insurance> GetInsurances(int companyId, string insuranceName)
        {
            var insurance = new List<Insurance>();
            using (var conn = new SqlConnection(PrescienceRxConnectionString))
            {
                conn.Open();
                using (var cmd = new SqlCommand(GetInsurancesSp, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@CompanyId", SqlDbType.Int);
                    cmd.Parameters["@CompanyId"].Value = companyId;
                    cmd.Parameters.Add("@InsuranceName", SqlDbType.VarChar);
                    cmd.Parameters["@InsuranceName"].Value = insuranceName;
                    using (var myReader = cmd.ExecuteReader())
                    {
                        try
                        {
                            while (myReader.Read())
                            {
                                insurance.Add(new Insurance(myReader));
                            }
                        }
                        catch (Exception ex)
                        {
                            // TODO Logg Error here
                        }
                    }
                }
            }
            return insurance;
        }

        public Insurance GetInsurance(int insuranceId)//
        {
            Insurance insurance = null;
            using (var conn = new SqlConnection(PrescienceRxConnectionString))
            {
                conn.Open();

                using (var cmd = new SqlCommand(GetInsuranceByIdSp, conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.Add("@InsuranceId", System.Data.SqlDbType.Int);
                    cmd.Parameters["@InsuranceId"].Value = insuranceId;


                    using (var myReader = cmd.ExecuteReader())
                    {
                        try
                        {
                            if (myReader.HasRows)
                            {
                                myReader.Read();
                                insurance = new Insurance(myReader);
                            }
                        }
                        catch (Exception ex)
                        {
                            // TODO Logg Error here
                        }
                    }
                }

            }
            return insurance;
            
        }

        public bool RemoveInsurance(int insuranceId, int updatedBy)//
        {
            using (var conn = new SqlConnection(PrescienceRxConnectionString))
            {
                conn.Open();

                using (var cmd = new SqlCommand(UpdateInsuranceSp, conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.Add("@CompnayId", SqlDbType.Int).Value = insuranceId;
                    cmd.Parameters.Add("@IsActive", SqlDbType.Bit).Value = false;
                    cmd.Parameters.Add("@UpdatedBy", SqlDbType.Int).Value = updatedBy;
                    cmd.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = DateTime.UtcNow;

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
    }
}
