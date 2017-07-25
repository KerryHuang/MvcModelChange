using Sample.Domain;
using Sample.Repository.Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Repository.ADONET
{
    public class EmployeeRepository : BaseRepository, IEmployeeRepository
    {
        DAO<EmployeeViewModel> db;

        public EmployeeRepository()
        {
            db = new DAO<EmployeeViewModel>(this.ConnectionString);
            db.TableName = "Employees";
        }

        /// <summary>
        /// Gets the one.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public EmployeeViewModel Get(int id)
        {
            db.SQL.AppendLine("select * from Employees where EmployeeID = @EmployeeID");
            db.Parameters.Add("EmployeeID", id);
            return db.Get();
        }

        /// <summary>
        /// Gets the employees.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public IEnumerable<EmployeeViewModel> GetAll()
        {
            db.SQL.AppendLine("select * from Employees order by EmployeeID");
            return db.GetAll();
        }

        /// <summary>
        /// add the employee
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public EmployeeViewModel Create(EmployeeViewModel employee)
        {
            return db.Create(employee);
        }

        /// <summary>
        /// update the employee
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public EmployeeViewModel Update(EmployeeViewModel employee)
        {
            return db.Update(employee);
        }

        /// <summary>
        /// delete the employee
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int Delete(int id)
        {
            var item = Get(id);
            if (item != null)
            {
                return db.Delete(item);
            }
            return 0;
        }
    }
}
