using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend_Server_Application
{
    class Manage_Material_Section<Subject, Week, Filename>
    {
        public Task<string> LoadMaterials(Subject subject, Week week)
        {

            string FileNames = String.Empty;
            string TableName = String.Empty;

            switch (subject as string)
            {
                case "0":
                    TableName = "computer_systems_materials_foundation_year";
                    break;

                case "1":
                    TableName = "databases_materials_year_1";
                    break;

                case "2":
                    TableName = "foundation_project_materials_foundation_year";
                    break;

                case "3":
                    TableName = "fundamentals_of_programming_materials_foundation_year";
                    break;

                case "4":
                    TableName = "fundamentals_of_software_engineering_materials_year_1";
                    break;

                case "5":
                    TableName = "logical_analysis_materials_foundation_year";
                    break;
            }

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
                    using (var Command = new MySql.Data.MySqlClient.MySqlCommand("SELECT material_name FROM " + TableName + " WHERE Week_Value = '" + week as string + "' ORDER BY material_name ASC;", Connection))
                    {
                        using (var Reader = Command.ExecuteReader())
                        {
                            while (Reader.Read())
                            {
                                FileNames += Reader["material_name"].ToString() + "|";
                            }
                        }
                    }

                    return Task.FromResult(FileNames);
                }
                catch { }

                return Task.FromResult(String.Empty);
            }
        }

        public Task<byte[]> DownloadMaterial(Subject subject, Week week, Filename filename)
        {
            string TableName = String.Empty;

            switch (subject as string)
            {
                case "0":
                    TableName = "computer_systems_materials_foundation_year";
                    break;

                case "1":
                    TableName = "databases_materials_year_1";
                    break;

                case "2":
                    TableName = "foundation_project_materials_foundation_year";
                    break;

                case "3":
                    TableName = "fundamentals_of_programming_materials_foundation_year";
                    break;

                case "4":
                    TableName = "fundamentals_of_software_engineering_materials_year_1";
                    break;

                case "5":
                    TableName = "logical_analysis_materials_foundation_year";
                    break;
            }


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
                    using (var Command = new MySql.Data.MySqlClient.MySqlCommand("SELECT material_file FROM " + TableName + " WHERE material_name = '"+ filename as string +"' AND Week_Value = '" + week as string + "';", Connection))
                    {
                        using (var Reader = Command.ExecuteReader())
                        {
                            
                            while(Reader.Read())
                            {
                                return Task.FromResult((byte[])Reader["material_file"]);
                            }
                        }
                    }
                }
                catch { }

                return Task.FromResult(new byte[1] { 0 });
            }
        }

        public Task<string> DownloadMaterialFileSize(Subject subject, Week week, Filename filename)
        {
            string TableName = String.Empty;

            switch (subject as string)
            {
                case "0":
                    TableName = "computer_systems_materials_foundation_year";
                    break;

                case "1":
                    TableName = "databases_materials_year_1";
                    break;

                case "2":
                    TableName = "foundation_project_materials_foundation_year";
                    break;

                case "3":
                    TableName = "fundamentals_of_programming_materials_foundation_year";
                    break;

                case "4":
                    TableName = "fundamentals_of_software_engineering_materials_year_1";
                    break;

                case "5":
                    TableName = "logical_analysis_materials_foundation_year";
                    break;
            }


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
                    using (var Command = new MySql.Data.MySqlClient.MySqlCommand("SELECT material_file FROM " + TableName + " WHERE material_name = '" + filename as string + "' AND Week_Value = '" + week as string + "';", Connection))
                    {
                        using (var Reader = Command.ExecuteReader())
                        {
                            while (Reader.Read())
                            {
                                return Task.FromResult(((byte[])Reader["material_file"]).Length.ToString());
                            }
                        }
                    }
                }
                catch { }

                return Task.FromResult(String.Empty);
            }
        }


        ~Manage_Material_Section()
        { }
    }
}
