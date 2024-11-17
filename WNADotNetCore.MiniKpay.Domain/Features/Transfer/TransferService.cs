using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WNADotNetCore.MiniKpay.Database.Models;
using WNADotNetCore.MiniKpay.Domain.Features.Deposit;
using WNADotNetCore.MiniKpay.Domain.Features.Transaction;
using WNADotNetCore.MiniKpay.Domain.Features.User;
using WNADotNetCore.MiniKpay.Domain.Features.Withdraw;

namespace WNADotNetCore.MiniKpay.Domain.Features.Transfer
{
    public class TransferService
    {
        private readonly AppDbContext _db = new AppDbContext();
        private readonly UserService _userService;
        private readonly DepositService _depositService;
        private readonly WithdrawService _withdrawService;
        private readonly TransactionService _transactionSercive;

        public TransferService()
        {
            _userService = new UserService();
            _depositService = new DepositService();
            _withdrawService = new WithdrawService();
            _transactionSercive = new TransactionService();
        }

        public string MakeTransfer(TblTransfer transfer, string pin)
        {
            var senderInfo = _userService.GetUserByPhoneNumber(transfer.FromMobileNo);
            var receiverInfo = _userService.GetUserByPhoneNumber(transfer.ToMobileNo);

            if (senderInfo is null)
            {
                return "Sender Info is Invalid";
            }

            if (receiverInfo is null)
            {
                return "Receiver Info is Invalid";
            }
            var withdraw = new TblWithdraw()
            {
                MobileNo = senderInfo.MobileNo,
                Balance = transfer.Amount,
            };
            var withdrawResult = _withdrawService.MakeWithdraw(withdraw, pin);

            _userService.IncreaseUserBalance(receiverInfo.UserId, transfer.Amount);

            var transaction = new TblTransfer()
            {
                FromMobileNo = senderInfo!.MobileNo,
                ToMobileNo = receiverInfo!.MobileNo,
                Amount = transfer.Amount
            };
            _transactionSercive.CreateTransactionRec(transaction);
            return withdrawResult;
            
        }
    }
}
