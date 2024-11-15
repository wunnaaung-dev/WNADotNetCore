using System;
using System.Collections.Generic;

namespace DotNetTrainingBatch5.MiniKpayRest_API.Models;

public partial class TblWithdraw
{
    public int WithdrawId { get; set; }

    public string MobileNo { get; set; } = null!;

    public decimal Balance { get; set; }

    public bool DeleteFlag { get; set; }
}
