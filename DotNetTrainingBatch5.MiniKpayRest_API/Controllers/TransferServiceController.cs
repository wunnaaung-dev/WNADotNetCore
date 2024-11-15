using DotNetTrainingBatch5.MiniKpayRest_API.Features.Deposit;
using DotNetTrainingBatch5.MiniKpayRest_API.Features.Transfer;
using DotNetTrainingBatch5.MiniKpayRest_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotNetTrainingBatch5.MiniKpayRest_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransferServiceController : ControllerBase
    {
        private readonly TransferService _service;

        public TransferServiceController()
        {
            _service = new TransferService();
        }

        [HttpPost]
        public IActionResult MakeTransfer(TblTransfer transfer)
        {
            var result = _service.MakeTransfer(transfer.FromMobileNo, transfer.ToMobileNo, transfer.Amount);
            if (result.StartsWith("Phone"))
            {
                return NotFound(result);
            }
            return Ok(result);
        }
    }
}
