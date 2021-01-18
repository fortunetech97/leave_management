using leave_management.Contracts;
using leave_management.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Repository
{
    public class LeaveHistoryRepository : ILeaveHistoryRepository
    {
        private readonly ApplicationDbContext _db;
        public LeaveHistoryRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public bool Create(Data.LeaveHistory entity)
        {
            _db.LeaveHistories.Add(entity);
            return Save();
        }

        public bool Delete(Data.LeaveHistory entity)
        {
            _db.LeaveHistories.Remove(entity);
            return Save();
        }

        public ICollection<Data.LeaveHistory> FindAll()
        {
            var leaveHiostory = _db.LeaveHistories.ToList();
            return leaveHiostory;
        }

        public Data.LeaveHistory FindById(int id)
        {
            var leaveHistory = _db.LeaveHistories.Find(id);
            return leaveHistory;
        }

        public bool Save()
        {
            var changes = _db.SaveChanges();
            return changes > 0;
        }

        public bool Update(Data.LeaveHistory entity)
        {
            _db.LeaveHistories.Update(entity);
            return Save();
        }
    }
}
