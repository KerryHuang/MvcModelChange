using AutoMapper;
using Sample.Domain;
using Sample.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Repository.EF
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private NorthwindEntities db = new NorthwindEntities();

        /// <summary>
        /// Gets the one.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public Employee GetOne(int id)
        {
            var employee = this.db.Employees.FirstOrDefault(x => x.EmployeeID == id);

            if (employee != null)
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<Sample.Repository.EF.Employees, Employee>());
                config.AssertConfigurationIsValid();//←證驗應對
                var mapper = config.CreateMapper();
                Employee instance = mapper.Map<Employee>(employee);
                return instance;
            }
            return null;
        }

        /// <summary>
        /// Gets the employees.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Employee> GetEmployees()
        {
            var employees = this.db.Employees.OrderBy(x => x.EmployeeID).ToList();

            var config = new MapperConfiguration(cfg => cfg.CreateMap<Sample.Repository.EF.Employees, Employee>().PreserveReferences());
            config.AssertConfigurationIsValid();//←證驗應對
            var mapper = config.CreateMapper();
            List<Employee> result = mapper.Map<List<Employee>>(employees);

            return result;
        }
    }
}
