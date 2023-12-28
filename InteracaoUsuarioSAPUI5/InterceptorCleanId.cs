using System.Net;
using System.Web.Http;
using System.Web.Http.Filters;

namespace InteracaoUsuarioSAPUI5
{
    public class InterceptorCleanId : System.Web.Http.Filters.ActionFilterAttribute
    {
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            object id;
            if (actionExecutedContext.ActionContext.ActionArguments.TryGetValue("id", out id) &&
                actionExecutedContext.Request.Method == HttpMethod.Post)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
        }
    }
}
