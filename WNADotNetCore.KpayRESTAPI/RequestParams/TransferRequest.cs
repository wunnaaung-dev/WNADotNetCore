using WNADotNetCore.MiniKpay.Database.Models;

namespace WNADotNetCore.MiniKpay.API.RequestParams
{
    public class TransferRequest
    {
        public TblTransfer Transfer { get; set; }
        public string Pin { get; set; }
    }
}
