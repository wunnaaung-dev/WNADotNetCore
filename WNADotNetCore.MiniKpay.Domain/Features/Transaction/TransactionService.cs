using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WNADotNetCore.MiniKpay.Database.Models;
using WNADotNetCore.MiniKpay.Domain.Features.User;

namespace WNADotNetCore.MiniKpay.Domain.Features.Transaction
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
            if (senderInfo is null)
            {
                return ($"Sender Phone number does not exist");
            }
            if (receiverInfo is null)
            {
                return ($"Receiver Phone number does not exist");
            }
            _db.TblTransfers.Add(transfer);
            _db.SaveChanges();
            return "Success";
        }

        
    }
}
