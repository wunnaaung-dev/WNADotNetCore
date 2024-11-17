using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using WNADotNetCore.MiniKpay.Database.Models;
using WNADotNetCore.MiniKpay.Domain.Features.User;

namespace WNADotNetCore.MiniKpay.Domain.Features.Withdraw
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

        public string MakeWithdraw(TblWithdraw withdraw, string pin)
        {
            #region Check Owner Info
            var ownerInfo = _userService.GetUserByPhoneNumber(withdraw.MobileNo);
            var isValidUser = ownerInfo is not null && ownerInfo.DeleteFlag == false;
            if (!isValidUser)
            {
                return "Invalid Account";
            }
            #endregion

            #region Pin Check
            var checkPinResult = _userService.IsCorrectPin(ownerInfo!.MobileNo, pin);
            if (!checkPinResult)
            {
                return "Your Pin is incorrect";
            }
            #endregion

            #region BalanceCheck
            var balanceCheck = _userService.IsBalanceSufficient(ownerInfo.Balance, withdraw.Balance);
            if (!balanceCheck)
            {
                return "Insufficient Balance";
            }
            #endregion

            #region Minus Balance from Owner
            _userService.DecreaseUserBalance(ownerInfo!.UserId, withdraw.Balance);
            #endregion

            _db.TblWithdraws.Add(withdraw);
            _db.SaveChanges();
            return ($"Amount {withdraw.Balance} is successfully removed your account");
        }
    }
}
