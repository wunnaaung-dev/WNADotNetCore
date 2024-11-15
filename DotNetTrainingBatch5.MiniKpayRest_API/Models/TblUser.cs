using System;
using System.Collections.Generic;

namespace DotNetTrainingBatch5.MiniKpayRest_API.Models;

public partial class TblUser
{
    public int UserId { get; set; }

    public string Name { get; set; } = null!;

    public string MobileNo { get; set; } = null!;

    public decimal Balance { get; set; }

    public string Pin { get; set; } = null!;

    public bool DeleteFlag { get; set; }
}
