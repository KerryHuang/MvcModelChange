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
        Employee Get(int id);

        IEnumerable<Employee> GetAll();

        Employee Create(Employee employee);

        Employee Update(Employee employee);

        int Delete(int id);
    }
}
