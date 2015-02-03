using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HBS.Entities.Utilities;

namespace HBS.Entities
{
    class Menu
    {
        public int MenuId { get; set; }
        public int CompanyId { get; set; }
        public string Title { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string OrderNumber { get; set; }

        public Menu()
        {
        }

        public Menu(IDataReader dbReader)
            : this()
        {
            if (dbReader.HasColumn("MenuId") && dbReader["MenuId"] != DBNull.Value)
                MenuId = (int)dbReader["MenuId"];
            
            if (dbReader.HasColumn("CompanyId") && dbReader["CompanyId"] != DBNull.Value)
                CompanyId = (int)dbReader["CompanyId"];
            
            if (dbReader.HasColumn("Title") && dbReader["Title"] != DBNull.Value)
                Title = (string)dbReader["Title"];
            
            if (dbReader.HasColumn("Controller") && dbReader["Controller"] != DBNull.Value)
                Controller = (string)dbReader["Controller"];
            
            if (dbReader.HasColumn("Action") && dbReader["Action"] != DBNull.Value)
                Action = (string)dbReader["Action"];
            
            if (dbReader.HasColumn("OrderNumber") && dbReader["OrderNumber"] != DBNull.Value)
                Controller = (string)dbReader["OrderNumber"];
          
        }

    }
}
