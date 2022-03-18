using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend_Server_Application
{
    class Manage_Grades<Subject, Id>
    {
        public Task<string> Download_Grades(Subject subject, Id id)
        {
            string GradePage = String.Empty;
            string GradesTableName = String.Empty;
            string FinalGradeTableName = String.Empty;

            switch (subject as string)
            {
                case "0":
                    GradesTableName = "computer_systems_grades_foundation_year";
                    FinalGradeTableName = "computer_systems_final_grade_foundation_year";
                    break;

                case "1":
                    GradesTableName = "databases_grades_year_1";
                    FinalGradeTableName = "databases_final_grade_year_1";
                    break;

                case "2":
                    GradesTableName = "foundation_project_grades_foundation_year";
                    FinalGradeTableName = "foundation_project_final_grade_foundation_year";
                    break;

                case "3":
                    GradesTableName = "fundamentals_of_programming_grades_foundation_year";
                    FinalGradeTableName = "fundamentals_of_programming_final_grade_foundation_year";
                    break;

                case "4":
                    GradesTableName = "fundamentals_of_software_engineering_grades_year_1";
                    FinalGradeTableName = "fundamentals_of_software_engineering_final_grade_year_1";
                    break;

                case "5":
                    GradesTableName = "logical_analysis_grades_foundation_year";
                    FinalGradeTableName = "logical_analysis_final_grade_foundation_year";
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
                    return Task.FromResult("N/A|N/A|N/A|N/A?");
                }

                try
                {
                    using (var Command = new MySql.Data.MySqlClient.MySqlCommand("SELECT * FROM " + GradesTableName + " WHERE student_Id ='" + id as string + "';", Connection))
                    {
                        using (var Reader = Command.ExecuteReader())
                        {
                          

                            while (Reader.Read())
                            {
                                switch(Reader["Grade1"] != DBNull.Value) 
                                {
                                    case true:
                                        GradePage += Reader["Grade1"].ToString() + "|";
                                        break;

                                    case false:
                                        GradePage += "N/A" + "|";
                                        break;
                                }

                                switch (Reader["Grade2"] != DBNull.Value)
                                {
                                    case true:
                                        GradePage += Reader["Grade2"].ToString() + "|";
                                        break;

                                    case false:
                                        GradePage += "N/A" + "|";
                                        break;
                                }

                                switch (Reader["Grade3"] != DBNull.Value)
                                {
                                    case true:
                                        GradePage += Reader["Grade3"].ToString() + "|";
                                        break;

                                    case false:
                                        GradePage += "N/A" + "|";
                                        break;
                                }
                            }
                        }
                    }

                    using (var Command = new MySql.Data.MySqlClient.MySqlCommand("SELECT FinalGrade FROM " + FinalGradeTableName + " WHERE student_Id ='" + id as string + "';", Connection))
                    {
                        using (var Reader = Command.ExecuteReader())
                        {
                            while (Reader.Read())
                            {
                                switch (Reader["FinalGrade"] != DBNull.Value)
                                {
                                    case true:
                                        GradePage += Reader["FinalGrade"].ToString();
                                        return Task.FromResult(GradePage);

                                    case false:
                                        GradePage += "N/A";
                                        return Task.FromResult(GradePage);
                                }
                            }
                        }
                    }


                    return Task.FromResult("N/A|N/A|N/A|N/A?");
                }
                catch
                {
                    return Task.FromResult("N/A|N/A|N/A|N/A?");
                }
            }
        }
    }
}
