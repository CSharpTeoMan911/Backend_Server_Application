using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend_Server_Application
{
    class Manage_Contacts
    {
        public Task<string> Download_Contacts()
        {
            string ContactsPage = String.Empty;

            using (var Connection = new MySql.Data.MySqlClient.MySqlConnection("SERVER = localhost; DATABASE = universityrecords; UID = student; PASSWORD = User_Log_In"))
            {
                try
                {
                    Connection.Open();
                }
                catch
                {
                    Connection.Close();

                    return Task.FromResult(String.Empty);
                }

                try
                {
                    using (var Command = new MySql.Data.MySqlClient.MySqlCommand("SELECT academic_institution, institution_landline_number, institution_email FROM university_contacts", Connection))
                    {
                        using (var Reader = Command.ExecuteReader())
                        {
                            while(Reader.Read())
                            {
                                switch (Reader["institution_landline_number"] != DBNull.Value)
                                {
                                    case true:
                                        ContactsPage += Reader["institution_landline_number"].ToString() + "|";
                                        break;

                                    case false:
                                        ContactsPage += String.Empty;
                                        break;
                                }

                                switch (Reader["institution_email"] != DBNull.Value)
                                {
                                    case true:
                                        ContactsPage += Reader["institution_email"].ToString() + "|";
                                        break;

                                    case false:
                                        ContactsPage += String.Empty;
                                        break;
                                }
                            }
                        }

                        return Task.FromResult(ContactsPage);
                    }
                }
                catch 
                {
                    return Task.FromResult(String.Empty);
                }
            }
        }

        public Task<byte[]> Download_First_Institution_Contact_Pictures()
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

                    return Task.FromResult(new byte[1] {0});
                }

                try
                {
                    using (var Command = new MySql.Data.MySqlClient.MySqlCommand("SELECT institution_picture FROM university_contacts WHERE academic_institution = 'Bolton University' ", Connection))
                    {
                        using (var Reader = Command.ExecuteReader())
                        {
                            while (Reader.Read())
                            {
                                switch (Reader["institution_picture"] != DBNull.Value)
                                {
                                    case true:
                                        return Task.FromResult((byte[])(Reader["institution_picture"]));

                                    case false:
                                        return Task.FromResult(new byte[1] { 0 });
                                }
                            }
                        }
                    }
                }
                catch
                {
                    return Task.FromResult(new byte[1] { 0 });
                }

                return Task.FromResult(new byte[1] { 0 });
            }
        }


        public Task<byte[]> Download_Second_Institution_Contact_Pictures()
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

                    return Task.FromResult(new byte[1] { 0 });
                }

                try
                {
                    using (var Command = new MySql.Data.MySqlClient.MySqlCommand("SELECT institution_picture FROM university_contacts WHERE academic_institution = 'Regent College'", Connection))
                    {
                        using (var Reader = Command.ExecuteReader())
                        {
                            while (Reader.Read())
                            {
                                switch (Reader["institution_picture"] != DBNull.Value)
                                {
                                    case true:
                                        return Task.FromResult((byte[])(Reader["institution_picture"]));

                                    case false:
                                        return Task.FromResult(new byte[1] { 0 });
                                }
                            }
                        }
                    }
                }
                catch
                {
                    return Task.FromResult(new byte[1] { 0 });
                }

                return Task.FromResult(new byte[1] { 0 });
            }
        }
    }
}
