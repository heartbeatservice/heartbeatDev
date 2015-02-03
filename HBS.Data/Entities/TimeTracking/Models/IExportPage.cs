namespace HBS.Data.Entities.TimeTracking.Models
{
    public interface IExportPage
    {
        void ExportExcel(string data, string fileName);
    }
}
