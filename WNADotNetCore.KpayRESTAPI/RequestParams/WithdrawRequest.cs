using WNADotNetCore.MiniKpay.Database.Models;

namespace WNADotNetCore.MiniKpay.API.RequestParams
{
    public class WithdrawRequest
    {
        public TblWithdraw Withdraw { get; set; }
        public string Pin { get; set; }
    }
}
