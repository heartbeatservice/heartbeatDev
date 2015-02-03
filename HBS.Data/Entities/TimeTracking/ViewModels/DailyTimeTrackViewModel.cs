using HBS.Data.Entities.TimeTracking.Models;

namespace HBS.Data.Entities.TimeTracking.ViewModels
{
    public class DailyTimeTrackViewModel
    {
        public DailyTimeTrackViewModel()
        {
            DailyTimeTrack= new DailyTimeTrack();
            UserFullName = string.Empty;
        }

        public DailyTimeTrackViewModel(DailyTimeTrack dailyTimeTrack, string userFullName,string userName)
        {
            DailyTimeTrack = dailyTimeTrack;
            UserFullName = userFullName;
            UserName = userName;
        }

        public DailyTimeTrack DailyTimeTrack { get; set; }
        public string UserFullName { get; set; }
        public string UserName { get; set; }
        
    }
}
