using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;

namespace API.Authorization;

public abstract class BaseAuthorizationHandler<T> : AuthorizationHandler<T> where T : IAuthorizationRequirement, new()
{
    private readonly IHttpContextAccessor httpContextAccessor;

    protected BaseAuthorizationHandler(IHttpContextAccessor httpContextAccessor)
    {
        this.httpContextAccessor = httpContextAccessor;
    }

    protected string? GetRequesterId(AuthorizationHandlerContext context)
    {
        return context.User.FindFirstValue(ClaimTypes.NameIdentifier)!;
    }

    /// <summary>
    ///     Gets a value with a specific key from either the HTTP request query or the request body.
    /// </summary>
    /// <param name="key">The named variable from the HTTP request whose value should be returned.</param>
    /// <returns>
    ///     A <see cref="Task" /> representing the asynchronous operation. The task value is a string,
    ///     the value of the variable requested.
    /// </returns>
    protected async Task<string?> GetValueFromRequest(string key)
    {
        return GetValueFromRequestString(key) ?? await GetValueFromRequestBody(key);
    }

    /// <summary>
    ///     Finds the value of a named variable from the request string.
    /// </summary>
    /// <param name="key">The named variable from the HTTP request whose value should be returned.</param>
    /// <returns>The value of the named variable in the request string.</returns>
    private string? GetValueFromRequestString(string key)
    {
        var value = httpContextAccessor.HttpContext?.Request.RouteValues
            .SingleOrDefault(x => x.Key == key).Value?.ToString();
        var fromQuery = httpContextAccessor.HttpContext?.Request.Query[key].ToString();
        return value ?? (fromQuery == "" ? null : fromQuery);
    }

    /// <summary>
    ///     Finds the value of a named variable from the request body.
    /// </summary>
    /// <param name="key">The named variable from the HTTP request whose value should be returned.</param>
    /// <returns>The value of the named variable in the request body.</returns>
    private async Task<string?> GetValueFromRequestBody(string key)
    {
        if (httpContextAccessor.HttpContext == null ||
            httpContextAccessor.HttpContext.Request.ContentType == null) return null;

        // if content is of type multipart/form-data (files were possibly uploaded)
        var contentType = httpContextAccessor.HttpContext.Request.ContentType.Split(" ")[0].TrimEnd(';');
        if (contentType == "multipart/form-data") return httpContextAccessor.HttpContext.Request.Form[key].ToString();

        // content must be json. read the body and reset the buffer index so the body can be read again in other functions
        var requestContent = await new StreamReader(httpContextAccessor.HttpContext.Request.Body).ReadToEndAsync();
        _ = httpContextAccessor.HttpContext.Request.Body.Seek(0, SeekOrigin.Begin);

        if (requestContent == null) return null;

        var json = JsonConvert.DeserializeObject<Dictionary<string, object>>(requestContent);
        return json?.GetValueOrDefault(key)?.ToString();
    }
}