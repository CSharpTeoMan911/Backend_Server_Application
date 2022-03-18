using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.IO.Compression;

namespace Backend_Server_Application
{
    class Program
    {

        //  
        //   _____________________________________________________________________________________________________________________________________________________________________
        //  |                                                   HEADER                                                               |                    CONTENT                 |
        //  |________________________________________________________________________________________________________________________|____________________________________________|
        //  |                                                                                                                        |                                            |
        //  |  ________      _________       ______                                 ____________                                     |                 _________                  |
        //  | | OPTION |    | SUBJECT |     | WEEK |                               | STUDENT ID |                                    |                | CONTENT |                 |
        //  | |________|    |_________|     |______|                               |____________|                                    |                |_________|                 |
        //  |                                                                                                                        |                                            |
        //  | [ 0 ] [ 0 ]          [ 1 ]      [ 1 ] [ 1 ]       [ 1 ] [ 1 ] [ 1 ] [ 1 ] [ 1 ] [ 1 ] [ 1 ] [ 1 ] [ 1 ] [ 1 ] [ 1 ] [ 1 ]  |       [ 1 ] [ 1 ] [ 1 ] [ 1 ]  . . . . |
        //  |                                                                                                                        |                                            |
        //  |________________________________________________________________________________________________________________________|____________________________________________|


        private static System.Timers.Timer port_finder_timer;
        private static System.Threading.Thread ParallelProcessing;

        private static bool PortFound;
        private static int Port = 20;
        private static bool Server_Closing;


        private static void Main()
        {
            AppDomain.CurrentDomain.ProcessExit += CurrentDomain_ProcessExit;

            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("           __________________________");
            Console.WriteLine("          ||                        ||");
            Console.WriteLine("          ||________________________||");
            Console.WriteLine("          || |-||-||-||-||-||------ ||");
            Console.WriteLine("          || |-||-||-||-||-||------ ||");
            Console.Write("          || ------------------[");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("O");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write("]- ||\n");
            Console.WriteLine("          || |-||-||-||-||-||------ ||");
            Console.Write("          || ------------------[");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("O");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write("]- ||\n");
            Console.WriteLine("          || |-||-||-||-||-||------ ||");
            Console.Write("          || ------------------[");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("O");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write("]- ||\n");
            Console.WriteLine("          || |-||-||-||-||-||------ ||");
            Console.WriteLine("          || |-||-||-||-||-||------ ||");
            Console.Write("          || ------------------[");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("O");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write("]- ||\n");
            Console.WriteLine("          || |-||-||-||-||-||------ ||");
            Console.Write("          || ------------------[");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("O");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write("]- ||\n");
            Console.WriteLine("          || |-||-||-||-||-||------ ||");
            Console.Write("          || ------------------[");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("O");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write("]- ||\n");
            Console.WriteLine("          || |-||-||-||-||-||------ ||");
            Console.WriteLine("          |__________________________|");
            Console.WriteLine("          ||________________________||");
            Console.WriteLine("          ||________________________||");
            Console.WriteLine("          ||________________________||");
            Console.WriteLine("          ||________________________||\n\n\n");




            Console.Write("          [-] Enter [ START ] to start the server,  [ CLOSE ] or ' EXIT ' to [ EXIT ]: ");
            var User_Input = Console.ReadLine();
            Console.ResetColor();


            if (User_Input == "START")
            {
               
                IPAddress iPAddress = IPAddress.Any;


                port_finder_timer = new System.Timers.Timer();
                port_finder_timer.Elapsed += Port_finder_timer_Elapsed;
                port_finder_timer.Interval = 100;
                port_finder_timer.Start();




            }
            else if (User_Input == "EXIT")
            {
                Environment.Exit(0);
            }
            else
            {
                Console.Clear();
                Main();
            };

        CloseTheApp:

            var UserInput = Console.ReadLine();

            if (UserInput == "EXIT")
            {
                switch (PortFound = true)
                {
                    case true:
                        Environment.Exit(0);
                        break;

                    case false:
                        goto CloseTheApp;
                }
            }
            else
            {
                switch (PortFound)
                {
                    case true:
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("\n\n\n\n\n\t\t[ ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ]");
                        Console.WriteLine("\t\t[ ! ! ! ] SERVER STARTED [ ! ! !  ]");
                        Console.WriteLine("\t\t[ ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ]\n\n\n\n\n");
                        Console.WriteLine("\n\n\n\t\tEnter [ EXIT ] to exit the server application.\n\n\n\n\n");
                        Console.Write("\t\t_[ - ] Input:  ");
                        goto CloseTheApp;

                    case false:
                        goto CloseTheApp;
                }
            }
        }

        private static void CurrentDomain_ProcessExit(object sender, EventArgs e)
        {
            Server_Closing = true;
        }

        private static void Port_finder_timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {

            switch (PortFound)
            {

                case false:
                    try
                    {
                        IPAddress iPAddress = IPAddress.Any;
                        using (var port_finder = new TcpClient())
                        {
                            port_finder.ConnectAsync(iPAddress, Port);
                            port_finder.Close();
                            port_finder.Dispose();

                            InitiateServer(Port);
                        }
                    }
                    catch
                    {
                        switch (Port == 0)
                        {
                            case true:
                                Port += 20;
                                break;

                            case false:
                                Port++;
                                break;
                        }

                    }
                    break;
            }
        }

        private static void InitiateServer(int Port)
        {
            port_finder_timer?.Stop();
            PortFound = true;

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\n\n\n\n\n\t\t\t\t[ ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ]");
            Console.WriteLine("\t\t\t\t[ ! ! ! ] SERVER STARTED [ ! ! !  ]");
            Console.WriteLine("\t\t\t\t[ ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ]\n\n\n\n\n");
            Console.WriteLine("\n\n\n\t\t[ - ] SERVER LISTENING FOR ACTIVE CONNECTIONS ON PORT: " + Port + " [ - ]");
            Console.WriteLine("\n\n\n\t\t[ - ] [ - ] [ - ] [ - ] [ - ] [ - ] [ - ] [ - ] [ - ] [ - ] [ - ] [ - ] [ - ] [ - ] [ - ]");
            Console.WriteLine("\n\n\t\tEnter [ EXIT ] to exit the server application.\n\n");
            Console.Write("\t\t_[ - ] Input:  ");


            Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP );
            server.ReceiveBufferSize = 18000;
            IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Any, Port);
            server.Bind(iPEndPoint);
            server.Listen(3000);


            while (Server_Closing == false)
            {

                Socket client = server.Accept();
                ParallelProcessing = new System.Threading.Thread(delegate ()
                {
                    Server_Operation(client);
                });
                ParallelProcessing.SetApartmentState(System.Threading.ApartmentState.STA);
                ParallelProcessing.Priority = System.Threading.ThreadPriority.AboveNormal;
                ParallelProcessing.IsBackground = true;
                ParallelProcessing.Start();
            }
        }

        private static void Server_Operation(Socket data_receiver_stream)
        {
            data_receiver_stream.ReceiveBufferSize = 18000;

            byte[] data_buffer = new byte[data_receiver_stream.ReceiveBufferSize];

            data_receiver_stream.Receive(data_buffer, data_buffer.Length , SocketFlags.None);

            string Recived_Message = Encoding.UTF8.GetString(data_buffer, 0, data_buffer.Count());
            string Option = String.Empty;
            string Subject = String.Empty;
            string Week = String.Empty;
            string Id = String.Empty;
            string Password = String.Empty;
            string FileName = String.Empty;

            
            

            switch(Recived_Message[0] != '0')
            {
                case true:
                    Option += Recived_Message[0].ToString();
                    break;
            }


            Option += Recived_Message[1].ToString();

            Subject += Recived_Message[2].ToString();


            switch (Recived_Message[3] != '0')
            {
                case true:
                    Week += Recived_Message[3].ToString();
                    break;
            }
            
            Week += Recived_Message[4].ToString();
       

            for (int Index = 6; Index < Recived_Message.IndexOf(']'); Index++)
            {
                
                Id += Recived_Message[Index];
            }

            for(int Index = Recived_Message.LastIndexOf('[') + 1; Index < Recived_Message.LastIndexOf(']'); Index++)
            {
                Password += Recived_Message[Index].ToString();
            }

            switch (Recived_Message.Length - 1 - Recived_Message.LastIndexOf(']') > 0)
            {
                case true:
                    for (int Index = Recived_Message.LastIndexOf(']') + 1; Index <= Recived_Message.Length - 1; Index++)
                    {
                        FileName += Recived_Message[Index].ToString();
                    }
                    break;
            }


            switch (Option)
            {
                case "0":

                    ParallelProcessing = new System.Threading.Thread(delegate ()
                    {
                        Profile_Picture_Download_Operation(data_receiver_stream, Id, Password);
                    });
                    ParallelProcessing.SetApartmentState(System.Threading.ApartmentState.STA);
                    ParallelProcessing.Priority = System.Threading.ThreadPriority.AboveNormal;
                    ParallelProcessing.IsBackground = true;
                    ParallelProcessing.Start();
                    break;

                case "1":

                    ParallelProcessing = new System.Threading.Thread(delegate ()
                    {
                        Grades_Download_Operation(data_receiver_stream, Subject, Id, Password);
                    });
                    ParallelProcessing.SetApartmentState(System.Threading.ApartmentState.STA);
                    ParallelProcessing.Priority = System.Threading.ThreadPriority.AboveNormal;
                    ParallelProcessing.IsBackground = true;
                    ParallelProcessing.Start();
                    break;

                case "2":

                    ParallelProcessing = new System.Threading.Thread(delegate ()
                    {
                        Material_Download_Operation(data_receiver_stream, Subject, Week, FileName, Id, Password);
                    });
                    ParallelProcessing.SetApartmentState(System.Threading.ApartmentState.STA);
                    ParallelProcessing.Priority = System.Threading.ThreadPriority.AboveNormal;
                    ParallelProcessing.IsBackground = true;
                    ParallelProcessing.Start();
                    break;

                case "3":

                    ParallelProcessing = new System.Threading.Thread(delegate ()
                    {
                        Contacts_Download_Operation(data_receiver_stream, Id, Password);
                    });
                    ParallelProcessing.SetApartmentState(System.Threading.ApartmentState.STA);
                    ParallelProcessing.Priority = System.Threading.ThreadPriority.AboveNormal;
                    ParallelProcessing.IsBackground = true;
                    ParallelProcessing.Start();
                    break;

                case "4":

                    ParallelProcessing = new System.Threading.Thread(delegate ()
                    {
                        First_Institution_Contacts_Picture_Download_Operation(data_receiver_stream, Id, Password);
                    });
                    ParallelProcessing.SetApartmentState(System.Threading.ApartmentState.STA);
                    ParallelProcessing.Priority = System.Threading.ThreadPriority.AboveNormal;
                    ParallelProcessing.IsBackground = true;
                    ParallelProcessing.Start();
                    break;

                case "5":

                    ParallelProcessing = new System.Threading.Thread(delegate ()
                    {
                        Second_Institution_Contacts_Picture_Download_Operation(data_receiver_stream, Id, Password);
                    });
                    ParallelProcessing.SetApartmentState(System.Threading.ApartmentState.STA);
                    ParallelProcessing.Priority = System.Threading.ThreadPriority.AboveNormal;
                    ParallelProcessing.IsBackground = true;
                    ParallelProcessing.Start();
                    break;

                case "6":

                    ParallelProcessing = new System.Threading.Thread(delegate ()
                    {
                        Download_Material_File_Names(data_receiver_stream, Subject, Week, Id, Password);
                    });
                    ParallelProcessing.SetApartmentState(System.Threading.ApartmentState.STA);
                    ParallelProcessing.Priority = System.Threading.ThreadPriority.AboveNormal;
                    ParallelProcessing.IsBackground = true;
                    ParallelProcessing.Start();
                    break;

                case "7":

                    ParallelProcessing = new System.Threading.Thread(delegate ()
                    {
                        Get_File_Size_Of_The_Material_To_Be_Downloaded(data_receiver_stream, Subject, Week, FileName, Id, Password);
                    });
                    ParallelProcessing.SetApartmentState(System.Threading.ApartmentState.STA);
                    ParallelProcessing.Priority = System.Threading.ThreadPriority.AboveNormal;
                    ParallelProcessing.IsBackground = true;
                    ParallelProcessing.Start();
                    break;

                case "8":

                    ParallelProcessing = new System.Threading.Thread(delegate ()
                    {
                        Student_Log_In(data_receiver_stream, Id, Password);
                    });
                    ParallelProcessing.SetApartmentState(System.Threading.ApartmentState.STA);
                    ParallelProcessing.Priority = System.Threading.ThreadPriority.AboveNormal;
                    ParallelProcessing.IsBackground = true;
                    ParallelProcessing.Start();
                    break;


                case "9":

                    ParallelProcessing = new System.Threading.Thread(delegate ()
                    {
                        Student_Log_Out(data_receiver_stream, Id, Password);
                    });
                    ParallelProcessing.SetApartmentState(System.Threading.ApartmentState.STA);
                    ParallelProcessing.Priority = System.Threading.ThreadPriority.AboveNormal;
                    ParallelProcessing.IsBackground = true;
                    ParallelProcessing.Start();
                    break;


                case "10":

                    ParallelProcessing = new System.Threading.Thread(delegate ()
                    {
                        Student_Register(data_receiver_stream, Id, Password);
                    });
                    ParallelProcessing.SetApartmentState(System.Threading.ApartmentState.STA);
                    ParallelProcessing.Priority = System.Threading.ThreadPriority.AboveNormal;
                    ParallelProcessing.IsBackground = true;
                    ParallelProcessing.Start();
                    break;
            }
        }


        private static async void Profile_Picture_Download_Operation(Socket client, string Id, string Password)
        {
            client.SendBufferSize = 180000;

            Manage_Log_In<string, string> verify_log_in = new Manage_Log_In<string, string>();
            var result = await verify_log_in.Verify_If_Logged_In(Id, Password);

            switch(result)
            {
                case true:
                    Mange_Profile_Section<string, string> profile_picture_data_stream = new Mange_Profile_Section<string, string>();
                    var retrieved_data = await profile_picture_data_stream.Download_Profile_Picture(Id);
                    client.Send(retrieved_data);
                    break;
            }
           

            GC.Collect(2, GCCollectionMode.Forced, false);
        }


        private static async void Grades_Download_Operation(Socket client, string Subject, string Id, string Password)
        {
            client.SendBufferSize = 180000;
           
             Manage_Log_In<string, string> verify_log_in = new Manage_Log_In<string, string>();
            var result = await verify_log_in.Verify_If_Logged_In(Id, Password);

            switch (result)
            {
                case true:

                    Manage_Grades<string, string> manage_grades = new Manage_Grades<string, string>();
                    var retrieved_data = await manage_grades.Download_Grades(Subject, Id);
                    var Encoded_Retrieved_Data = Encoding.ASCII.GetBytes(retrieved_data);

                    client.Send(Encoded_Retrieved_Data);
                    break;
            }
        }


        private static async void Material_Download_Operation(Socket client, string Subject, string Week, string FileName, string Id, string Password)
        {
            client.SendBufferSize = 180000;

            Manage_Log_In<string, string> verify_log_in = new Manage_Log_In<string, string>();
            var result = await verify_log_in.Verify_If_Logged_In(Id, Password);

            switch (result)
            {
                case true:
                    Manage_Material_Section<string, string, string> material_Section = new Manage_Material_Section<string, string, string>();
                    var retrieved_data = await material_Section.DownloadMaterial(Subject, Week, FileName);
                    client.Send(retrieved_data);
                    break;
            }
        }

        private static async void Contacts_Download_Operation(Socket client, string Id, string Password)
        {
            client.SendBufferSize = 180000;

            Manage_Log_In<string, string> verify_log_in = new Manage_Log_In<string, string>();
            var result = await verify_log_in.Verify_If_Logged_In(Id, Password);

            switch (result)
            {
                case true:
                    Manage_Contacts manage_Contacts = new Manage_Contacts();
                    var retrieved_data = await manage_Contacts.Download_Contacts();
                    var Encoded_Retrieved_Data = Encoding.ASCII.GetBytes(retrieved_data);

                    client.Send(Encoded_Retrieved_Data);
                    break;
            }
        }

        private static async void First_Institution_Contacts_Picture_Download_Operation(Socket client, string Id, string Password)
        {
            client.SendBufferSize = 180000;

            Manage_Log_In<string, string> verify_log_in = new Manage_Log_In<string, string>();
            var result = await verify_log_in.Verify_If_Logged_In(Id, Password);

            switch (result)
            {
                case true:
                    Manage_Contacts manage_Contacts = new Manage_Contacts();
                    var retrieved_data = await manage_Contacts.Download_First_Institution_Contact_Pictures();
                    client.Send(retrieved_data);
                    break;
            }
        }

        private static async void Second_Institution_Contacts_Picture_Download_Operation(Socket client, string Id, string Password)
        {
            client.SendBufferSize = 180000;

            Manage_Log_In<string, string> verify_log_in = new Manage_Log_In<string, string>();
            var result = await verify_log_in.Verify_If_Logged_In(Id, Password);

            switch (result)
            {
                case true:
                    Manage_Contacts manage_Contacts = new Manage_Contacts();
                    var retrieved_data = await manage_Contacts.Download_Second_Institution_Contact_Pictures();
                    client.Send(retrieved_data);
                    break;
            }
        }

        private static async void Download_Material_File_Names(Socket client, string Subject, string Week, string Id, string Password)
        {
            client.SendBufferSize = 180000;

            Manage_Log_In<string, string> verify_log_in = new Manage_Log_In<string, string>();
            var result = await verify_log_in.Verify_If_Logged_In(Id, Password);

            switch (result)
            {
                case true:
                    Manage_Material_Section<string, string, string> material_Section = new Manage_Material_Section<string, string, string>();
                    var retrieved_data = await material_Section.LoadMaterials(Subject, Week);
                    var Encoded_Retrieved_Data = Encoding.ASCII.GetBytes(retrieved_data);

                    client.Send(Encoded_Retrieved_Data);
                    break;
            }
        }

        private static async void Get_File_Size_Of_The_Material_To_Be_Downloaded(Socket client, string Subject, string Week, string FileName, string Id, string Password)
        {
            client.SendBufferSize = 180000;

            Manage_Log_In<string, string> verify_log_in = new Manage_Log_In<string, string>();
            var result = await verify_log_in.Verify_If_Logged_In(Id, Password);

            switch (result)
            {
                case true:
                    Manage_Material_Section<string, string, string> material_Section = new Manage_Material_Section<string, string, string>();
                    var retrieved_data = await material_Section.DownloadMaterialFileSize(Subject, Week, FileName);
                    var Encoded_Retrieved_Data = Encoding.ASCII.GetBytes(retrieved_data);

                    client.Send(Encoded_Retrieved_Data);
                    break;
            }
        }

        private static async void Student_Log_In(Socket client, string Id, string Password)
        {
            client.SendBufferSize = 180000;
            Manage_Log_In<string, string> manage_log_In = new Manage_Log_In<string, string>();
            var log_in_result =  await manage_log_In.Log_In(Id, Password);

            switch(log_in_result)
            {
                case true:
                    client.Send(Encoding.ASCII.GetBytes("[OK]"));
                    break;

                case false:
                    client.Send(Encoding.ASCII.GetBytes("[NO]"));
                    break;
            }
        }

        private static async void Student_Register(Socket client, string Id, string Password)
        {
            client.SendBufferSize = 180000;
            Manage_Registration<string, string> manage_registration = new Manage_Registration<string, string>();
            var log_in_result = await manage_registration.Register(Id, Password);

            switch (log_in_result)
            {
                case true:
                    client.Send(Encoding.ASCII.GetBytes("[OK]"));
                    break;

                case false:
                    client.Send(Encoding.ASCII.GetBytes("[NO]"));
                    break;
            }
        }

        private static async void Student_Log_Out(Socket client, string Id, string Password)
        {
            client.SendBufferSize = 180000;
            Manage_Log_Out<string, string> manage_log_In = new Manage_Log_Out<string, string>();
            var log_in_result = await manage_log_In.Log_Out(Id, Password);

            switch (log_in_result)
            {
                case true:
                    client.Send(Encoding.ASCII.GetBytes("[OK]"));
                    break;

                case false:
                    client.Send(Encoding.ASCII.GetBytes("[NO]"));
                    break;
            }
        }
    }
}
