using DotNetTrainingBatch5.MiniKpayRest_API.Features.Deposit;
using DotNetTrainingBatch5.MiniKpayRest_API.Features.Transaction;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotNetTrainingBatch5.MiniKpayRest_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionRecordController : ControllerBase
    {
        private readonly TransactionService _service;

        public TransactionRecordController()
        {
            _service = new TransactionService();
        }

        [HttpGet]
        public IActionResult GetTransactions()
        {
            var lst = _service.GetTransactions();
            return Ok(lst);
        }

        [HttpGet("{phoneNo}")]
        public IActionResult GetDepositWithPhoneNo(string phoneNo)
        {
            var deposit = _service.GetTransactionsWithPhone(phoneNo);
            if (deposit is null)
            {
                return NotFound("No transaction records with this phone number");
            }
            return Ok(deposit);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTransaction(int id)
        {
            _service.DeleteTransaction(id);
            return Ok("Deleted successfully");
        }

    }
}
