using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HBS.Entities.Utilities;

namespace HBS.Entities
{
    public class UserProfile : BaseEntity
    {
        public int CompanyId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int UserId { get; set; }

        public UserProfile()
        {
            
        }

        public UserProfile(int companyId,string userName,string password,string firstName,string lastName,string email,int userId)
            : this()
        {

        }

        public UserProfile(IDataReader dbReader)
            :this()
        {
            if (dbReader["CompanyId"] != DBNull.Value) CompanyId = (int)dbReader["CompanyId"];
            if (dbReader["UserName"] != DBNull.Value) UserName = (string)dbReader["UserName"];
            if (dbReader["FirstName"] != DBNull.Value) FirstName = (string)dbReader["FirstName"];
            if (dbReader["LastName"] != DBNull.Value) LastName = (string)dbReader["LastName"];
            if (dbReader["Email"] != DBNull.Value) Email = (string)dbReader["Email"];
            if (dbReader["UserId"] != DBNull.Value) UserId = (int)dbReader["UserId"];
            if (dbReader["Password"] != DBNull.Value) Password = (string)dbReader["Password"];
            
        }
    }
}
