using DotNetTrainingBatch5.MiniKpayRest_API.Features.User;
using DotNetTrainingBatch5.MiniKpayRest_API.Models;
using Microsoft.EntityFrameworkCore;

namespace DotNetTrainingBatch5.MiniKpayRest_API.Features.Deposit
{
    public class DepositService
    {
        private readonly AppDbContext _db = new AppDbContext();
        private readonly UserService _userService;

        public DepositService()
        {
            _userService = new UserService();
        }

        public List<TblDeposit> GetDeposits()
        {
            var model = _db.TblDeposits.AsNoTracking().ToList();
            return model;
        }

        public List<TblWithdraw> GetDepoitWithPhone(string phoneNo)
        {
            var withdraw = _db.TblWithdraws.AsNoTracking().Where(x => x.MobileNo == phoneNo).ToList();
            return withdraw;
        }

        public string MakeDeposit(TblDeposit deposit)
        {
            var ownerInfo = _userService.GetUserByPhoneNumber(deposit.MobileNo);
            var isValidUser = ownerInfo is not null && ownerInfo.DeleteFlag == false;
            if (!isValidUser)
            {
                return "Invalid Account";
            }
            _userService.IncreaseUserBalance(ownerInfo!.UserId, deposit.Balance);
            _db.TblDeposits.Add(deposit);
            _db.SaveChanges();
            return ($"Amount {deposit.Balance} is successfully added to your account");
        }
       
    }
}
