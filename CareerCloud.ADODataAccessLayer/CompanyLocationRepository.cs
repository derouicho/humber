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
    public class CompanyLocationRepository : BaseADO, IDataRepository<CompanyLocationPoco>
    {
        public void Add(params CompanyLocationPoco[] items)
        {
            SqlConnection _connection = new SqlConnection(_connString);
            using (_connection)
            {
                _connection.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = _connection;
                foreach (CompanyLocationPoco poco in items)
                {
                    cmd.CommandText = @"Insert into Company_Locations
(Id,Company,Country_Code,State_Province_Code,Street_Address,City_Town,Zip_Postal_Code)
VALUES (@Id,@Company,@Country_Code,@State_Province_Code,@Street_Address,@City_Town,@Zip_Postal_Code)";


                    cmd.Parameters.AddWithValue("@Company", poco.Company);
                    cmd.Parameters.AddWithValue("@Country_Code", poco.CountryCode);
                    cmd.Parameters.AddWithValue("@State_Province_Code", poco.Province);
                    cmd.Parameters.AddWithValue("@Street_Address", poco.Street);
                    cmd.Parameters.AddWithValue("@City_Town", poco.City);
                    cmd.Parameters.AddWithValue("@Zip_Postal_Code", poco.PostalCode);
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

        public IList<CompanyLocationPoco> GetAll(params System.Linq.Expressions.Expression<Func<CompanyLocationPoco, object>>[] navigationProperties)
        {
            CompanyLocationPoco[] pocos = new CompanyLocationPoco[1000];
            SqlConnection _connection = new SqlConnection(_connString);
            using (_connection)
            {
                _connection.Open();
                SqlCommand cmd = new SqlCommand(@"select * from Company_Locations", _connection);

                SqlDataReader reader = cmd.ExecuteReader();
                int position = 0;
                while (reader.Read())
                {
                    CompanyLocationPoco poco = new CompanyLocationPoco();


                    poco.Id = (Guid)reader[0];
                    poco.Company = (Guid)reader[1];
                    poco.CountryCode = (string)reader[2];
                    poco.Province = (string)reader[3];
                    poco.Street = (string)reader[4];
                    poco.City = reader.IsDBNull(5) ? (string)"" : reader.GetString(5);


                    poco.PostalCode = reader.IsDBNull(6) ? (string)"" : reader.GetString(6);
                    poco.TimeStamp = (byte[])reader[7];

                    pocos[position] = poco;
                    position++;
                }
            }
            _connection.Close();
            return pocos.Where(p => p != null).ToList(); 
        }

        public IList<CompanyLocationPoco> GetList(System.Linq.Expressions.Expression<Func<CompanyLocationPoco, bool>> where, params System.Linq.Expressions.Expression<Func<CompanyLocationPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyLocationPoco GetSingle(System.Linq.Expressions.Expression<Func<CompanyLocationPoco, bool>> where, params System.Linq.Expressions.Expression<Func<CompanyLocationPoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyLocationPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault(); throw new NotImplementedException();
        }

        public void Remove(params CompanyLocationPoco[] items)
        {
            SqlConnection _connection = new SqlConnection(_connString);
            using (_connection)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = _connection;
                foreach (CompanyLocationPoco poco in items)
                {
                    cmd.CommandText = @"Delete From Company_Locations
                    where ID =@Id";
                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    _connection.Open();
                    cmd.ExecuteNonQuery();
                    _connection.Close();

                }
            }
        }

        public void Update(params CompanyLocationPoco[] items)
        {
            SqlConnection _connection = new SqlConnection(_connString);
            using (_connection)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = _connection;
                foreach (CompanyLocationPoco poco in items)
                {
                    cmd.CommandText = @"Update Company_Locations
                    set Company =@Company,
                        Country_Code =@Country_Code,
                        State_Province_Code =@State_Province_Code,
                        Street_Address =@Street_Address,
                        City_Town =@City_Town,
                        Zip_Postal_Code =@Zip_Postal_Code
                    where ID = @Id";

                    cmd.Parameters.AddWithValue("@Company", poco.Company);
                    cmd.Parameters.AddWithValue("@Country_Code", poco.CountryCode);
                    cmd.Parameters.AddWithValue("@State_Province_Code", poco.Province);
                    cmd.Parameters.AddWithValue("@Street_Address", poco.Street);
                    cmd.Parameters.AddWithValue("@City_Town", poco.City);
                    cmd.Parameters.AddWithValue("@Zip_Postal_Code", poco.PostalCode);
                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    _connection.Open();
                    cmd.ExecuteNonQuery();
                    _connection.Close();

                }
            }
        }
    }
}
