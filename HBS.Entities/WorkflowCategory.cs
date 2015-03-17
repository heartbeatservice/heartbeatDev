using System;
using System.Data;
using HBS.Entities.Utilities;

namespace HBS.Entities
{
    public class WorkflowCategory
    {
        public int WorkflowCategoryID { get; set; }
        public string CategoryName { get; set; }

        public WorkflowCategory()
        {

        }

        public WorkflowCategory(int workflowCategoryID, string categoryName)
            : this()
        {
            WorkflowCategoryID = workflowCategoryID;
            CategoryName = categoryName;
        }

        public WorkflowCategory(IDataReader dbReader)
            : this()
        {
            if (dbReader["WorkflowCategoryID"] != DBNull.Value) WorkflowCategoryID = (int)dbReader["WorkflowCategoryID"];
            if (dbReader["CategoryName"] != DBNull.Value) CategoryName = (string)dbReader["CategoryName"];
        }
    }
}
