using Sample.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Repository.Interface
{
    public interface IEmployeeRepository
    {
        Employee GetOne(int id);

        IEnumerable<Employee> GetEmployees();

    }
}
