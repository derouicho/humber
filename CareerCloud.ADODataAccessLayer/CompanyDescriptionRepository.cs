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
    public class CompanyDescriptionRepository : BaseADO, IDataRepository<CompanyDescriptionPoco>
    {
        public void Add(params CompanyDescriptionPoco[] items)
        {
            SqlConnection _connection = new SqlConnection(_connString);
            using (_connection)
            {
                _connection.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = _connection;
                foreach (CompanyDescriptionPoco poco in items)
                {
                    cmd.CommandText = @"Insert into Company_Descriptions
(Id,Company,LanguageID,Company_Name,Company_Description)
VALUES (@Id,@Company,@LanguageID,@Company_Name,@Company_Description)";


                    cmd.Parameters.AddWithValue("@Company", poco.Company);
                    cmd.Parameters.AddWithValue("@LanguageID", poco.LanguageId);
                    cmd.Parameters.AddWithValue("@Company_Name", poco.CompanyName);
                    cmd.Parameters.AddWithValue("@Company_Description", poco.CompanyDescription);

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

        public IList<CompanyDescriptionPoco> GetAll(params System.Linq.Expressions.Expression<Func<CompanyDescriptionPoco, object>>[] navigationProperties)
        {
            CompanyDescriptionPoco[] pocos = new CompanyDescriptionPoco[1000];
            SqlConnection _connection = new SqlConnection(_connString);
            using (_connection)
            {
                _connection.Open();
                SqlCommand cmd = new SqlCommand(@"select * from Company_Descriptions", _connection);

                SqlDataReader reader = cmd.ExecuteReader();
                int position = 0;
                while (reader.Read())
                {
                    CompanyDescriptionPoco poco = new CompanyDescriptionPoco();


                    poco.Id = reader.GetGuid(0);
                    poco.Company = (Guid)reader[1];
                    poco.LanguageId = (string)reader[2];
                    poco.CompanyName = (string)reader[3];
                    poco.CompanyDescription = (string)reader[4];
                    poco.TimeStamp = (byte[])reader[5];

                    pocos[position] = poco;
                    position++;
                }
            }
            _connection.Close();
            return pocos.Where(p => p != null).ToList();
        }

        public IList<CompanyDescriptionPoco> GetList(System.Linq.Expressions.Expression<Func<CompanyDescriptionPoco, bool>> where, params System.Linq.Expressions.Expression<Func<CompanyDescriptionPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyDescriptionPoco GetSingle(System.Linq.Expressions.Expression<Func<CompanyDescriptionPoco, bool>> where, params System.Linq.Expressions.Expression<Func<CompanyDescriptionPoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyDescriptionPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyDescriptionPoco[] items)
        {
            SqlConnection _connection = new SqlConnection(_connString);
            using (_connection)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = _connection;
                foreach (CompanyDescriptionPoco poco in items)
                {
                    cmd.CommandText = @"Delete From Company_Descriptions
                    where ID =@Id";
                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    _connection.Open();
                    cmd.ExecuteNonQuery();
                    _connection.Close();

                }
            }
        }

        public void Update(params CompanyDescriptionPoco[] items)
        {
            SqlConnection _connection = new SqlConnection(_connString);
            using (_connection)
            {
                SqlCommand cmd = new SqlCommand();
                                cmd.Connection = _connection;
                                foreach (CompanyDescriptionPoco poco in items)
                                {
                                    cmd.CommandText = @"Update Company_Descriptions
                                    set Company =@Company,
                                        LanguageID =@LanguageID,
                                        Company_Name =@Company_Name,
                                        Company_Description =@Company_Description
                                    where ID = @Id";

                                    cmd.Parameters.AddWithValue("@Company", poco.Company);
                                    cmd.Parameters.AddWithValue("@LanguageID", poco.LanguageId);
                                    cmd.Parameters.AddWithValue("@Company_Name", poco.CompanyName);
                                    cmd.Parameters.AddWithValue("@Company_Description", poco.CompanyDescription);
                                   
                                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                                    _connection.Open();
                                    cmd.ExecuteNonQuery();
                                    _connection.Close();

                                }
            }
        }
    }
}
