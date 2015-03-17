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
    public class WorkflowCategoryController : ApiController
    {
        private IWorkflowCategoryRepository _repository = new WorkflowCategoryRepository();

        public DataSourceResult Get(HttpRequestMessage requestMessage)
        {
            //DataSourceRequest request = JsonConvert.DeserializeObject<DataSourceRequest>(requestMessage.RequestUri.ParseQueryString().GetKey(0));

            return _repository.GetWorkflowCategory().ToDataSourceResult(); //.ToDataSourceResult(request.Take, request.Skip, request.Sort, request.Filter);
        }

        public HttpResponseMessage Post(WorkflowCategory workflowCategory)
        {
            int workflowCategoryID = _repository.AddWorkflowCategory(workflowCategory);

            if (workflowCategoryID > 0)
            {
                workflowCategory.WorkflowCategoryID = workflowCategoryID;

                DataSourceResult result = new DataSourceResult
                {
                    Data = new[] { workflowCategory },
                    Total = 1
                };

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, result);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = workflowCategory.WorkflowCategoryID }));
                return response;
            }
            else
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
        }

        public HttpResponseMessage Put(WorkflowCategory workflowCategory)
        {
            if (_repository.EditWorkflowCategory(workflowCategory))
            {
                DataSourceResult result = new DataSourceResult
                {
                    Data = new[] { workflowCategory },
                    Total = 1
                };

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, result);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = workflowCategory.WorkflowCategoryID }));
                return response;
            }
            else
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
        }

        public HttpResponseMessage Delete(WorkflowCategory workflowCategory)
        {
            if (_repository.DeleteWorkflowCategory(workflowCategory.WorkflowCategoryID))
            {
                DataSourceResult result = new DataSourceResult
                {
                    Data = new[] { workflowCategory },
                    Total = 1
                };

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, result);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = workflowCategory.WorkflowCategoryID }));
                return response;
            }
            else
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
        }

        [AcceptVerbs("OPTIONS")]
        public HttpResponseMessage Options()
        {
            var resp = new HttpResponseMessage(HttpStatusCode.OK);

            return resp;
        }
    }
}
