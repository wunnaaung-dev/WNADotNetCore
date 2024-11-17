using WNADotNetCore.MiniKpay.Database.Models;

namespace WNADotNetCore.MiniKpay.API.RequestParams
{
       public class DepositRequest
        {
            public TblDeposit Deposit { get; set; }
            public string Pin { get; set; }
        }
    
}
