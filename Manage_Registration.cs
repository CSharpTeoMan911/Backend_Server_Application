using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend_Server_Application
{
    class Manage_Registration<Username, Password>
    {


        public Task<bool> Register(Username username, Password password)
        {
            using (var Connection = new MySql.Data.MySqlClient.MySqlConnection("SERVER = localhost; UID = student; PASSWORD = User_Log_In; DATABASE = universityrecords"))
            {
                try
                {
                    Connection.Open();
                }
                catch 
                {
                    Connection.Close();
                    return Task.FromResult(true);
                }

                try
                {
                    using (var Command = new MySql.Data.MySqlClient.MySqlCommand("SELECT * FROM student_credentials", Connection))
                    {
                        using (var Reader = Command.ExecuteReader())
                        {
                            while(Reader.Read())
                            {
                                switch ((Reader["student_Id"] as string == username as string) || Reader["student_password"] as string == password as string)
                                {
                                    case true:
                                        return Task.FromResult(false);
                                }
                            }

                            Reader.Close();
                        }


                        using (var Register_Command = new MySql.Data.MySqlClient.MySqlCommand("INSERT INTO student_credentials(student_Id, student_password) VALUES(@student_Id, @student_password)", Connection))
                        {
                            Register_Command.Parameters.AddWithValue("@student_Id", username);
                            Register_Command.Parameters.AddWithValue("@student_password", password);
                            Register_Command.ExecuteNonQuery();
                            System.Diagnostics.Debug.WriteLine("OK");
                            return Task.FromResult(true);
                        }
                    }
                }
                catch(Exception E)
                {
                    System.Diagnostics.Debug.WriteLine(E.Message);
                    return Task.FromResult(false);
                }
            }
        }
    }
}
