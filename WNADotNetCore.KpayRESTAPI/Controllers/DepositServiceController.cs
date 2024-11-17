using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WNADotNetCore.MiniKpay.API.RequestParams;
using WNADotNetCore.MiniKpay.Database.Models;
using WNADotNetCore.MiniKpay.Domain.Features.Deposit;
using WNADotNetCore.MiniKpay.Domain.Features.User;

namespace WNADotNetCore.MiniKpay.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepositServiceController : ControllerBase
    {
        private readonly DepositService _depositService;
        private readonly UserService _userService;

        public DepositServiceController()
        {
            _depositService = new DepositService();
            _userService = new UserService();
        }

        [HttpGet]
        public IActionResult GetDeposits()
        {
            var lst = _depositService.GetDeposits();
            return Ok(lst);
        }

        [HttpGet("{phoneNo}")]
        public IActionResult GetDepositWithPhoneNo(string phoneNo)
        {
            var isPhoneNoExist = _userService.GetUserByPhoneNumber(phoneNo);
            if (isPhoneNoExist is null)
            {
                return NotFound("Phone No Does Not Exist");
            }
            var deposit = _depositService.GetDepositWithPhone(phoneNo);
            return Ok(deposit);
        }

        [HttpPost]
        public IActionResult MakeDeposit([FromBody] DepositRequest depositReq)
        {
            var result = _depositService.MakeDeposit(depositReq.Deposit , depositReq.Pin);

            if (result.StartsWith("Invalid"))
            {
                return NotFound(result);
            }
            if (result.StartsWith("Your Pin"))
            {
                return BadRequest(result);
            }
            
            return Ok(result);
        }
    }
}
