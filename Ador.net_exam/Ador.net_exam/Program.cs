using Microsoft.Data.SqlClient;
using System.Reflection.PortableExecutable;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ador.net_exam
{
    internal class Program
    {
        static void DataReaderExample(string connectionString)
        {
            SqlConnection conn = null;
            SqlDataReader reader = null;
            try
            {
                conn = new SqlConnection(connectionString);
                SqlCommand sqlCommand = new SqlCommand("SELECT * FROM students", conn);
                conn.Open();
                reader = sqlCommand.ExecuteReader();
                Console.WriteLine(reader.GetName(0) + " " + " " + reader.GetName(1) + " " + reader.GetName(2) + " " + reader.GetName(3));
                while (reader.Read())
                {
                    Console.WriteLine(reader["first_name"] + " " + reader["second_name"] + " " + reader["group_id"]);
                }
                Console.ReadLine();
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }
        static void MultiDataReaderExample(string connectionString)
        {
            SqlConnection conn = null;
            SqlDataReader reader = null;
            try
            {
                conn = new SqlConnection(connectionString);
                SqlCommand sqlCommand = new SqlCommand("SELECT * FROM students; SELECT * FROM teachers; SELECT * FROM grades", conn);
                conn.Open();
                reader = sqlCommand.ExecuteReader();
                do
                {
                    Console.WriteLine("=================================================");
                    Console.WriteLine(reader.GetName(0) + " " + " " + reader.GetName(1) + " " + reader.GetName(2) + " " + reader.GetName(3));
                    while (reader.Read())
                    {
                        Console.WriteLine(reader[0] + " " + reader[1] + " " + reader[2] + " " + reader[3]);
                    }
                } while (reader.NextResult());
                Console.ReadLine();
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }
        static void CorrectCommandExample(string connectionString)
        {
            SqlConnection conn = null;
            try
            {
                conn = new SqlConnection(connectionString);
                SqlCommand sqlCommand = new SqlCommand(
                    "INSERT INTO grades(student_id, teacher_id, rating) VALUES (@s_id, @t_id, @rt)",
                    conn);
                SqlParameter studentID = new SqlParameter();
                studentID.ParameterName = "@s_id";
                studentID.SqlDbType = System.Data.SqlDbType.Int;
                studentID.Value = 5;
                sqlCommand.Parameters.Add(studentID);

                sqlCommand.Parameters.Add("@t_id", System.Data.SqlDbType.Int).Value = 1;

                sqlCommand.Parameters.AddWithValue("@rt", 7);

                conn.Open();
                sqlCommand.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                if (!ex.Message.Contains("Вставлено"))
                {
                    Console.WriteLine("Ошибка SQL: " + ex.Message);
                }
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }
        static void ScalarFunctionExample(string connectionString)
        {
            SqlConnection conn = null;
            try
            {
                conn = new SqlConnection(connectionString);
                SqlCommand sqlCommand = new SqlCommand("SELECT count(*) FROM students", conn);
                conn.Open();
                Console.WriteLine($"Количество студентов в таблице: {sqlCommand.ExecuteScalar()}.");
                Console.ReadLine();
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }
        static void Main(string[] args)
        {
            string connectionString = @"Server=.\SQLEXPRESS;database=university;Integrated Security=true;TrustServerCertificate=true";
            DataReaderExample(connectionString);
            CorrectCommandExample(connectionString);
            MultiDataReaderExample(connectionString);
            ScalarFunctionExample(connectionString);
        }
    }
}
