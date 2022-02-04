using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// Step 1
// Add appropriate Namespaces
using System.Data.SqlClient;

namespace ADODemo
{
    class Program
    {
        static SqlConnection connection;
        static void Main(string[] args)
        {
            string choice="y";
            while (choice == "y")
            {
                int ch;
                MainMenu();
                ch = Byte.Parse(Console.ReadLine());
                switch (ch)
                {
                    case 1:
                        {
                            Console.WriteLine("Enter Course ID");
                            int courseId = int.Parse(Console.ReadLine());
                            Console.WriteLine("Enter Course Name");
                            string courseName = Console.ReadLine();
                            Console.WriteLine("Enter Course Description");
                            string description = Console.ReadLine();
                            InsertCourse(courseId, courseName, description); break;
                        }
                    case 2:
                        {
                            Console.WriteLine("Enter Course ID for which you want to edit record");
                            int courseId = int.Parse(Console.ReadLine());
                            Console.WriteLine("Enter Course Name");
                            string courseName = Console.ReadLine();
                            Console.WriteLine("Enter Course Description");
                            string description = Console.ReadLine();
                            UpdateCourse(courseId, courseName, description); break;
                        }
                    case 3: {
                        
                                Console.WriteLine("Enter Course ID for which you want to delete record");
                                int courseId = int.Parse(Console.ReadLine());
                                DeleteCourse(courseId); break;
                             }
                    case 4: GetCourses(); break;
                    default: Console.WriteLine("Invalid choice"); break; ;

                }
                Console.WriteLine("Do you want to repeat any process");
                choice = Console.ReadLine();
            }
        }

        private static void MainMenu()
        {
            Console.WriteLine("1. Insert Record");
            Console.WriteLine("2. Edit Record");
            Console.WriteLine("3. Delete Record");
            Console.WriteLine("4. List of Records");
            Console.WriteLine("Enter Choice");
        }

        static SqlConnection GetConnection()
        {
            connection = new SqlConnection("server=LAPTOP-53S2KQS8;initial catalog=CGIDB;integrated security=true");
            return connection;
        }
        private static void GetCourses()
        {
            // Step 2
            // Create Connection object which will contain ConnectionString
            connection = GetConnection();
            // SqlConnection connection = new SqlConnection("server=LAPTOP-53S2KQS8;initial catalog=CGIDB;user id=user1;password=password@123");
            // Step 3
            // Create Command Object, which takes 2 para,
            // 1. Command to be executed
            // 2. Connection object

            //SqlCommand command = new SqlCommand();
            //command.CommandText = "select * from course";
            //command.Connection = connection;

            SqlCommand command = new SqlCommand("Select * from course", connection);

            // Step 4
            // Establish connection
            connection.Open();
            // Step 5
            // Execute Query which is in command object on Sql Server
            SqlDataReader reader = command.ExecuteReader();

            // Step 6

            // Check whether reader has got records, if yes itearate over reade
            // and display them
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Console.WriteLine(reader[0] + " " + reader[1] + " " + reader[2]); ;
                }
            }

            // Step 7
            // Close connection
            connection.Close();
        }

        static void InsertCourse(int courseId, string courseName,string description)
        {
            //SqlConnection connection = new SqlConnection();
            //connection.ConnectionString = "server=LAPTOP-53S2KQS8;initial catalog=CGIDB;integrated security=true";
            connection = GetConnection();
            SqlCommand command = new SqlCommand();
            command.CommandText = "Insert into course(courseid, coursename,description) values (@courseId, @courseName, @description)";
            command.Connection = connection;
            command.Parameters.AddWithValue("@courseId", courseId);
            command.Parameters.AddWithValue("@courseName", courseName);
            command.Parameters.AddWithValue("@description", description);
            connection.Open();

            command.ExecuteNonQuery();

            connection.Close();
            
        }

        static void UpdateCourse(int courseId, string courseName, string description)
        {
            //SqlConnection connection = new SqlConnection();
            //connection.ConnectionString = "server=LAPTOP-53S2KQS8;initial catalog=CGIDB;integrated security=true";
            connection = GetConnection();
            SqlCommand command = new SqlCommand();
            command.CommandText = "update course set coursename=@courseName,description=@description " +
                "where courseid=@courseId";
            command.Connection = connection;
            command.Parameters.AddWithValue("@courseId", courseId);
            command.Parameters.AddWithValue("@courseName", courseName);
            command.Parameters.AddWithValue("@description", description);
            connection.Open();

            command.ExecuteNonQuery();

            connection.Close();

        }

        static void DeleteCourse(int courseId)
        {
            //SqlConnection connection = new SqlConnection();
            //connection.ConnectionString = "server=LAPTOP-53S2KQS8;initial catalog=CGIDB;integrated security=true";
            connection = GetConnection();
            SqlCommand command = new SqlCommand();
            command.CommandText = "delete course where courseid=@courseId";
            command.Parameters.AddWithValue("@courseId", courseId);
            command.Connection = connection;

            connection.Open();

            command.ExecuteNonQuery();

            connection.Close();

        }
    }
}
