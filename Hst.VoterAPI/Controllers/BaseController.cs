using Hst.Logger;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Hst.VoterAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected readonly ILoggerManager logger = null;
        protected readonly IHttpContextAccessor context = null;
        protected readonly IConfiguration configuration = null;
        protected readonly int userid = 0;
        public BaseController(IHttpContextAccessor httpContextAccessor, ILoggerManager loggerManager, IConfiguration con)
        {
            logger = loggerManager;
            context = httpContextAccessor;
            configuration = con;

            var culture = configuration.GetSection("appSettings:Cuture").Value;
            if (!string.IsNullOrEmpty(culture))
                Thread.CurrentThread.CurrentCulture = new CultureInfo(culture);

            try
            {
                var identity = context.HttpContext.User.Identity as ClaimsIdentity;
                if (identity != null)
                {
                    Claim claim = identity.Claims.Where(c => c.Type.ToLower() == "userid").FirstOrDefault();
                    userid = claim != null ? Convert.ToInt32(claim.Value) : 0;
                }
            }
            catch (Exception ex)
            {
                logger.LogError("Error in baseconfiguration" + ex.Message);
            }

        }
    }
}
