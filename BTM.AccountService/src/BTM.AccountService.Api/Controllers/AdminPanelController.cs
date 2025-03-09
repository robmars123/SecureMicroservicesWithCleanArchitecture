using BTM.Account.Shared.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BTM.AccountService.Api.Controllers
{
    [Route("api/AdminPanel")]
    [ApiController]
    [Authorize]
    public class AdminPanelController : ControllerBase
    {
        [HttpGet]
        [Authorize(Roles = GlobalConstants.Roles.Admin)]
        public string Get()
        {
            return "Result from API";
        }
        
    }
}
