using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HBS.Entities.Utilities;
using System.Data;

namespace HBS.Entities
{
    public class Customer : BaseEntity
    {
        public int CustomerId { get; set; }
        public int CompanyId { get; set; }   //TODO: we already have Created by in base class so why we need user ID here? 
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleInitial { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string DateOfBirth { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string HomePhone { get; set; }
        public string CellPhone { get; set; }
        public List<Appointment> CustomerAppointments { get; set; }


        public Customer()
        {
            CustomerAppointments = new List<Appointment>();
        }

        public Customer(IDataReader dbReader)
            : this()
        {
            

            if (dbReader.HasColumn("CompanyId") && dbReader["CompanyId"] != DBNull.Value)
                CompanyId = (int)dbReader["CompanyId"];

            if (dbReader.HasColumn("CustomerId") && dbReader["CustomerId"] != DBNull.Value)
                CustomerId = (int)dbReader["CustomerId"];

            if (dbReader.HasColumn("FirstName") && dbReader["FirstName"] != DBNull.Value)
                FirstName = (string)dbReader["FirstName"];

            if (dbReader.HasColumn("LastName") && dbReader["LastName"] != DBNull.Value)
                LastName = (string)dbReader["LastName"];
            
            if (dbReader.HasColumn("MiddleInitial") && dbReader["MiddleInitial"] != DBNull.Value)
                MiddleInitial = (string)dbReader["MiddleInitial"];
            
            if (dbReader.HasColumn("Address1") && dbReader["Address1"] != DBNull.Value)
                Address1 = (string)dbReader["Address1"];
            
            if (dbReader.HasColumn("Address2") && dbReader["Address2"] != DBNull.Value)
                Address2 = (string)dbReader["Address2"];

            if (dbReader.HasColumn("DateOfBirth") && dbReader["DateOfBirth"] != DBNull.Value)
                DateOfBirth = Convert.ToDateTime(dbReader["DateOfBirth"]).ToShortDateString();

            if (dbReader.HasColumn("City") && dbReader["City"] != DBNull.Value)
                City = (string)dbReader["City"];
            
            if (dbReader.HasColumn("State") && dbReader["State"] != DBNull.Value)
                State = (string)dbReader["State"];
            
            if (dbReader.HasColumn("Zip") && dbReader["Zip"] != DBNull.Value)
                Zip = (string)dbReader["Zip"];

            if (dbReader.HasColumn("HomePhone") && dbReader["HomePhone"] != DBNull.Value)
                HomePhone = (string)dbReader["HomePhone"];
            if (dbReader.HasColumn("Email") && dbReader["Email"] != DBNull.Value)
                Email = (string)dbReader["Email"];
            
            if (dbReader.HasColumn("CellPhone") && dbReader["CellPhone"] != DBNull.Value)
                CellPhone = (string)dbReader["CellPhone"];
            
            if (dbReader.HasColumn("CreatedBy") && dbReader["CreatedBy"] != DBNull.Value)
                base.CreatedBy = (int)dbReader["CreatedBy"];
            
            if (dbReader.HasColumn("UpdatedBy") && dbReader["UpdatedBy"] != DBNull.Value)
                base.UpdatedBy = (int)dbReader["UpdatedBy"];
            
            if (dbReader.HasColumn("DateCreated") && dbReader["DateCreated"] != DBNull.Value)
                base.DateCreated = (DateTime)dbReader["DateCreated"];
            
            if (dbReader.HasColumn("DateUpdated") && dbReader["DateUpdated"] != DBNull.Value)
                base.DateUpdated = (DateTime)dbReader["DateUpdated"];

        }


    }
}
