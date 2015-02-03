using System.Collections.Generic;

namespace HBS.Data.Entities.TimeTracking.Raven
{
    public class MedicalUser
    {
        public string Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string User { get; set; }
        public string Pwd { get; set; }
        public string Role { get; set; }
        public string Lastlogin { get; set; }
        public string Hourly_rate { get; set; }
        public string Address { get; set; }
        public string Phonenumber { get; set; }
        public string Guid { get; set; }
        public bool IsUserMigrated{ get; set; }
        public bool UserHasDataToMigrate { get; set; }
        //public UserTimeStamps TodaysTimeStamp { get; set; }
        public List<UserTimeStampsHistory> UserHistoryList { get; set; }
    }
}