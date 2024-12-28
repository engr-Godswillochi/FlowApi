using CashHub.Data;
using CashHub.DTO;
using CashHub.Models;
using Microsoft.AspNetCore.Mvc;

namespace CashHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthControllers : ControllerBase
    {
        
        private readonly CashHubContext _userContext;

        public AuthControllers(CashHubContext userContext)
        {
            _userContext = userContext;
        }
        
        
    }
}