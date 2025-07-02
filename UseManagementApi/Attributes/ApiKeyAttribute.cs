using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace UseManagementApi.Attributes;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]

public class ApiKeyAttribute : Attribute, IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(
        ActionExecutingContext context,
        ActionExecutionDelegate next)
    
    {
        var configuration = context.HttpContext.RequestServices.GetService<IConfiguration>();

        var apiKeyName = configuration.GetValue<string>("ApiKeySettings:Name");
        var expectedApiKey = configuration.GetValue<string>("ApiKeySettings:Key");
        
        if (!context.HttpContext.Request.Query.TryGetValue(apiKeyName, out var extractedApiKey))
        {
            context.Result = new ContentResult()
            {
                StatusCode = 401,
                Content = "ApiKey não encontrada..."
            };
            return;
        }

        if (!expectedApiKey.Equals(extractedApiKey))
        {
            context.Result = new ContentResult()
            {
                StatusCode = 403,
                Content = "Acesso não autorizado..."
            };
            return;
        }
        await next();
    }
    
}