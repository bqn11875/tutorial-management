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

        public List<object> ExecuteReader(string commandText)
        {
            var conn = new SqlConnection(_context.Database.GetDbConnection().ConnectionString);
            var list = new List<object>();

            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                using (var command = new SqlCommand(string.Format("exec {0}", commandText), conn))
                {
                    SqlDataReader reader = command.ExecuteReader();
                    var fieldCount = reader.FieldCount;
                    while (reader.Read())
                    {
                        var fieldValues = new object[fieldCount];
                        int instances = reader.GetValues(fieldValues);
                        for (int fieldCounter = 0; fieldCounter < fieldCount; fieldCounter++)
                        {
                            if (Convert.IsDBNull(fieldValues[fieldCounter]))
                                fieldValues[fieldCounter] = "NA";
                        }
                        list.Add(fieldValues);
                    }

                    conn.Close();

                    return list;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<object> ExecuteReader(string commandText, SqlParameter[] parameters = null)
        {
            var conn = new SqlConnection(_context.Database.GetDbConnection().ConnectionString);
            var list = new List<object>();

            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                var parameterBuilder = new StringBuilder();
                if (parameters != null && parameters.Any())
                {
                    for (int i = 0; i < parameters.Length; i++)
                    {
                        if (parameters[i].SqlDbType == SqlDbType.VarChar
                            || parameters[i].SqlDbType == SqlDbType.NVarChar
                            || parameters[i].SqlDbType == SqlDbType.Char
                            || parameters[i].SqlDbType == SqlDbType.NChar
                            || parameters[i].SqlDbType == SqlDbType.Text
                            || parameters[i].SqlDbType == SqlDbType.NText)
                        {
                            parameterBuilder.Append(string.Format("@{0} = '{1}'", parameters[i].ParameterName,
                                string.IsNullOrEmpty(parameters[i].Value.ToString())
                                ? string.Empty : parameters[i].Value.ToString()));
                        }
                        else if (parameters[i].SqlDbType == SqlDbType.BigInt
                            || parameters[i].SqlDbType == SqlDbType.Int
                            || parameters[i].SqlDbType == SqlDbType.SmallInt
                            || parameters[i].SqlDbType == SqlDbType.TinyInt
                            || parameters[i].SqlDbType == SqlDbType.Float
                            || parameters[i].SqlDbType == SqlDbType.Decimal
                            || parameters[i].SqlDbType == SqlDbType.Money
                            || parameters[i].SqlDbType == SqlDbType.SmallMoney)
                        {
                            parameterBuilder.Append(string.Format("@{0} = {1}", parameters[i].ParameterName,
                                parameters[i].Value));
                        }
                        else if (parameters[i].SqlDbType == SqlDbType.Bit)
                        {
                            parameterBuilder.Append(string.Format("@{0} = {1}", parameters[i].ParameterName,
                                Convert.ToBoolean(parameters[i].Value)));
                        }

                        if (i < parameters.Length - 1)
                        {
                            parameterBuilder.Append(",");
                        }
                    }

                    using (var command = new SqlCommand(string.Format("exec {0} {1}", commandText, parameterBuilder.ToString()), conn))
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        var fieldCount = reader.FieldCount;
                        while (reader.Read())
                        {
                            var fieldValues = new object[fieldCount];
                            int instances = reader.GetValues(fieldValues);
                            for (int fieldCounter = 0; fieldCounter < fieldCount; fieldCounter++)
                            {
                                if (Convert.IsDBNull(fieldValues[fieldCounter]))
                                    fieldValues[fieldCounter] = "NA";
                            }
                            list.Add(fieldValues);
                        }

                        conn.Close();

                        return list;
                    }
                }
                else
                {
                    using (var command = new SqlCommand(string.Format("exec {0}", commandText), conn))
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        var fieldCount = reader.FieldCount;
                        while (reader.Read())
                        {
                            var fieldValues = new object[fieldCount];
                            int instances = reader.GetValues(fieldValues);
                            for (int fieldCounter = 0; fieldCounter < fieldCount; fieldCounter++)
                            {
                                if (Convert.IsDBNull(fieldValues[fieldCounter]))
                                    fieldValues[fieldCounter] = "NA";
                            }
                            list.Add(fieldValues);
                        }

                        conn.Close();

                        return list;
                    }
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

            var parameterBuilder = new StringBuilder();
            if (parameters != null && parameters.Any())
            {
                for (int i = 0; i < parameters.Length; i++)
                {
                    if (parameters[i].SqlDbType == SqlDbType.VarChar
                        || parameters[i].SqlDbType == SqlDbType.NVarChar
                        || parameters[i].SqlDbType == SqlDbType.Char
                        || parameters[i].SqlDbType == SqlDbType.NChar
                        || parameters[i].SqlDbType == SqlDbType.Text
                        || parameters[i].SqlDbType == SqlDbType.NText)
                    {
                        parameterBuilder.Append(string.Format("@{0} = '{1}'", parameters[i].ParameterName,
                            string.IsNullOrEmpty(parameters[i].Value.ToString())
                            ? string.Empty : parameters[i].Value.ToString()));
                    }
                    else if (parameters[i].SqlDbType == SqlDbType.BigInt
                        || parameters[i].SqlDbType == SqlDbType.Int
                        || parameters[i].SqlDbType == SqlDbType.SmallInt
                        || parameters[i].SqlDbType == SqlDbType.TinyInt
                        || parameters[i].SqlDbType == SqlDbType.Float
                        || parameters[i].SqlDbType == SqlDbType.Decimal
                        || parameters[i].SqlDbType == SqlDbType.Money
                        || parameters[i].SqlDbType == SqlDbType.SmallMoney)
                    {
                        parameterBuilder.Append(string.Format("@{0} = {1}", parameters[i].ParameterName,
                            parameters[i].Value));
                    }
                    else if (parameters[i].SqlDbType == SqlDbType.Bit)
                    {
                        parameterBuilder.Append(string.Format("@{0} = {1}", parameters[i].ParameterName,
                            Convert.ToBoolean(parameters[i].Value)));
                    }

                    if (i < parameters.Length - 1)
                    {
                        parameterBuilder.Append(",");
                    }
                }
            }

            var cmd = new SqlCommand(string.Format("exec {0} {1}", commandText, parameterBuilder.ToString()), conn);

            var id = cmd.ExecuteScalar(); // return just inserted record's id

            conn.Close();

            return id;
        }
    }
}
