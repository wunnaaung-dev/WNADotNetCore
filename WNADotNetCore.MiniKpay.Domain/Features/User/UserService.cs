using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WNADotNetCore.MiniKpay.Database.Models;

namespace WNADotNetCore.MiniKpay.Domain.Features.User
{
    public class UserService
    {
        private readonly AppDbContext _db = new AppDbContext();

        public List<TblUser> GetUsers()
        {
            var model = _db.TblUsers.AsNoTracking().ToList();
            return model;
        }

        public TblUser GetUser(int id)
        {
            var person = _db.TblUsers.AsNoTracking().FirstOrDefault(x => x.UserId == id);
            return person;
        }

        public TblUser CreateUser(TblUser user)
        {
            _db.TblUsers.Add(user);
            _db.SaveChanges();
            return user;
        }

        public bool IsCorrectPin(string phoneNo, string pin)
        {
            var person = _db.TblUsers.AsNoTracking().FirstOrDefault(x => x.MobileNo == phoneNo);
            return person!.Pin.Equals(pin) ? true : false;
        }

        public bool IsValidUser(int id)
        {
            var person = _db.TblUsers.AsNoTracking().FirstOrDefault(x => x.UserId == id);
            return person is not null ? true : false;
        }

        public TblUser UpdateUserPin(int id, string pin)
        {
            var person = _db.TblUsers.AsNoTracking().FirstOrDefault(x => x.UserId == id);
            
            person!.Pin = pin;

            _db.Entry(person).State = EntityState.Modified;
            _db.SaveChanges();
            return person;
        }

        public bool IsBalanceSufficient(decimal balance, decimal amount)
        {
            return balance > 0 && balance > amount ? true : false;
        }

        public void IncreaseUserBalance(int id, decimal balance)
        {
            var person = _db.TblUsers.AsNoTracking().FirstOrDefault(x => x.UserId == id);
          
            if (balance > 0)
            {
                person!.Balance += balance;
            }

            _db.Entry(person!).State = EntityState.Modified;
            _db.SaveChanges();
   
        }

        public void DecreaseUserBalance(int id, decimal amount)
        {
            var person = _db.TblUsers.AsNoTracking().FirstOrDefault(x => x.UserId == id);

            if (!IsBalanceSufficient(person!.Balance, amount))
            {
                throw new Exception("Insufficient balance");
            }

            if (amount > 0)
            {
                person.Balance -= amount;
            }
            _db.Entry(person).State = EntityState.Modified;
            _db.SaveChanges();
        }

        public TblUser GetUserByPhoneNumber(string phoneNumber)
        {
            var user = _db.TblUsers.AsNoTracking().FirstOrDefault(x => x.MobileNo == phoneNumber);
            return user!;
        }

        public bool DeactivateUser(int id)
        {
            var person = _db.TblUsers.AsNoTracking().FirstOrDefault(x => x.UserId == id);
            person!.DeleteFlag = true;
            _db.Entry(person).State = EntityState.Modified;
            int result = _db.SaveChanges();
            return result > 0;
        }
    }
}
