using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend_Server_Application
{
    class Manage_Log_In<UserName, Password>
    {
        public Task<bool> Log_In(UserName username, Password password)
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
                    return Task.FromResult(false);
                }

                using (var Command = new MySql.Data.MySqlClient.MySqlCommand("SELECT student_Id, student_password, user_logged_in FROM student_credentials", Connection))
                {
                    try
                    {
                        using (var Reader = Command.ExecuteReader())
                        {
                            while (Reader.Read())
                            {
                                switch(Reader["student_Id"] as string == username as string)
                                {
                                    case true:

                                        switch(Reader["student_password"] as string == password as string)
                                        {
                                            case true:
                                                Reader.Close();
                                                Reader.Dispose();
                                                using (var Query = new MySql.Data.MySqlClient.MySqlCommand("UPDATE student_credentials SET user_logged_in = @user_logged_in WHERE student_Id = @student_Id", Connection))
                                                {
                                                    Query.Parameters.AddWithValue("@user_logged_in", "True");
                                                    Query.Parameters.AddWithValue("@student_Id", username);
                                                    Query.ExecuteNonQuery();
                                                }
                                                return Task.FromResult(true);
                                        }
                                        break;
                                }
                            }
                        }

                       
                    }
                    catch
                    {
                        return Task.FromResult(false);
                    }
                }
            }


            return Task.FromResult(false);
        }


        public Task<bool> Verify_If_Logged_In(UserName username, Password password)
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
                    return Task.FromResult(false);
                }

                using (var Command = new MySql.Data.MySqlClient.MySqlCommand("SELECT student_Id, student_password, user_logged_in FROM student_credentials", Connection))
                {
                    try
                    {
                        using (var Reader = Command.ExecuteReader())
                        {
                            while (Reader.Read())
                            {
                                switch (Reader["student_Id"] as string == username as string)
                                {
                                    case true:

                                        switch (Reader["student_password"] as string == password as string)
                                        {
                                            case true:

                                                switch (Reader["user_logged_in"] as string)
                                                {
                                                    case "True":
                                                        return Task.FromResult(true);

                                                    case null:
                                                        return Task.FromResult(false);
                                                }
                                                break;
                                        }
                                        break;
                                }
                            }
                        }


                    }
                    catch
                    {
                        return Task.FromResult(false);
                    }
                }


                return Task.FromResult(false);
            }
        }
    }
}
