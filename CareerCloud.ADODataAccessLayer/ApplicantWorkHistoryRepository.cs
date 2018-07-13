﻿using CareerCloud.DataAccessLayer;
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
    public class ApplicantWorkHistoryRepository : BaseADO, IDataRepository<ApplicantWorkHistoryPoco>
    {
        public void Add(params ApplicantWorkHistoryPoco[] items)
        {
            SqlConnection _connection = new SqlConnection(_connString);
            using (_connection)
            {
                _connection.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = _connection;
                foreach (ApplicantWorkHistoryPoco poco in items)
                {
                    cmd.CommandText = @"Insert into Applicant_Work_History
(Id,Applicant,Company_Name,Country_Code,Job_Title,Job_Description,Start_Month,Start_Year,End_Month,End_Year,Location)
VALUES (@Id,@Applicant,@Company_Name,@Country_Code,@Job_Title,@Job_Description,@Start_Month,@Start_Year,@End_Month,@End_Year,@Location)";


                    cmd.Parameters.AddWithValue("@Applicant", poco.Applicant);
                    cmd.Parameters.AddWithValue("@Company_Name", poco.CompanyName);
                    cmd.Parameters.AddWithValue("@Country_Code", poco.CountryCode);
                    cmd.Parameters.AddWithValue("@Job_Title", poco.JobTitle);
                    cmd.Parameters.AddWithValue("@Job_Description", poco.JobDescription);
                    cmd.Parameters.AddWithValue("@Start_Month", poco.StartMonth);
                    cmd.Parameters.AddWithValue("@Start_Year", poco.StartYear);
                    cmd.Parameters.AddWithValue("@End_Month", poco.EndMonth);
                    cmd.Parameters.AddWithValue("@End_Year", poco.EndYear);
                    cmd.Parameters.AddWithValue("@Location", poco.Location);
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

        public IList<ApplicantWorkHistoryPoco> GetAll(params System.Linq.Expressions.Expression<Func<ApplicantWorkHistoryPoco, object>>[] navigationProperties)
        {
            ApplicantWorkHistoryPoco[] pocos = new ApplicantWorkHistoryPoco[1000];
            SqlConnection _connection = new SqlConnection(_connString);
            using (_connection)
            {
                _connection.Open();
                SqlCommand cmd = new SqlCommand(@"select * from Applicant_Work_History", _connection);

                SqlDataReader reader = cmd.ExecuteReader();
                int position = 0;
                while (reader.Read())
                {
                    ApplicantWorkHistoryPoco poco = new ApplicantWorkHistoryPoco();


                    poco.Id = reader.GetGuid(0);
                    poco.Applicant = reader.GetGuid(1);
                    poco.CompanyName = (string)reader[2];
                    poco.CountryCode = (string)reader[3];
                    poco.Location = (string)reader[4];
                    poco.JobTitle = (string)reader[5];
                    
                    poco.JobDescription = (string)reader[6];
                    poco.StartMonth = (short)reader[7];
                    poco.StartYear = (int)reader[8];
                    poco.EndMonth = (short)reader[9];
                    poco.EndYear = (int)reader[10];
                    poco.TimeStamp = (byte[])reader[11];

                    pocos[position] = poco;
                    position++;
                }
            }
            _connection.Close();
            return pocos.Where(p => p != null).ToList();
        }

        public IList<ApplicantWorkHistoryPoco> GetList(System.Linq.Expressions.Expression<Func<ApplicantWorkHistoryPoco, bool>> where, params System.Linq.Expressions.Expression<Func<ApplicantWorkHistoryPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantWorkHistoryPoco GetSingle(System.Linq.Expressions.Expression<Func<ApplicantWorkHistoryPoco, bool>> where, params System.Linq.Expressions.Expression<Func<ApplicantWorkHistoryPoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantWorkHistoryPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantWorkHistoryPoco[] items)
        {
            SqlConnection _connection = new SqlConnection(_connString);
            using (_connection)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = _connection;
                foreach (ApplicantWorkHistoryPoco poco in items)
                {
                    cmd.CommandText = @"Delete From Applicant_Work_History
                    where ID =@Id";
                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    _connection.Open();
                    cmd.ExecuteNonQuery();
                    _connection.Close();

                }
            }
        }

        public void Update(params ApplicantWorkHistoryPoco[] items)
        {
            SqlConnection _connection = new SqlConnection(_connString);
            using (_connection)
            {
        SqlCommand cmd = new SqlCommand();
                cmd.Connection = _connection;
                foreach (ApplicantWorkHistoryPoco poco in items)
                {
                    cmd.CommandText = @"Update Applicant_Work_History
                    set Applicant =@Applicant,
                        Company_Name=@Company_Name,
                        Country_Code =@Country_Code,
                        Job_Title =@Job_Title,
                        Job_Description =@Job_Description,
                        Start_Month =@Start_Month,
                        Start_Year =@Start_Year,
                         End_Month =@End_Month,
                        End_Year =@End_Year,
                        Location =@Location
                    where ID = @Id";

                    cmd.Parameters.AddWithValue("@Applicant", poco.Applicant);
                    cmd.Parameters.AddWithValue("@Company_Name", poco.CompanyName);
                    cmd.Parameters.AddWithValue("@Country_Code", poco.CountryCode);
                    cmd.Parameters.AddWithValue("@Job_Title", poco.JobTitle);
                    cmd.Parameters.AddWithValue("@Job_Description", poco.JobDescription);
                    cmd.Parameters.AddWithValue("@Start_Month", poco.StartMonth);
                    cmd.Parameters.AddWithValue("@Start_Year", poco.StartYear);
                    cmd.Parameters.AddWithValue("@End_Month", poco.EndMonth);
                    cmd.Parameters.AddWithValue("@End_Year", poco.EndYear);
                    cmd.Parameters.AddWithValue("@Location", poco.Location);
                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    _connection.Open();
                    cmd.ExecuteNonQuery();
                    _connection.Close();

                }
            }
        }
    }
}
