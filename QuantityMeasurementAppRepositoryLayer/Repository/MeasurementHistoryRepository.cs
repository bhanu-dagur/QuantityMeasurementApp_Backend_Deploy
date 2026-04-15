using QuantityMeasurementAppModelLayer.Entity;
using Microsoft.Data.SqlClient;
using QuantityMeasurementAppModelLayer.Util;
using QuantityMeasurementAppRepositoryLayer.Interface;

namespace QuantityMeasurementAppRepositoryLayer.Repository;

public class MeasurementHistoryRepository : IMeasurementHistoryRepository
{
    private readonly string _connectionString;
    public MeasurementHistoryRepository()
    {
        _connectionString = AppConfig.ConnectionString;
    }

    public void SaveHistory(QuantityMeasurementHistoryEntity entry)
    {

        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = @"INSERT INTO MeasurementHistory (InputValue1, InputUnit1,InputValue2, InputUnit2, 
                                TargetUnit, Operation, ResultValue,ResultUnit) 
                                VALUES (@val1, @unit1,@val2, @unit2, @tUnit,@operation,@result,@resUnit)";

            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@val1", entry.InputValue1);
                cmd.Parameters.AddWithValue("@unit1", entry.InputUnit1);
                cmd.Parameters.AddWithValue("@val2", entry.InputValue2);
                cmd.Parameters.AddWithValue("@unit2", entry.InputUnit2);
                cmd.Parameters.AddWithValue("@tUnit", entry.TargetUnit);
                cmd.Parameters.AddWithValue("@operation", entry.Operation);
                cmd.Parameters.AddWithValue("@result", entry.ResultValue);
                cmd.Parameters.AddWithValue("@resUnit", entry.ResultUnit);

                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }

    }

    public void SaveUser(User user)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = @"INSERT INTO [User] (FullName, Password,Email,Phone) 
                                VALUES (@fullname, @password,@email,@phone)";
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@fullname", user.FullName);
                cmd.Parameters.AddWithValue("@password", user.Password);
                cmd.Parameters.AddWithValue("@email", user.Email);
                cmd.Parameters.AddWithValue("@phone", user.Phone);
                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
    public User VerifyUser(string email)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = @"SELECT * FROM [User] WHERE Email = @email";
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@email", email);
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return new User()
                    {
                        FullName = reader["FullName"].ToString(),
                        Password = reader["Password"].ToString(),
                        Email=reader["Email"].ToString(),
                        Phone=reader["Phone"].ToString()
                    };
                }
            }

        }
        return null;
    }


}