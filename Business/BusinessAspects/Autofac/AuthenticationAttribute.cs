using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Castle.DynamicProxy;
using Business.Constants;
using Core.Extensions;
using Core.Utilities.Results;
using System.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Core.Utilities.Security.JWT;

namespace Business.BusinessAspects.Autofac
{
    public class AuthenticationAttribute : MethodInterception
    {
        private IHttpContextAccessor _httpContextAccessor;
        private ITokenHelper _tokenHelper;
        public AuthenticationAttribute()
        {
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
            _tokenHelper = (ITokenHelper)_httpContextAccessor.HttpContext.RequestServices.GetService(typeof(ITokenHelper));
        }
        protected override void OnBefore(IInvocation invocation)
        {
            bool isAuthenticated = _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated;
            if (isAuthenticated) return;
            throw new AuthorizeException();
        }
    }
}
