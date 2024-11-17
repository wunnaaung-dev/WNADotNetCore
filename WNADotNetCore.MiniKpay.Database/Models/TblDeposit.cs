using System;
using System.Collections.Generic;

namespace WNADotNetCore.MiniKpay.Database.Models;

public partial class TblDeposit
{
    public int DepositId { get; set; }

    public string MobileNo { get; set; } = null!;

    public decimal Balance { get; set; }

    public bool DeleteFlag { get; set; }
}
