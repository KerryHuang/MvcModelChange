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
        EmployeeViewModel Get(int id);

        IEnumerable<EmployeeViewModel> GetAll();

        EmployeeViewModel Create(EmployeeViewModel instance);

        EmployeeViewModel Update(EmployeeViewModel instance);

        int Delete(int id);
    }
}
