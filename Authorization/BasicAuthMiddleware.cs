namespace rapid_news_media_auth_api.Authorization;

using System.Net.Http.Headers;
using System.Text;
using rapid_news_media_auth_api.Services;

public class BasicAuthMiddleware
{
    private readonly RequestDelegate _next;

    public BasicAuthMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context, IAuthService authService)
    {
        try
        {
            var authHeader = AuthenticationHeaderValue.Parse(context.Request.Headers["Authorization"]);

            //Basic Authentication
            if (authHeader.Scheme.Equals("Basic"))
            {
                var credentialBytes = Convert.FromBase64String(authHeader.Parameter);
                var credentials = Encoding.UTF8.GetString(credentialBytes).Split(':', 2);
                var username = credentials[0];
                var password = credentials[1];

                // authenticate credentials with user service and attach user to http context
                context.Items["User"] = await authService.Login(username, password);
            }
            //OAuth authorization with JWT token 
            else if(authHeader.Scheme.Equals("Bearer"))
            {
                var tokenJWT = authHeader.Parameter;
                

                // validate JWT token with user service and attach user to http context
                context.Items["User"] = await authService.Validate(tokenJWT);
            }

        }
        catch
        {
            // do nothing if invalid auth header
            // user is not attached to context so request won't have access to secure routes
        }

        await _next(context);
    }
}