using MySql.Data.MySqlClient;
using MySQLRepository.Model;
using MySQLRepository.Repository;
using System;

namespace MySQLRepository.UnitOfWork
{
    public class MySqlUnitOfWork : IMySqlUnitOfWork, IDisposable
    {
        private MySqlConnection context;

        public MySqlUnitOfWork()
        {
            try
            {
                context = MySQLConnection.MySqlConnectionApplication();
            }
            catch (Exception ex)
            {
                // Need to log write
                throw ex;
            }

        }

        #region MySQL

        private IMySQLRepository<BasicInfo> tblBasicInfo;
        public IMySQLRepository<BasicInfo> TblBasicInfo
        {
            get
            {
                if (this.tblBasicInfo == null)
                    this.tblBasicInfo = new MySQLRepository<BasicInfo>(context);
                return tblBasicInfo;
            }
        }

        #endregion



        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
