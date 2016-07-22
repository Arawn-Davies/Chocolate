using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cosmos;
using Sys = Cosmos.System;
using System.IO;
using Chocolate.SystemRing;

namespace Chocolate
{

    class Terminal
    {
        #region Init
        public static void Init()
        {
            Console.WriteLine("Initializing FileSystems...");
            var FileSys = new Sys.FileSystem.CosmosVFS();
            Sys.FileSystem.VFS.VFSManager.RegisterVFS(FileSys);
            FileSys.Initialize();
            SystemRing.GetCoreInfo.CoreInfo();
            Console.WriteLine("Filesystems Initialized.");
            Console.WriteLine("System dir separator: " + Sys.FileSystem.VFS.VFSManager.GetDirectorySeparatorChar());
            if (!Directory.Exists(@"0:\"))
            {
                osvars.livesession = true;
                SystemRing.ErrorHandler.Warning(2, "fsnf");
                Console.WriteLine("Chocolate could not find a valid FAT Filesystem on your main hard drive.");
                Console.WriteLine("Some Applications may not work, so for full functionality");
                Console.WriteLine("use a hard drive partition tool to create one.");
                Console.WriteLine("Chocolate will run in Live mode.");
            }
            else
            {
                osvars.livesession = false;
               
            }
        }
        #endregion
        public static string current_user;
        public static Cosmos.System.FileSystem.CosmosVFS FileSys = null;
        public static bool folder_exists(string path)
        {
            bool val = false;
            foreach (var dir in Directory.GetDirectories(current_directory))
            {
                if (path == dir)
                {
                    val = true;
                }
            }
            return val;
        }
        public static string current_directory = "0:\\";
        public static void Setup()
        {
            Init();
            if (osvars.livesession == true)
            {
                current_user = "liveuser";
            }
            else
            {
                current_user = "default";

            }
            AdvConsole.Clear();
            AdvConsole.Fill(ConsoleColor.Blue);
            AdvConsole.BackgroundColor = ConsoleColor.Blue;
            AdvConsole.ForegroundColor = ConsoleColor.White;
            AdvConsole.Clear();
            Console.WriteLine("Welcome to Chocolate!");
            Console.WriteLine("Use 'help' to see a list of commands.");
            Start();
        }
        public static void Start()
        {
            
            Console.Write(current_user + "@chocolate" + " " + current_directory + " $");
            string input = Console.ReadLine();
            SendCommand(input);
            Start();

        }
        public static void SendCommand(string input)
        {
            string lower = input.ToLower();
            string[] args = input.Split(' ');
            if (lower == "reboot")
            {
                AdvConsole.WriteLine("The system is going down for reboot NOW");
                Sys.Power.Reboot();
            }
            else if (lower.StartsWith("help"))
            {
                Applications.Help.help();
            }
            if (lower.StartsWith("cd "))
            {
                #region CD
                if (osvars.livesession == true)
                {
                    Chocolate.SystemRing.ErrorHandler.Warning(2, "live");
                }
                else
                {
                    string arg = input.Remove(0, 3);
                    if (arg == "..")
                    {
                        var cdir = FileSys.GetDirectory(current_directory);
                        string pdir = cdir.mParent.mName;
                        if (!string.IsNullOrEmpty(pdir))
                        {
                            current_directory = pdir;
                        }
                    }
                    else if (folder_exists(arg))
                    {
                        Directory.SetCurrentDirectory(current_directory);
                        current_directory = current_directory + arg;
                    }
                    else if (!folder_exists(arg))
                    {
                        Console.WriteLine("Directory: " + arg + " : does not exist");
                    }
                    else
                    {
                        Console.WriteLine(current_directory);
                    }
                }
                #endregion 
            }
            else if (lower.StartsWith("cocoaview "))
            {
                #region cocoaview
                if (osvars.livesession == true)
                {

                }
                else
                {
                    string fname = input.Remove(0, 10);
                    if (File.Exists(current_directory + fname))
                    {
                        Console.WriteLine(File.ReadAllText(current_directory + fname));
                    }
                    else
                    {
                        Console.WriteLine("cocoaview: File Does not exist");
                    }
                }
                #endregion
            }
            else if (lower.StartsWith("dir"))
            {
                #region DIR
                if (osvars.livesession == true)
                {

                }
                else
                {
                    string[] folders = Directory.GetDirectories(current_directory);

                    foreach (var folder in folders)
                    {
                        Console.WriteLine(folder);
                    }
                }
                #endregion
            }
            else if (lower.StartsWith("mkdir "))
            {
                #region mkdir
                if (osvars.livesession == true)
                {
                    ErrorHandler.Warning(2, "live");
                }
                else
                {
                    string dirname = input.Remove(0, 6);
                    if (Directory.Exists(current_directory + dirname))
                    {
                        Console.WriteLine("Directory already exists:");
                    }
                    else
                    {
                        Directory.CreateDirectory(current_directory + dirname);
                    }
                }
                #endregion
            }
            else if (lower.StartsWith("ls"))
            {
                string[] files = Directory.GetFiles(current_directory);

                foreach (var file in files)
                {
                    Console.WriteLine(file);
                }
            }
            else if (lower.StartsWith("echo"))
            {
                #region echo
                try
                {
                    Console.WriteLine(input.Remove(0, 5));
                }
                catch (Exception ex)
                {
                    Console.WriteLine("echo: " + ex.Message);
                }
                #endregion
            }
            #region shellextras
            else if (lower == "")
            {
            }
            else
            {
                Console.WriteLine("Error: " + lower + " is not a recognised command, please see the help command.");
            }
            #endregion
        }

    }
}
