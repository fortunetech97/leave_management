using leave_management.Contracts;
using leave_management.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Repository
{
    public class LeaveTypeRepository : ILeaveTypeRepository
    {
        private readonly ApplicationDbContext _db;
        public LeaveTypeRepository(ApplicationDbContext db)
        {
            _db = db;       
        }
        public bool Create(LeaveType entity)
        {
            var leaveType = _db.LeaveType.Add(entity);
            return Save();
        }

        public bool Delete(LeaveType entity)
        {
           _db.LeaveType.Remove(entity);
            return Save();
        }

        public ICollection<LeaveType> FindAll()
        {
            var leaveType = _db.LeaveType.ToList();
            return leaveType;
        }

        public LeaveType FindById(int id)
        {
            var leaveType = _db.LeaveType.Find(id);
            return leaveType;
        }

        public ICollection<LeaveType> GetEmployeesByLeaveType(int id)
        {
            throw new NotImplementedException();
        }

        public bool isExists(int id)
        {
            var leaveType = _db.LeaveType.Find(id);
            if(leaveType == null)
            {
                return false;
            }
            return true;
        }

        public bool Save()
        {
            var changes = _db.SaveChanges();
            return changes > 0;
        }

        public bool Update(LeaveType entity)
        {
            _db.LeaveType.Update(entity);
             return Save();
        }
    }
}
