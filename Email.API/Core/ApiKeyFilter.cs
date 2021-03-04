using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using System;
using System.Linq;

namespace Email.API.Core
{
    public class ApiKeyAuthorizeAttribute : TypeFilterAttribute
    {
        public ApiKeyAuthorizeAttribute() : base(typeof(ApiKeyAuthorization))
        {
        }
    }

    public class ApiKeyAuthorization : IAuthorizationFilter
    {
        private readonly IAutenticacao _autenticacao;
        public ApiKeyAuthorization(IAutenticacao autenticacao)
        {
            _autenticacao = autenticacao;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            StringValues apiKey;
            context.HttpContext.Request.Headers.TryGetValue("Authorization", out apiKey);
            var value = apiKey.ToString().Split(" ", StringSplitOptions.RemoveEmptyEntries);
            if (!value.Contains("ApiKey"))
            {
                context.Result = new StatusCodeResult(401);
                return;
            }
            if (!_autenticacao.Autenticar(value[1]))
            {
                context.Result = new StatusCodeResult(401);
                return;
            }
        }
    }
}
