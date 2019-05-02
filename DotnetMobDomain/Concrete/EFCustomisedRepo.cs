using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotnetMobDomain.Abstract;
using DotnetMobDomain.Concrete;

namespace DotnetMobDomain.Concrete
{
    public class EFCustomisedRepo : IDotnetMobRepo
    {
        //OfficeDBMSSqlContext dBMSSqlContext = new OfficeDBMSSqlContext();
        private OfficeDBMSSqlContext _dBMSSqlContext;
        public EFCustomisedRepo(OfficeDBMSSqlContext context)
        {
            _dBMSSqlContext = context;
        }

        public void DeleteEmployee(int empId)
        {
            Employee emp = new Employee() { EmpId = empId };
            _dBMSSqlContext.Employees.Attach(emp);
            _dBMSSqlContext.Employees.Remove(emp);
            _dBMSSqlContext.SaveChanges();
        }

        public void MarkEmployeeAsDelete(int empid)
        {
            Employee emp = new Employee();
            emp = _dBMSSqlContext.Employees.Find(empid);
            emp.EmpStatus = (int)DomainEnums.EmployeeStatus.Delete;
            _dBMSSqlContext.SaveChanges();
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            return _dBMSSqlContext.Employees.Where(e => e.EmpStatus == (int)DomainEnums.EmployeeStatus.Active);
        }

        public IQueryable<Employee> GetEmployeeById(int EmpIdentity)
        {
            return _dBMSSqlContext.Employees.Where(e => e.EmpId == EmpIdentity);
        }

        public IQueryable<Employee> GetCancelledEmployeesRepo()
        {
            return _dBMSSqlContext.Employees.Where(e => e.EmpStatus == (int)DomainEnums.EmployeeStatus.Delete);
        }

        public void SaveEmployee(Employee emp)
        {
            _dBMSSqlContext.Employees.Add(emp);
            _dBMSSqlContext.SaveChanges();
        }

        public void UpdateEmployee(Employee employee)
        {
            //this is an update
            Employee emp = new Employee();
            emp = _dBMSSqlContext.Employees.Find(employee.EmpId);
            emp.EmpFName = employee.EmpFName;
            emp.EmpLName = employee.EmpLName;
            emp.Department = employee.Department;
            emp.EmpStatus = employee.EmpStatus;
            //dBMSSqlContext.Employees.Add(emp);
            _dBMSSqlContext.SaveChanges();
        }
    }
}
