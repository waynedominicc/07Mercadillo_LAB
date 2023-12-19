using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _07Mercadillo_LAB
{
    public class Student
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string Program { get; set; }

    }
    internal class ClubRegistrationQuery
    {
        private SqlConnection sqlConnect;
        private SqlCommand sqlCommand;
        private SqlDataAdapter sqlAdapter;
        private SqlDataReader sqlReader;

        public DataTable dataTable;
        public BindingSource bindingSource;

        private string connectionString;

        public string _FirstName, _MiddleName, _LastName, _Gender, _Program;
        public int _Age;

        public ClubRegistrationQuery()
        {
            connectionString = "Data Source=LAPTOP-QS67U0AV\\SQLEXPRESS;Initial Catalog=ClubDB;Integrated Security=True";
            sqlConnect = new SqlConnection(connectionString);
            dataTable = new DataTable();
            bindingSource = new BindingSource();
        }
        public bool DisplayList()
        {
            try
            {
                string ViewClubMembers = "SELECT StudentId, FirstName, MiddleName, LastName, Age, Gender, Program FROM ClubMembers";
                sqlAdapter = new SqlDataAdapter(ViewClubMembers, sqlConnect);
                dataTable.Clear();
                sqlAdapter.Fill(dataTable);
                bindingSource.DataSource = dataTable;
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
                return false;
            }
        }
        public bool RegisterStudent(int ID, long StudentID, string FirstName, string MiddleName, string LastName, int Age, string Gender, string Program)
        {
            sqlCommand = new SqlCommand("INSERT INTO ClubMembers VALUES(@ID, @StudentID, @FirstName, @MiddleName, @LastName, @Age, @Gender, @Program)", sqlConnect);
            sqlCommand.Parameters.Add("@ID", SqlDbType.Int).Value = ID;
            sqlCommand.Parameters.Add("@RegistrationID", SqlDbType.BigInt).Value = StudentID;
            sqlCommand.Parameters.Add("@StudentID", SqlDbType.VarChar).Value = StudentID;
            sqlCommand.Parameters.Add("@FirstName", SqlDbType.VarChar).Value = FirstName;
            sqlCommand.Parameters.Add("@MiddleName", SqlDbType.VarChar).Value = MiddleName;
            sqlCommand.Parameters.Add("@LastName", SqlDbType.VarChar).Value = LastName;
            sqlCommand.Parameters.Add("@Age", SqlDbType.Int).Value = Age;
            sqlCommand.Parameters.Add("@Gender", SqlDbType.VarChar).Value = Gender;
            sqlCommand.Parameters.Add("@Program", SqlDbType.VarChar).Value = Program;
            sqlConnect.Open();
            sqlCommand.ExecuteNonQuery();
            sqlConnect.Close();
            return true;
        }
        public List<string> GetStudentIDs()
        {
            List<string> studentIDs = new List<string>();

            try
            {
                sqlConnect.Open();
                string query = "SELECT StudentId FROM ClubMembers";
                SqlCommand cmd = new SqlCommand(query, sqlConnect);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    studentIDs.Add(reader["StudentId"].ToString());
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while fetching student IDs: " + ex.Message);
            }
            finally
            {
                sqlConnect.Close();
            }
            return studentIDs;
        }

        public Student GetStudentInfo(string studentID)
        {
            Student student = new Student();

            try
            {
                sqlConnect.Open();
                string query = "SELECT * FROM ClubMembers WHERE StudentId = @StudentId";
                SqlCommand cmd = new SqlCommand(query, sqlConnect);
                cmd.Parameters.AddWithValue("@StudentId", studentID);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    student.FirstName = reader["FirstName"].ToString();
                    student.LastName = reader["LastName"].ToString();
                    student.MiddleName = reader["MiddleName"].ToString();

                    if (int.TryParse(reader["Age"].ToString(), out int age))
                    {
                        student.Age = age;
                    }
                    student.Gender = reader["Gender"].ToString();
                    student.Program = reader["Program"].ToString();
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while fetching student information: " + ex.Message);
            }
            finally
            {
                sqlConnect.Close();
            }
            return student;
        }
        public bool UpdateStudentInfo(string studentID, string updatedFirstName, string updatedLastName, string updatedMiddleName, int updatedAge, string updatedGender, string updatedProgram)
        {
            sqlConnect.Open();
            string query = "UPDATE ClubMembers SET FirstName = @FirstName, LastName = @LastName, MiddleName = @MiddleName, Age = @Age, Gender = @Gender, Program = @Program WHERE StudentId = @StudentId";
            SqlCommand cmd = new SqlCommand(query, sqlConnect);
            cmd.Parameters.AddWithValue("@FirstName", updatedFirstName);
            cmd.Parameters.AddWithValue("@LastName", updatedLastName);
            cmd.Parameters.AddWithValue("@MiddleName", updatedMiddleName);
            cmd.Parameters.AddWithValue("@Age", updatedAge);
            cmd.Parameters.AddWithValue("@Gender", updatedGender);
            cmd.Parameters.AddWithValue("@Program", updatedProgram);
            cmd.Parameters.AddWithValue("@StudentId", studentID);
            cmd.ExecuteNonQuery();
            sqlConnect.Close();
            MessageBox.Show("Student Info is successfully updated!");
            return true;
        }
    }
}

