using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HBS.Entities;

namespace HBS.Data.Abstract
{
    public interface IWorkflowRepository
    {
        IQueryable<Workflow> GetAllWorkflow();
        bool AddWorkflow(Workflow toInsert);
        bool EditWorkflow(Workflow toUpdate);
        bool DeleteWorkflow(int id);
    }
}
