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
    public class WorkflowController : ApiController
    {
        IWorkflowRepository repository = new WorkflowRepository();

        public DataSourceResult Get(HttpRequestMessage requestMessage)
        {
            DataSourceRequest request = JsonConvert.DeserializeObject<DataSourceRequest>(requestMessage.RequestUri.ParseQueryString().GetKey(0));
            return repository.GetAllWorkflow().ToDataSourceResult(request.Take, request.Skip, request.Sort, request.Filter);
        }

        public HttpResponseMessage Post(Workflow workflow)
        {
            // set default value for user id
            if (workflow.OwnerID == 0) workflow.OwnerID = 1;
            if (workflow.WorkerID == 0) workflow.WorkerID = 1;

            if (repository.AddWorkflow(workflow))
            {
                DataSourceResult result = new DataSourceResult
                {
                    Data = new[] { workflow },
                    Total = 1
                };

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, result);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = workflow.WorkflowID }));
                return response;
            }
            else
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
        }

        public HttpResponseMessage Put(Workflow workflow)
        {
            if (repository.EditWorkflow(workflow))
            {
                DataSourceResult result = new DataSourceResult
                {
                    Data = new[] { workflow },
                    Total = 1
                };

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, result);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = workflow.WorkflowID }));
                return response;
            }
            else
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
        }

        public HttpResponseMessage Delete(Workflow workflow)
        {
            if (repository.DeleteWorkflow(workflow.WorkflowID))
            {
                DataSourceResult result = new DataSourceResult
                {
                    Data = new[] { workflow },
                    Total = 1
                };

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, result);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = workflow.WorkflowID }));
                return response;
            }
            else
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
        }

        [AcceptVerbs("OPTIONS")]
        public HttpResponseMessage Options()
        {
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}
