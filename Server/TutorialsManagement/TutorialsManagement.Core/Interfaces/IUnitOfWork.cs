using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutorialsManagement.Core.Interfaces
{
    public interface IUnitOfWork
    {
        DataTable ExecuteReader(string commandText);
        DataTable ExecuteReader(string commandText, SqlParameter[] parameters = null);
        object ExecuteNonQuery(string commandText, SqlParameter[] parameters = null);
    }
}
