using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutorialsManagement.Core.Interfaces;

namespace TutorialsManagement.DataAccess.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;

        public UnitOfWork(DbContext context)
        {
            _context = context;
        }

        public DataTable ExecuteReader(string commandText)
        {
            var conn = new SqlConnection(_context.Database.GetDbConnection().ConnectionString);

            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                using (var cmd = new SqlCommand(commandText, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    var da = new SqlDataAdapter(cmd);
                    var ds = new DataSet();
                    da.Fill(ds);

                    conn.Close();

                    return ds.Tables[0];
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataTable ExecuteReader(string commandText, SqlParameter[] parameters = null)
        {
            var conn = new SqlConnection(_context.Database.GetDbConnection().ConnectionString);

            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                using (var cmd = new SqlCommand(commandText, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (parameters != null && parameters.Any())
                    {
                        foreach (var parameter in parameters)
                        {
                            cmd.Parameters.Add($"@{parameter.ParameterName}", parameter.SqlDbType).Value = parameter.Value;
                        }
                    }

                    var da = new SqlDataAdapter(cmd);
                    var ds = new DataSet();
                    da.Fill(ds);

                    conn.Close();

                    return ds.Tables[0];
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public object ExecuteNonQuery(string commandText, SqlParameter[] parameters = null)
        {
            var conn = new SqlConnection(_context.Database.GetDbConnection().ConnectionString);
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }

            var cmd = new SqlCommand(commandText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            if (parameters != null && parameters.Any())
            {
                foreach (var parameter in parameters)
                {
                    cmd.Parameters.Add($"@{parameter.ParameterName}", parameter.SqlDbType).Value = parameter.Value;
                }
            }

            var id = cmd.ExecuteScalar(); // return just inserted record's id

            conn.Close();

            return id;
        }
    }
}
