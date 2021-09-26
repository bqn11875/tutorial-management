using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutorialsManagement.Core.Interfaces
{
    public interface IUnitOfWork
    {
        List<object> ExecuteReader(string commandText);
        List<object> ExecuteReader(string commandText, SqlParameter[] parameters = null);
        object ExecuteNonQuery(string commandText, SqlParameter[] parameters = null);
    }
}
