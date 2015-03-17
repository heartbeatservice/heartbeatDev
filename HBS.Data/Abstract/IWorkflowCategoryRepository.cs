using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HBS.Entities;

namespace HBS.Data.Abstract
{
    public interface IWorkflowCategoryRepository
    {
        IQueryable<WorkflowCategory> GetWorkflowCategory();
        int AddWorkflowCategory(WorkflowCategory toInsert);
        bool EditWorkflowCategory(WorkflowCategory toUpdate);
        bool DeleteWorkflowCategory(int id);
    }
}
