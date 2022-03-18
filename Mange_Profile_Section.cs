using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend_Server_Application
{
    class Mange_Profile_Section<Id, Profile_Picture_Binary_Information>
    {
        public Task<byte[]> Download_Profile_Picture(Id id)
        {
 
            using (var Connection = new MySql.Data.MySqlClient.MySqlConnection("SERVER = localhost; DATABASE = universityrecords; UID = student; PASSWORD = User_Log_In"))
            {
                try
                {
                    Connection.Open();
                }
                catch
                {
                    Connection.Close();
                }

                using (var Command = new MySql.Data.MySqlClient.MySqlCommand("SELECT student_profile_picture FROM student_credentials WHERE student_Id = '" + id as string + "';", Connection))
                {
                    try
                    {
                        using (var Reader = Command.ExecuteReader())
                        {
                            while(Reader.Read())
                            {
                                switch(Reader["student_profile_picture"] != DBNull.Value)
                                {
                                    case true:
                                        return Task.FromResult((byte[])Reader["student_profile_picture"]);
                                }
                            }
                        }
                    }
                    catch {}
                }
            }
                return Task.FromResult(new byte[1] {0});
        }


        public Task<bool> Upload_Profile_Picture(Profile_Picture_Binary_Information picture_binary_information)
        {
            using (var Connection = new MySql.Data.MySqlClient.MySqlConnection("SERVER = localhost; DATABASE = universityrecords; UID = student; PASSWORD = User_Log_In"))
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

                //string Id = String.Empty;

                //for(int Index = 0; Index <= 11; Index++)
                //{
                //    switch((picture_binary_information as byte[])[Index] == 0)
                //    {
                //        case false:
                //            Id += ((picture_binary_information as byte[])[Index]).ToString();
                //            break;
                //    }
                //}


                // ! ! ! ! Change byte header system ! ! ! ! 


                //byte[] buffer = new byte[(picture_binary_information as byte[]).Length - 63];

                //for(int Index = 0; Index <= buffer.Length -1; Index++)
                //{
                //    buffer[Index] = (picture_binary_information as byte[])[Index + 63];
                //}

                using (var Command = new MySql.Data.MySqlClient.MySqlCommand("UPDATE student_credentials SET student_profile_picture = @profile_picture  WHERE student_Id = @student_Id", Connection))
                {
                    try
                    {
                        Command.Parameters.AddWithValue("@profile_picture", picture_binary_information as byte[]);
                        Command.Parameters.AddWithValue("@student_Id", "1");
                        Command.ExecuteNonQuery();
                    }
                    catch
                    {
                        return Task.FromResult(false);
                    }
                }
            }
                return Task.FromResult(false);
        }
    }
}
