using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.ADODataAccessLayer
{
    public class SecurityLoginsRoleRepository : BaseADO, IDataRepository<SecurityLoginsRolePoco>
    {
        public void Add(params SecurityLoginsRolePoco[] items)
        {
            SqlConnection _connection = new SqlConnection(_connString);
            using (_connection)
            {
                _connection.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = _connection;
                foreach (SecurityLoginsRolePoco poco in items)
                {
                    cmd.CommandText = @"Insert into Security_Logins_Roles
(Id,Login,Role)
VALUES (@Id,@Login, @Role)";


                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.Parameters.AddWithValue("@Login", poco.Login);

                    cmd.Parameters.AddWithValue("@Role", poco.Role);


                    cmd.ExecuteNonQuery();


                }
                _connection.Close();
            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<SecurityLoginsRolePoco> GetAll(params System.Linq.Expressions.Expression<Func<SecurityLoginsRolePoco, object>>[] navigationProperties)
        {
            SecurityLoginsRolePoco[] pocos = new SecurityLoginsRolePoco[1000];
            SqlConnection _connection = new SqlConnection(_connString);
            using (_connection)
            {
                _connection.Open();
                SqlCommand cmd = new SqlCommand(@"select * from Security_Logins_Roles", _connection);

                SqlDataReader reader = cmd.ExecuteReader();
                int position = 0;
                while (reader.Read())
                {
                    SecurityLoginsRolePoco poco = new SecurityLoginsRolePoco();


                    poco.Id = reader.GetGuid(0);
                    poco.Login = (Guid)reader[1];
                    poco.Role = (Guid)reader[2];
                    poco.TimeStamp = (byte[])reader[3];

                    pocos[position] = poco;
                    position++;
                }
            }
            _connection.Close();
            return pocos.Where(p => p != null).ToList();
        }

        public IList<SecurityLoginsRolePoco> GetList(System.Linq.Expressions.Expression<Func<SecurityLoginsRolePoco, bool>> where, params System.Linq.Expressions.Expression<Func<SecurityLoginsRolePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public SecurityLoginsRolePoco GetSingle(System.Linq.Expressions.Expression<Func<SecurityLoginsRolePoco, bool>> where, params System.Linq.Expressions.Expression<Func<SecurityLoginsRolePoco, object>>[] navigationProperties)
        {
            IQueryable<SecurityLoginsRolePoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params SecurityLoginsRolePoco[] items)
        {
            SqlConnection _connection = new SqlConnection(_connString);
            using (_connection)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = _connection;
                foreach (SecurityLoginsRolePoco poco in items)
                {
                    cmd.CommandText = @"Delete From Security_Logins_Roles
                    where ID =@Id";
                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    _connection.Open();
                    cmd.ExecuteNonQuery();
                    _connection.Close();

                }
            }
        }

        public void Update(params SecurityLoginsRolePoco[] items)
        {
            SqlConnection _connection = new SqlConnection(_connString);
            using (_connection)
            {
                       SqlCommand cmd = new SqlCommand();
                        cmd.Connection = _connection;
                        foreach (SecurityLoginsRolePoco poco in items)
                        {
                            cmd.CommandText = @"Update Security_Logins_Roles
                            set Login =@Login,
                                Role =@Role,
                               
                            where ID = @Id";

                            cmd.Parameters.AddWithValue("@Login", poco.Login);
                            cmd.Parameters.AddWithValue("@Role", poco.Role);
                           
                            cmd.Parameters.AddWithValue("@Id", poco.Id);
                            _connection.Open();
                            cmd.ExecuteNonQuery();
                            _connection.Close();

                        }
            }
        }
    }
}
