using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HBS.Data.Abstract;
using HBS.Data.Concrete;
using HBS.Data.Utilities;
using HBS.Entities;
using Newtonsoft.Json;
using System.Web;

namespace HBS.WebApi.Controllers
{
    public class WorkflowHistoryController : ApiController
    {
        IWorkflowRepository repository = new WorkflowRepository();

        public bool Post([FromBody]WorkflowHistory workflowHistory)
        {
            WorkflowHistory test = workflowHistory;
            return false;
        }
    }
}
