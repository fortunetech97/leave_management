using leave_management.Contracts;
using leave_management.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Repository
{
    public class LeaveAllocationRepository : ILeaveAllocationRepository
    {
        private readonly ApplicationDbContext _db;
        public LeaveAllocationRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public bool Create(Data.LeaveAllocation entity)
        {
            _db.LeaveAllocation.Add(entity);
            return Save();
        }

        public bool Delete(Data.LeaveAllocation entity)
        {
            _db.LeaveAllocation.Remove(entity);
            return Save();
        }

        public ICollection<Data.LeaveAllocation> FindAll()
        {
            var leaveAllocation = _db.LeaveAllocation.ToList();
            return leaveAllocation;
        }

        public Data.LeaveAllocation FindById(int id)
        {
            var leaveAllocation = _db.LeaveAllocation.Find(id);
            return leaveAllocation;
        }

        public bool Save()
        {
            var changes =_db.SaveChanges();
            return changes > 0;
        }

        public bool Update(Data.LeaveAllocation entity)
        {
            _db.LeaveAllocation.Update(entity);
            return Save();
        }
    }
}
