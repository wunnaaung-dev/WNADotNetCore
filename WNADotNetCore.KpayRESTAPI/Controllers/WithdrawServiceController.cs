using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WNADotNetCore.MiniKpay.API.RequestParams;
using WNADotNetCore.MiniKpay.Domain.Features.User;
using WNADotNetCore.MiniKpay.Domain.Features.Withdraw;

namespace WNADotNetCore.MiniKpay.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WithdrawServiceController : ControllerBase
    {
        private readonly WithdrawService _withdrawService;
        private readonly UserService _userService;

        public WithdrawServiceController()
        {
            _withdrawService = new WithdrawService();
            _userService = new UserService();
        }

        [HttpGet]
        public IActionResult GetWithdraw()
        {
            var lst = _withdrawService.GetWithdraws();
            return Ok(lst);
        }

        [HttpGet("{phoneNo}")]
        public IActionResult GetWithdrawWithPhoneNo(string phoneNo)
        {
            var isPhoneNoExist = _userService.GetUserByPhoneNumber(phoneNo);
            if (isPhoneNoExist is null)
            {
                return NotFound("Phone No is not found");
            }
            var withdraw = _withdrawService.GetWithdrawWithPhone(phoneNo);
            return Ok(withdraw);
        }

        [HttpPost]
        public IActionResult MakeWithdraw(WithdrawRequest withdrawReq)
        {
            var result = _withdrawService.MakeWithdraw(withdrawReq.Withdraw, withdrawReq.Pin);
            
            if (result.StartsWith("Invalid"))
            {
                return NotFound(result);
            }

            if (result.StartsWith("Your Pin"))
            {
                return NotFound(result);
            }

            if (result.StartsWith("Insufficient"))
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}

