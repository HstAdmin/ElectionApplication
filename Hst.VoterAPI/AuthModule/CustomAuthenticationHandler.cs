using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Diagnostics.Contracts;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Threading;

namespace Hst.VoterAPI.AuthModule
{

    public class CustomAuthentication : Attribute, IAuthorizationFilter
    {
        public CustomAuthentication()
        {
        }
        public void OnAuthorization(AuthorizationFilterContext filterContext)
        {
            if (filterContext != null)
            {


                if (SkipAuthorization(filterContext))
                    return;


                if (IsUserAuthorized(filterContext))
                {
                    filterContext.HttpContext.Response.Headers.Add("AuthStatus", "Authorized");
                    filterContext.HttpContext.Response.Headers.Add("storeAccessiblity", "Authorized");
                    return;
                }
                else
                {
                    filterContext.HttpContext.Response.Headers.Add("AuthStatus", "NotAuthorized");

                    filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                    filterContext.HttpContext.Response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = "Not Authorized";
                    filterContext.Result = new JsonResult("NotAuthorized")
                    {
                        Value = new
                        {
                            Status = "Error",
                            Message = "Invalid Token"
                        },
                    };
                }


            }
        }

        private bool SkipAuthorization(AuthorizationFilterContext actionContext)
        {
            Contract.Assert(actionContext != null);

            return actionContext.ActionDescriptor.FilterDescriptors.Any(em => em.GetType() == typeof(AllowAnonymousAttribute));
        }

        public bool IsUserAuthorized(AuthorizationFilterContext actionContext)
        {
            Microsoft.Extensions.Primitives.StringValues header;
            actionContext.HttpContext.Request.Headers.TryGetValue("Authorization", out header);
            var authHeader = header.FirstOrDefault();
            if (!string.IsNullOrEmpty(authHeader))
            {
                authHeader = authHeader.Replace("bearer", "").Replace("Bearer", "").Replace(" ", "");
                JwtSecurityToken userPayloadToken = AuthManager.GenerateUserClaimFromJWT(authHeader);
                if (userPayloadToken != null)
                {
                    var identity = AuthManager.PopulateUserIdentity(userPayloadToken);
                    string[] roles = { "All" };
                    var genericPrincipal = new GenericPrincipal(identity, roles);
                    Thread.CurrentPrincipal = genericPrincipal;
                    var authenticationIdentity = Thread.CurrentPrincipal.Identity as JWTAuthenticationIdentity;
                    if (authenticationIdentity != null && !String.IsNullOrEmpty(authenticationIdentity.UserName))
                    {
                        authenticationIdentity.UserName = identity.UserName;
                        authenticationIdentity.UserId = identity.UserId;
                        authenticationIdentity.Email = identity.Email;
                    }
                    return true;
                }


            }
            return false;


        }

    }

}
