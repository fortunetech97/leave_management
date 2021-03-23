using leave_management.Contracts;
using leave_management.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Repository
{
    public class LeaveRequestRepository : ILeaveRequestRepository
    {
        private readonly ApplicationDbContext _db;
        public LeaveRequestRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public bool Create(Data.LeaveRequest entity)
        {
            _db.LeaveRequests.Add(entity);
            return Save();
        }

        public bool Delete(Data.LeaveRequest entity)
        {
            _db.LeaveRequests.Remove(entity);
            return Save();
        }

        public ICollection<Data.LeaveRequest> FindAll()
        {
            var leaveHiostory = _db.LeaveRequests
                .Include(a => a.RequestingEmployee)
                .Include(a => a.ApprovedBy)
                .Include(a => a.LeaveType)
                .ToList();
            return leaveHiostory;
        }

        public Data.LeaveRequest FindById(int id)
        {
            var leaveHistory = _db.LeaveRequests
                .Include(a => a.RequestingEmployee)
                .Include(a => a.ApprovedBy)
                .Include(a => a.LeaveType)
                .FirstOrDefault(a=>a.Id==id);
            return leaveHistory;
        }

        public ICollection<LeaveRequest> GetLeaveRequestByEmployee (string employeeid)
        {
            var leaveRequest = FindAll()
                .Where(q => q.RequestingEmployeeId == employeeid)
                .ToList();
            return leaveRequest;
        }

        public bool Save()
        {
            var changes = _db.SaveChanges();
            return changes > 0;
        }

        public bool Update(Data.LeaveRequest entity)
        {
            _db.LeaveRequests.Update(entity);
            return Save();
        }
    }
}
