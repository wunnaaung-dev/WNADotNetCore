using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WNADotNetCore.MiniKpay.Database.Models;
using WNADotNetCore.MiniKpay.Domain.Features.User;

namespace WNADotNetCore.MiniKpay.Domain.Features.Deposit
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

        public List<TblWithdraw> GetDepositWithPhone(string phoneNo)
        {
            var deposit = _db.TblWithdraws.AsNoTracking().Where(x => x.MobileNo == phoneNo).ToList();
            return deposit;
        }

        public string MakeDeposit(TblDeposit deposit, string pin)
        {
            var ownerInfo = _userService.GetUserByPhoneNumber(deposit.MobileNo);
            var isValidUser = ownerInfo is not null && ownerInfo.DeleteFlag == false;
            if (!isValidUser)
            {
                return "Invalid Account";
            }
            var checkPinResult = _userService.IsCorrectPin(ownerInfo!.MobileNo, pin);
            if (!checkPinResult)
            {
                return "Your Pin is incorrect";
            }
            _userService.IncreaseUserBalance(ownerInfo!.UserId, deposit.Balance);
            _db.TblDeposits.Add(deposit);
            _db.SaveChanges();
            return ($"Amount {deposit.Balance} is successfully added to your account");
        }
    }
}
