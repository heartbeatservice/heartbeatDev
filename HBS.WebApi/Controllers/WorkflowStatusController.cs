using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HBS.Data.Abstract;
using HBS.Data.Concrete;
using HBS.Entities;

namespace HBS.WebApi.Controllers
{
    public class WorkflowStatusController : ApiController
    {
        public IWorkflowStatusRepository workflowStatusEntity = new WorkflowStatusRepository();

        public IEnumerable<KendoDDL> Get(HttpRequestMessage requestMessage)
        {
            return workflowStatusEntity.GetAllWorkflowStatus().ToList();
        }
    }
}
