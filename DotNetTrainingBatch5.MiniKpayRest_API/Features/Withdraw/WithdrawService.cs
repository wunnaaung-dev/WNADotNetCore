using DotNetTrainingBatch5.MiniKpayRest_API.Features.User;
using DotNetTrainingBatch5.MiniKpayRest_API.Models;
using Microsoft.EntityFrameworkCore;

namespace DotNetTrainingBatch5.MiniKpayRest_API.Features.Withdraw
{
    public class WithdrawService
    {
        private readonly AppDbContext _db = new AppDbContext();
        private readonly UserService _userService;

        public WithdrawService()
        {
            _userService = new UserService();
        }

        public List<TblWithdraw> GetWithdraws()
        {
            var model = _db.TblWithdraws.AsNoTracking().ToList();
            return model;
        }

        public List<TblWithdraw> GetWithdrawWithPhone(string phoneNo)
        {
            var withdraw = _db.TblWithdraws.AsNoTracking().Where(x => x.MobileNo == phoneNo).ToList();
            return withdraw;
        }

        public string MakeWithdraw(TblWithdraw withdraw)
        {
            var ownerInfo = _userService.GetUserByPhoneNumber(withdraw.MobileNo);
            var isValidUser = ownerInfo is not null && ownerInfo.DeleteFlag == false;
            if (!isValidUser)
            {
                return "Invalid Account";
            }
            _userService.DecreaseUserBalance(ownerInfo!.UserId, withdraw.Balance);
            _db.TblWithdraws.Add(withdraw);
            _db.SaveChanges();
            return ($"Amount {withdraw.Balance} is successfully removed your account");
        }
    }
}
