using leave_management.Contracts;
using leave_management.Data;
using Microsoft.EntityFrameworkCore;
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

        public bool CheckAllocation(int leavetypeid, string employeeid)
        {
            var period = DateTime.Now.Year;
            return FindAll().Where(a => a.EmployeeId == employeeid && a.LeaveTypeId == leavetypeid && a.Period == period).Any();
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
            var leaveAllocation = _db.LeaveAllocation
                .Include(q => q.LeaveType)
                .Include(q => q.Employee)
                .ToList();
            return leaveAllocation;
        }

        public Data.LeaveAllocation FindById(int id)
        {
            var leaveAllocation = _db.LeaveAllocation
                .Include(q=> q.LeaveType)
                .Include(q=>q.Employee)
                .FirstOrDefault(q=> q.Id == id);
            return leaveAllocation;
        }

        public ICollection<LeaveAllocation> GetLeaveAllocationsByEmployee(string id)
        {
            var period = DateTime.Now.Year;
            return FindAll()
                     .Where(q => q.EmployeeId == id && q.Period == period )
                     .ToList();
        }

        public LeaveAllocation GetLeaveAllocationsByEmployeeAndType(string id, int leavetypeId)
        {
            var period = DateTime.Now.Year;
            return FindAll()
                     .FirstOrDefault(q => q.EmployeeId == id && q.Period == period && q.LeaveTypeId == leavetypeId);
        }

        public bool isExists(int id)
        {
            var exists = _db.LeaveAllocation.Any(q => q.Id == id);
            return exists;
                
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
