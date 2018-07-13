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
    public class SecurityRoleRepository : BaseADO, IDataRepository<SecurityRolePoco>
    {
        public void Add(params SecurityRolePoco[] items)
        {
            SqlConnection _connection = new SqlConnection(_connString);
            using (_connection)
            {
                _connection.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = _connection;
                foreach (SecurityRolePoco poco in items)
                {
                    cmd.CommandText = @"Insert into Security_Roles
(Id,Role,Is_Inactive)
VALUES (@Id,@Role, @Is_Inactive)";


                    cmd.Parameters.AddWithValue("@Role", poco.Role);
                    cmd.Parameters.AddWithValue("@Is_Inactive", poco.IsInactive);

                    cmd.Parameters.AddWithValue("@Id", poco.Id);


                    cmd.ExecuteNonQuery();


                }
                _connection.Close();
            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<SecurityRolePoco> GetAll(params System.Linq.Expressions.Expression<Func<SecurityRolePoco, object>>[] navigationProperties)
        {
            SecurityRolePoco[] pocos = new SecurityRolePoco[1000];
            SqlConnection _connection = new SqlConnection(_connString);
            using (_connection)
            {
                _connection.Open();
                SqlCommand cmd = new SqlCommand(@"select * from Security_Roles", _connection);

                SqlDataReader reader = cmd.ExecuteReader();
                int position = 0;
                while (reader.Read())
                {
                    SecurityRolePoco poco = new SecurityRolePoco();

                 
                    poco.Id = reader.GetGuid(0);
                    poco.Role = (string) reader[1];
                    poco.IsInactive = (bool) reader[2];
    

                    pocos[position] = poco;
                    position++;
                }
            }
            _connection.Close();
            return pocos.Where(p => p != null).ToList();
        }

        public IList<SecurityRolePoco> GetList(System.Linq.Expressions.Expression<Func<SecurityRolePoco, bool>> where, params System.Linq.Expressions.Expression<Func<SecurityRolePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public SecurityRolePoco GetSingle(System.Linq.Expressions.Expression<Func<SecurityRolePoco, bool>> where, params System.Linq.Expressions.Expression<Func<SecurityRolePoco, object>>[] navigationProperties)
        {
            IQueryable<SecurityRolePoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params SecurityRolePoco[] items)
        {
            SqlConnection _connection = new SqlConnection(_connString);
            using (_connection)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = _connection;
                foreach (SecurityRolePoco poco in items)
                {
                    cmd.CommandText = @"Delete From Security_Roles
                    where ID =@Id";
                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    _connection.Open();
                    cmd.ExecuteNonQuery();
                    _connection.Close();

                }
            }
        }

        public void Update(params SecurityRolePoco[] items)
        {
            SqlConnection _connection = new SqlConnection(_connString);
            using (_connection)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = _connection;
                foreach (SecurityRolePoco poco in items)
                {
                    cmd.CommandText = @"Update Security_Roles
                    set Role =@Role,
                        Is_Inactive =@Is_Inactive
                    where ID = @Id";

                   
                    cmd.Parameters.AddWithValue("@Role", poco.Role);
                    cmd.Parameters.AddWithValue("@Is_Inactive", poco.IsInactive);
                  
                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    _connection.Open();
                    cmd.ExecuteNonQuery();
                    _connection.Close();

                }
            }
        }
    }
}
