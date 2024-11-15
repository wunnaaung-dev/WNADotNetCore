using DotNetTrainingBatch5.MiniKpayRest_API.Features.Deposit;
using DotNetTrainingBatch5.MiniKpayRest_API.Features.Withdraw;
using DotNetTrainingBatch5.MiniKpayRest_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace DotNetTrainingBatch5.MiniKpayRest_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WithdrawServiceController : ControllerBase
    {
        private readonly WithdrawService _service;

        public WithdrawServiceController()
        {
            _service = new WithdrawService();
        }

        [HttpGet]
        public IActionResult GetWithdraw()
        {
            var lst = _service.GetWithdraws();
            return Ok(lst);
        }

        [HttpGet("{phoneNo}")]
        public IActionResult GetWithdrawWithPhoneNo(string phoneNo)
        {
            var withdraw = _service.GetWithdrawWithPhone(phoneNo);
            if (withdraw is null)
            {
                return NotFound("Ma twae buu ha");
            }
            return Ok(withdraw);
        }

        [HttpPost]
        public IActionResult MakeWithdraw(TblWithdraw withdraw)
        {
            var result = _service.MakeWithdraw(withdraw);
            if (result.StartsWith("Invalid"))
            {
                return NotFound(result);
            }
            return Ok(result);
        }
    }
}
