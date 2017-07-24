using Microsoft.Practices.EnterpriseLibrary.Data;
using Sample.Domain;
using Sample.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Repository.EntLibDAAB
{
    public class EmployeeRepository : BaseRepository, IEmployeeRepository
    {
        /// <summary>
        /// Gets the one.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public Employee Get(int id)
        {
            string sqlStatement = "select * from Employees where EmployeeID = @EmployeeID";

            DataAccessor<Employee> accessor =
                this.Db.CreateSqlStringAccessor<Employee>(
                    sqlStatement,
                    new EmployeeIDParameterMapper(),
                    new EmployeeMapper());

            return accessor.Execute(new object[] { id }).FirstOrDefault();
        }

        /// <summary>
        /// Gets the employees.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public IEnumerable<Employee> GetAll()
        {
            string sqlStatement = "select * from Employees order by EmployeeID";

            DataAccessor<Employee> accessor =
                this.Db.CreateSqlStringAccessor<Employee>(sqlStatement, new EmployeeMapper());

            return accessor.Execute();
        }


        public Employee Create(Employee employee)
        {
            throw new NotImplementedException();
        }


        public Employee Update(Employee employee)
        {
            throw new NotImplementedException();
        }

        public int Delete(int id)
        {
            throw new NotImplementedException();
        }
    }

    public class EmployeeIDParameterMapper : IParameterMapper
    {
        public void AssignParameters(DbCommand command, object[] parameterValues)
        {
            var param = command.CreateParameter();
            param.ParameterName = "EmployeeID";
            param.Value = parameterValues[0];
            command.Parameters.Add(param);
        }
    }

    public class EmployeeMapper : IRowMapper<Employee>
    {
        public Employee MapRow(IDataRecord reader)
        {
            Employee item = new Employee();

            for (int i = 0; i < reader.FieldCount; i++)
            {
                PropertyInfo property = item.GetType().GetProperty(reader.GetName(i));

                if (property != null && !reader.GetValue(i).Equals(DBNull.Value))
                {
                    ReflectionHelper.SetValue(property.Name, reader.GetValue(i), item);
                }
            }
            return item;
        }
    }
}
