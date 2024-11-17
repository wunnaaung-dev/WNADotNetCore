using System;
using System.Collections.Generic;

namespace WNADotNetCore.MiniKpay.Database.Models;

public partial class TblTransfer
{
    public int TransferId { get; set; }

    public string FromMobileNo { get; set; } = null!;

    public string ToMobileNo { get; set; } = null!;

    public decimal Amount { get; set; }

    public bool DeleteFlag { get; set; }
}
