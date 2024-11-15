using DotNetTrainingBatch5.MiniKpayRest_API.Features.User;
using DotNetTrainingBatch5.MiniKpayRest_API.Features.Withdraw;
using DotNetTrainingBatch5.MiniKpayRest_API.Models;
using Microsoft.EntityFrameworkCore;

namespace DotNetTrainingBatch5.MiniKpayRest_API.Features.Transaction
{
    public class TransactionService
    {
        private readonly AppDbContext _db = new AppDbContext();
        private readonly UserService _userService;

        public TransactionService()
        {
            _userService = new UserService();   
        }

        public List<TblTransfer> GetTransactions()
        {
            var model = _db.TblTransfers.AsNoTracking().ToList();
            return model;
        }

        public List<TblTransfer> GetTransactionsWithPhone(string phoneNo)
        {
            var transfers = _db.TblTransfers.AsNoTracking().Where(x => x.FromMobileNo == phoneNo || x.ToMobileNo == phoneNo).ToList();
            return transfers;
        }

        public string CreateTransactionRec(TblTransfer transfer)
        {
            var senderInfo = _userService.GetUserByPhoneNumber(transfer.FromMobileNo);
            var receiverInfo = _userService.GetUserByPhoneNumber(transfer.ToMobileNo);
            if (receiverInfo is null)
            {
                return ($"Phone number does not exist");
            }
            _db.TblTransfers.Add(transfer);
            _db.SaveChanges();
            return "Success";
        }

        public bool DeleteTransaction(int id)
        {
            var transaction = _db.TblTransfers.AsNoTracking().FirstOrDefault(x => x.TransferId == id);
            if (transaction is null)
            {
                return false;
            }
            transaction.DeleteFlag = true;
            _db.Entry(transaction).State = EntityState.Modified;
            int result = _db.SaveChanges();
            return result > 0;
        }
    }
}
