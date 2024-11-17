using System;
using System.Collections.Generic;

namespace WNADotNetCore.MiniKpay.Database.Models;

public partial class TblWithdraw
{
    public int WithdrawId { get; set; }

    public string MobileNo { get; set; } = null!;

    public decimal Balance { get; set; }

    public bool DeleteFlag { get; set; }
}
