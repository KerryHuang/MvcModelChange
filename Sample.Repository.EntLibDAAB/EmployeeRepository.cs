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
        public EmployeeViewModel Get(int id)
        {
            string sqlStatement = "select * from Employees where EmployeeID = @EmployeeID";

            DataAccessor<EmployeeViewModel> accessor =
                this.Db.CreateSqlStringAccessor<EmployeeViewModel>(
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
        public IEnumerable<EmployeeViewModel> GetAll()
        {
            string sqlStatement = "select * from Employees order by EmployeeID";

            DataAccessor<EmployeeViewModel> accessor =
                this.Db.CreateSqlStringAccessor<EmployeeViewModel>(sqlStatement, new EmployeeMapper());

            return accessor.Execute();
        }


        public EmployeeViewModel Create(EmployeeViewModel employee)
        {
            throw new NotImplementedException();
        }


        public EmployeeViewModel Update(EmployeeViewModel employee)
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

    public class EmployeeMapper : IRowMapper<EmployeeViewModel>
    {
        public EmployeeViewModel MapRow(IDataRecord reader)
        {
            EmployeeViewModel item = new EmployeeViewModel();

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
