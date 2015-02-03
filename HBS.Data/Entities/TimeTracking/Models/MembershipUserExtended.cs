using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using HBS.Data.Entities.TimeTracking.EF;
using HBS.Data.Entities.TimeTracking.Raven;

namespace HBS.Data.Entities.TimeTracking.Models
{
    public class MembershipUserExtended : MembershipUser, IComparable<MembershipUserExtended>
    {
/*
        private TimeTrackingEntities _tTimeTrackingEntities;
*/

        public MembershipUserExtended(MembershipUser mu)
            : base(mu.ProviderName, mu.UserName, mu.ProviderUserKey, mu.Email, mu.PasswordQuestion,
            mu.Comment, mu.IsApproved, mu.IsLockedOut, mu.CreationDate, mu.LastLoginDate, mu.LastActivityDate,
            mu.LastPasswordChangedDate, mu.LastLockoutDate)
        {

        }

        public MembershipUserExtended(MembershipUser mu, string firstName, string lastName, string title, double? hourlyRate, string address, string city, string state, string zip, string phone, string createdBy, DateTime? createdDate, string updatedBy, DateTime? updatedDate,string password)
            : base(mu.ProviderName, mu.UserName, mu.ProviderUserKey, mu.Email, mu.PasswordQuestion,
            mu.Comment, mu.IsApproved, mu.IsLockedOut, mu.CreationDate, mu.LastLoginDate, mu.LastActivityDate,
            mu.LastPasswordChangedDate, mu.LastLockoutDate)
        {
            FirstName = firstName ?? string.Empty;
            LastName = lastName ?? string.Empty;
            HourlyRate = hourlyRate;
            Phone = phone ?? string.Empty;
            Title = title ?? string.Empty;
            Address = address ?? string.Empty;
            City = city ?? string.Empty;
            State = state ?? string.Empty;
            Zip = zip ?? string.Empty;
            Title = title ?? string.Empty;
            CreatedBy = createdBy ?? string.Empty;
            CreatedDate = createdDate;
            Password = password;
            UpdatedBy = updatedBy ?? string.Empty;
            UpdatedDate = updatedDate;
            UserRoles = Roles.GetRolesForUser(mu.UserName).ToList();
        }

        public MembershipUserExtended()
        {
            UserRoles = new List<string>();
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName{
            get { return FirstName + " " + LastName; }
        }
        public string Title { get; set; }
        public double? HourlyRate { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public List<string> UserRoles { get; set; }


        public MembershipUserExtended(string firstName, string lastName, string title, double? hourlyRate, string address, string city, string state, string zip, string phone, string createdBy, DateTime? createdDate, string updatedBy, DateTime? updatedDate)
        {
            FirstName = firstName ?? string.Empty;
            LastName = lastName ?? string.Empty;
            HourlyRate = hourlyRate;
            Phone = phone ?? string.Empty;
            Title = title ?? string.Empty;
            Address = address ?? string.Empty;
            City = city ?? string.Empty;
            State = state ?? string.Empty;
            Zip = zip ?? string.Empty;
            Title = title ?? string.Empty;
            CreatedBy = createdBy ?? string.Empty;
            CreatedDate = createdDate;
            UpdatedBy = updatedBy ?? string.Empty;
            UpdatedDate = updatedDate;
            UserRoles=new List<string>();
        }

        public MembershipUserExtended(MedicalUser legacyUser)
        {
            FirstName = legacyUser.Firstname ?? string.Empty;
            LastName = legacyUser.Lastname ?? string.Empty;
            double hRate;
            if (double.TryParse(legacyUser.Hourly_rate, out hRate))
                HourlyRate = hRate;
            Phone = legacyUser.Phonenumber ?? string.Empty;
            Title = string.Empty;
            Address = legacyUser.Address ?? string.Empty;
            City = string.Empty;
            State =string.Empty;
            Zip = string.Empty;
            UserRoles = new List<string>(){legacyUser.Role};
        }


        public static bool ValidateUser(string userName, string password)
        {
            return Membership.ValidateUser(userName, password);
        }

        /// <summary>
        /// Gets the extended membership profile user.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="userIsOnline"></param>
        /// <returns></returns>
        public static MembershipUserExtended GetUser(string username, bool userIsOnline)
        {
            using (var dbContext = new TimeTrackingEntities())
            {

                MembershipUser user = Membership.GetUser(username, userIsOnline);

                if (user != null)
                {
                    var upUser =
                        dbContext.ExtendedUserProfiles.FirstOrDefault(eu => eu.UserName == user.UserName);

                    if (upUser != null)
                        return new MembershipUserExtended(user, upUser.FirstName, upUser.LastName, upUser.Title,
                                                          upUser.HourlyRate, upUser.Address, upUser.City, upUser.State,
                                                          upUser.Zip, upUser.Phone, upUser.CreatedBy, upUser.CreatedDate, upUser.UpdatedBy, upUser.UpdatedDate,upUser.TempPassword);
                    return new MembershipUserExtended();

                }
                return new MembershipUserExtended();
            }
        }


        /// <summary>
        /// Creates the Membership user and update Extended Membership user.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="password">The password.</param>
        /// <param name="email">The email.</param>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastName">The last name.</param>
        /// <param name="title"></param>
        /// <param name="hourlyRate"></param>
        /// <param name="address"></param>
        /// <param name="city"></param>
        /// <param name="state"></param>
        /// <param name="zip"></param>
        /// <param name="phone"></param>
        /// <param name="createUserStatus"></param>
        
        /// <returns></returns>
        public static MembershipUserExtended CreateUser(string userName, string password, string email, string firstName, string lastName, string title, double? hourlyRate, string address, string city, string state, string zip, string phone, out MembershipCreateStatus createUserStatus)
        {
            if (password.Length < 6)
                password = Membership.GeneratePassword(6, 0);


            var user = Membership.CreateUser(userName, password, email, passwordQuestion: null, passwordAnswer: null, isApproved: true, providerUserKey: null, status: out createUserStatus);
            if (createUserStatus == MembershipCreateStatus.Success)
            {
                using (var dbContext = new TimeTrackingEntities())
                {
                    try
                    {
                        var extendedUserProfile = dbContext.ExtendedUserProfiles.FirstOrDefault(u => u.UserName == user.UserName);

                        if (extendedUserProfile == null)
                        {
                            extendedUserProfile = new ExtendedUserProfile
                                                      {
                                                          UserId = GetUserIdByUserName(user.UserName),
                                                          UserName = user.UserName,
                                                          FirstName = firstName,
                                                          LastName = lastName,
                                                          TempPassword = password,
                                                          Title = title,
                                                          HourlyRate = hourlyRate,
                                                          Address = address,
                                                          City = city,
                                                          State = state,
                                                          Zip = zip,
                                                          Phone = phone,
                                                          CreatedBy = HttpContext.Current.User == null || string.IsNullOrEmpty(HttpContext.Current.User.Identity.Name)
                                                                          ? "sys"
                                                                          : HttpContext.Current.User.Identity.Name,
                                                          CreatedDate = DateTime.Now
                                                      };
                            dbContext.ExtendedUserProfiles.Add(extendedUserProfile);
                            dbContext.SaveChanges();
                        }
                        else
                        {
                            if (user != null)
                                Update(user.UserName, firstName, lastName, title, hourlyRate, address, city, state, zip, phone);
                        }
                        return GetUser(userName, userIsOnline: false);
                    }
                    catch (Exception ex)
                    {
                        createUserStatus = MembershipCreateStatus.UserRejected;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Updates the specified membership and extented profile user.
        /// </summary>
        /// <param name="mUser">The m user.</param>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastName">The last name.</param>
        /// <param name="title"></param>
        /// <param name="hourlyRate"></param>
        /// <param name="address"></param>
        /// <param name="city"></param>
        /// <param name="state"></param>
        /// <param name="zip"></param>
        /// <param name="phone"></param>
        /// <returns></returns>
        /// 
        public static MembershipUserExtended Update(MembershipUser mUser, string firstName, string lastName, string title, double? hourlyRate, string address, string city, string state, string zip, string phone)
        {
            using (var dbContext = new TimeTrackingEntities())
            {
                Membership.UpdateUser(mUser);
                return Update(mUser.UserName, firstName, lastName, title, hourlyRate, address, city, state, zip, phone);
            }
        }
        public static MembershipUserExtended Update(string userName, string firstName, string lastName, string title, double? hourlyRate, string address, string city, string state, string zip, string phone)
        {
            using (var dbContext = new TimeTrackingEntities())
            {

                var extendedUser =
                    dbContext.ExtendedUserProfiles.FirstOrDefault(ur => ur.UserName == userName);

                if (extendedUser != null)
                {
                    extendedUser.FirstName = firstName;
                    extendedUser.LastName = lastName;
                    extendedUser.Title = title;
                    extendedUser.HourlyRate = hourlyRate;
                    extendedUser.Address = address;
                    extendedUser.City = city;
                    extendedUser.State = state;
                    extendedUser.Zip = zip;
                    extendedUser.Phone = phone;
                    extendedUser.CreatedBy = HttpContext.Current.User == null || string.IsNullOrEmpty(HttpContext.Current.User.Identity.Name)
                                                 ? "sys"
                                                 : HttpContext.Current.User.Identity.Name;
                    extendedUser.CreatedDate = DateTime.Now;
                    dbContext.SaveChanges();
                    return GetUser(userName, userIsOnline: false);
                }
                return null;
            }
        }

        public static Guid GetUserIdByUserName(string userName)
        {
            using (var dbContext = new TimeTrackingEntities())
            {
                var user = dbContext.aspnet_Users.FirstOrDefault(c => c.UserName.Equals(userName));

                if (user == null)
                    throw new Exception("user does not exists");

                return user.UserId;
            }
        }

        public static Dictionary<string,Guid> GetUserIdUserNameList()
        {
            using (var dbContext = new TimeTrackingEntities())
            {
                return (from user in dbContext.aspnet_Users
                        select new
                            {
                                uName = user.UserName,
                                uId = user.UserId
                            }).ToDictionary(c => c.uName, c => c.uId);
            }
        }
        [OutputCache(Duration = 2400, VaryByParam = "*")]
        public static Dictionary<string, string> GetFullNameUserNameList()
        {
            using (var dbContext = new TimeTrackingEntities())
            {
                return (from user in dbContext.ExtendedUserProfiles
                        select new
                        {
                            uName = user.UserName,
                            FullName = user.FirstName+" "+user.LastName
                        }).ToDictionary(c => c.FullName,c => c.uName);
            }
        }

        public static bool UserNameExists(string userName)
        {
            using (var dbContext = new TimeTrackingEntities())
            {
                return dbContext.aspnet_Users.Any(c => c.UserName.Equals(userName));
            }
        }

        public static MembershipUserCollection GetMembershipUserCollection()
        {
            return Membership.GetAllUsers();
        }
        public static List<ExtendedUserProfile> GetExtendedProfileUsersCollection()
        {
            using (var dbContext = new TimeTrackingEntities())
            {
                return dbContext.ExtendedUserProfiles.ToList();
            }
        }
        public static List<MembershipUserExtended> GetExtendedMembershipUserCollection()
        {
            var membershipUserCollection = GetMembershipUserCollection();
            var extendedProfileUserCollection = GetExtendedProfileUsersCollection();

            var extenderMembershipCollection = new List<MembershipUserExtended>();

            using (var dbContext = new TimeTrackingEntities())
            {
                extenderMembershipCollection.AddRange(
                    from object mu in membershipUserCollection 
                    let extenderUser = 
                    extendedProfileUserCollection.FirstOrDefault(c => c.UserName == ((MembershipUser) mu).UserName) 
                    where extenderUser != null
                    select new MembershipUserExtended((MembershipUser)mu, extenderUser.FirstName, extenderUser.LastName, extenderUser.Title, extenderUser.HourlyRate, extenderUser.Address, extenderUser.City, extenderUser.State, extenderUser.Zip, extenderUser.Phone, extenderUser.CreatedBy, extenderUser.CreatedDate, extenderUser.UpdatedBy, extenderUser.UpdatedDate, extenderUser.TempPassword)
                    );
                return extenderMembershipCollection;
            }
        }

        public static List<MembershipUserExtended> GetExtendedMembershipUserCollection(string searchTerm)
        {
            var userList = GetExtendedMembershipUserCollection();
            return userList.Where(c => c.FirstName.ToLower().Contains(searchTerm) || c.LastName.ToLower().Contains(searchTerm)).ToList();
        }


        #region IComparable<MembershipUserExtended> Members

        public int CompareTo(MembershipUserExtended userToCompare)
        {
            if (userToCompare == null)
                return 1;

            MembershipUserExtended otherTemperature = userToCompare;

            return this.UserName.CompareTo(otherTemperature);

        }

        #endregion
    }
}
