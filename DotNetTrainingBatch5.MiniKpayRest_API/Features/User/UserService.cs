using DotNetTrainingBatch5.MiniKpayRest_API.Models;
using Microsoft.EntityFrameworkCore;

namespace DotNetTrainingBatch5.MiniKpayRest_API.Features.User
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
            return person!;
        }

        public TblUser CreateUser(TblUser user)
        {
            _db.TblUsers.Add(user);
            _db.SaveChanges();
            return user;
        }

        public TblUser UpdateUserPin(int id, string pin)
        {
            var person = _db.TblUsers.AsNoTracking().FirstOrDefault(x => x.UserId == id);
            if (person is null)
            {
                return null;
            }

            person.Pin = pin;

            _db.Entry(person).State = EntityState.Modified;
            _db.SaveChanges();
            return person;
        }

        public bool IsBalanceSufficient(decimal balance, decimal amount)
        {
            return balance > amount ? true : false;
        }

        public TblUser IncreaseUserBalance(int id, decimal amount)
        {
            var person = _db.TblUsers.AsNoTracking().FirstOrDefault(x => x.UserId == id);
            if (person is null)
            {
                return null;
            }
            person.Balance += amount;

            _db.Entry(person).State = EntityState.Modified;
            _db.SaveChanges();
            return person;
        }

        public TblUser DecreaseUserBalance(int id, decimal amount)
        {
            var person = _db.TblUsers.AsNoTracking().FirstOrDefault(x => x.UserId == id);
            if (person is null)
            {
                return null;
            }

            var result = IsBalanceSufficient(person.Balance, amount);

            if (result)
            {
                person.Balance -= amount;
            } else
            {
                throw new Exception("Insufficient balance");
            }

            _db.Entry(person).State = EntityState.Modified;
            _db.SaveChanges();
            return person;
        }

        public TblUser GetUserByPhoneNumber(string phoneNumber)
        {
            var user = _db.TblUsers.AsNoTracking().FirstOrDefault(x => x.MobileNo == phoneNumber);
            return user;
        }

        public bool DeactivateUser(int id)
        {
            var person = _db.TblUsers.AsNoTracking().FirstOrDefault(x => x.UserId == id);
            if (person is null)
            {
                return false;
            }
            person.DeleteFlag = true;
            _db.Entry(person).State = EntityState.Modified;
            int result = _db.SaveChanges();
            return result > 0;
        }
    }
}
