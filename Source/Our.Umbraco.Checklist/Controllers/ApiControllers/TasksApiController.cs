namespace Our.Umbraco.Checklist.Controllers.ApiControllers
{
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;

    using global::Umbraco.Web.Editors;

    using Our.Umbraco.Checklist.Attributes;
    using Our.Umbraco.Checklist.CacheRefresher;
    using Our.Umbraco.Checklist.Models;
    using Our.Umbraco.Checklist.Services;

    [CamelCase]
    public class TasksApiController : UmbracoAuthorizedJsonController
    {
        [HttpGet]
        public HttpResponseMessage Get(int contentNodeId)
        {
            return this.Request.CreateResponse(HttpStatusCode.OK, TaskService.Instance.GetByContentNodeId(contentNodeId));
        }

        [HttpPost]
        public HttpResponseMessage Post(Task task)
        {
            return this.UpdateTask(ref task) != null ?
                this.Request.CreateResponse(HttpStatusCode.OK, task) :
                this.Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Can't save task");
        }

        [HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            var task = TaskService.Instance.GetById(id);

            if (task != null)
            {
                task.IsDeleted = true;
                return this.UpdateTask(ref task) != null ?
                    this.Request.CreateResponse(HttpStatusCode.OK, task) :
                    this.Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Can't delete task");
            }

            return this.Request.CreateErrorResponse(HttpStatusCode.NotFound, "Can't find task");
        }

        private Task UpdateTask(ref Task task)
        {
            task = TaskService.Instance.Save(task);

            if (task != null)
            {
                ChecklistCacheRefresher.ClearCache();
            }

            return task;
        }
    }
}
