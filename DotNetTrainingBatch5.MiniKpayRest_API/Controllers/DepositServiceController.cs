using DotNetTrainingBatch5.MiniKpayRest_API.Features.Deposit;
using DotNetTrainingBatch5.MiniKpayRest_API.Features.User;
using DotNetTrainingBatch5.MiniKpayRest_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace DotNetTrainingBatch5.MiniKpayRest_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepositServiceController : ControllerBase
    {

        private readonly DepositService _service;

        public DepositServiceController()
        {
            _service = new DepositService();
        }

        [HttpGet]
        public IActionResult GetDeposits()
        {
            var lst = _service.GetDeposits();
            return Ok(lst);
        }

        [HttpGet("{phoneNo}")]
        public IActionResult GetDepositWithPhoneNo(string phoneNo)
        {
            var deposit = _service.GetDepoitWithPhone(phoneNo);
            return Ok(deposit);
        }

        [HttpPost]
        public IActionResult MakeDeposit(TblDeposit deposit)
        {
            var result = _service.MakeDeposit(deposit);
            if (result.StartsWith("Invalid"))
            {
                return NotFound(result);
            }
            return Ok(result);
        }
    }
}
