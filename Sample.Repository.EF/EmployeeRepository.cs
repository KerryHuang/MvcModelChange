using AutoMapper;
using Sample.Domain;
using Sample.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
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
        public Employee Get(int id)
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
        public IEnumerable<Employee> GetAll()
        {
            var employees = this.db.Employees.OrderBy(x => x.EmployeeID).ToList();

            var config = new MapperConfiguration(cfg => cfg.CreateMap<Sample.Repository.EF.Employees, Employee>().PreserveReferences());
            config.AssertConfigurationIsValid();//←證驗應對
            var mapper = config.CreateMapper();
            List<Employee> result = mapper.Map<List<Employee>>(employees);

            return result;
        }

        public Employee Create(Employee employee)
        {
            if (employee == null)
            {
                throw new ArgumentNullException("entity");
            }

            var config = new MapperConfiguration(cfg => cfg.CreateMap<Employee, Sample.Repository.EF.Employees>());
            var mapper = config.CreateMapper();
            Employees entity = mapper.Map<Employees>(employee);

            db.Set<Employees>().Add(entity);
            SaveChanges();

            return employee;
        }

        public Employee Update(Employee employee)
        {
            if (employee == null)
            {
                throw new ArgumentNullException("entity");
            }

            var config = new MapperConfiguration(cfg => cfg.CreateMap<Employee, Sample.Repository.EF.Employees>());
            var mapper = config.CreateMapper();
            Employees entity = mapper.Map<Employees>(employee);

            db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            SaveChanges();

            return employee;
        }

        public int Delete(int id)
        {
            if (id == null)
            {
                throw new ArgumentNullException("entity");
            }

            var entity = this.db.Employees.FirstOrDefault(x => x.EmployeeID == id);

            db.Set<Employees>().Remove(entity);

            return SaveChanges();
        }

        private int SaveChanges()
        {
            List<string> message = new List<string>();

            try
            {
                return this.db.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                IEnumerable<string> errorMessages = ex.EntityValidationErrors.SelectMany(x => x.ValidationErrors)
                    .Select(v => string.Format("{0} - {1}", v.PropertyName, v.ErrorMessage));

                // Join the list to a single string.
                //sErrorMsg = string.Format("{0} validation errors: {1}", ex.Message, string.Join("; ", errorMessages));
                message.Add(string.Format("{0} validation errors: ", ex.Message));
                message.AddRange(errorMessages);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                message.AddRange(GetInnerException(ex));
            }
            catch (DbUpdateException ex)
            {
                message.AddRange(GetInnerException(ex));
            }
            catch (RetryLimitExceededException ex)
            {
                message.AddRange(GetInnerException(ex));
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    message.Add(ex.InnerException.Message);
                else
                    message.Add("更新錯誤");
            }

            if (message.Count > 0)
            {
                throw new Exception(string.Join("; ", message));
            }

            return 0;
        }

        private static List<string> GetInnerException(Exception ex)
        {
            List<string> errors = new List<string>();
            if (null == ex) { return errors; }
            //最多往下搜尋十層
            const int maxLevel = 10;
            Exception next = ex;

            for (int i = 0; i < maxLevel; i++)
            {
                if (null == next.InnerException)
                    break;
                next = next.InnerException;
                errors.Add(next.Message);
            }
            return errors;
        }
    }
}
