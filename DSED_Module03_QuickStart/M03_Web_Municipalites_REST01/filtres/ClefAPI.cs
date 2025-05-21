using Azure.Core;
using M01_Entite;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;

namespace M03_Web_Municipalites_REST01;

public class ClefAPIAttribute : TypeFilterAttribute
{
    public ClefAPIAttribute() : base(typeof(ClefAPIFilter))
    {
    }

    private class ClefAPIFilter : IAsyncActionFilter
    {
        private readonly IDepotClefAPI _depotClefAPI;

        public ClefAPIFilter(IDepotClefAPI depotClefAPI)
        {
            _depotClefAPI = depotClefAPI;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.HttpContext.Request.Headers.TryGetValue("clefAPI", out var clefAPI))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var clefValide = _depotClefAPI.ChercherClefAPI()?.CleApIfId.ToString();
            
            if (clefValide == null || !clefValide.Equals(clefAPI))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            await next();
        }
    }
}