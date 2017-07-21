using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Repository.EntLibDAAB
{
    public abstract class BaseRepository
    {
        private DatabaseProviderFactory factory = new DatabaseProviderFactory();

        private string connectionStringName = "Northwind";

        private Database db;
        protected Database Db
        {
            get
            {
                if (this.db == null)
                {
                    this.db = this.factory.Create(this.connectionStringName);
                }
                return this.db;
            }
        }

        public BaseRepository()
        {
        }

    }
}
