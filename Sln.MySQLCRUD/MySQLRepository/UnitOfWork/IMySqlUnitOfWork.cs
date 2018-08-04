using MySQLRepository.Model;
using MySQLRepository.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace MySQLRepository.UnitOfWork
{
    public interface IMySqlUnitOfWork
    {
        IMySQLRepository<BasicInfo> TblBasicInfo { get; }
    }
}
