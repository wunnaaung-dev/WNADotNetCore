using DotNetTrainingBatch5.Database.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WNADotNetCore.MiniKpay.API.RequestParams;
using WNADotNetCore.MiniKpay.Domain.Features.Transfer;

namespace WNADotNetCore.MiniKpay.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransferServiceController : ControllerBase
    {
        private readonly TransferService _service;

        public TransferServiceController(TransferService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult MakeTransfer(TransferRequest transferReq)
        {
            var result = _service.MakeTransfer(transferReq.Transfer,transferReq.Pin);
            
            if (result.StartsWith("Sender"))
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
