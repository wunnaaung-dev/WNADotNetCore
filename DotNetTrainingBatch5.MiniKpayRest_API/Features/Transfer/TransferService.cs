using DotNetTrainingBatch5.MiniKpayRest_API.Features.Transaction;
using DotNetTrainingBatch5.MiniKpayRest_API.Features.User;
using DotNetTrainingBatch5.MiniKpayRest_API.Models;

namespace DotNetTrainingBatch5.MiniKpayRest_API.Features.Transfer
{
    public class TransferService
    {
        private readonly AppDbContext _db = new AppDbContext();
        private readonly UserService _userService;
        private readonly TransactionService _transactionSercive;

        public TransferService()
        {
            _userService = new UserService();
            _transactionSercive = new TransactionService();
        }

        public string MakeTransfer(string senderPhoneNo, string receiverPhoneNo, decimal balance)
        {
            var senderInfo = _userService.GetUserByPhoneNumber(senderPhoneNo);
            var receiverInfo = _userService.GetUserByPhoneNumber(receiverPhoneNo);
            var isValidUser = receiverInfo is not null && receiverInfo.DeleteFlag == false;
            if (!isValidUser)
            {
                return ($"Account Not Found");
            }
            _userService.DecreaseUserBalance(senderInfo!.UserId, balance);
            _userService.IncreaseUserBalance(receiverInfo!.UserId, balance);
            var transfer = new TblTransfer()
            {
                FromMobileNo = senderInfo!.MobileNo,
                ToMobileNo = receiverInfo!.MobileNo,
                Amount = balance,
            };
            _transactionSercive.CreateTransactionRec(transfer);
            return ($"Amount {balance} is successfully transferred to {receiverInfo.Name}");
        }
    }
}
