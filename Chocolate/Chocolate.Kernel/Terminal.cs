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

    public class Terminal
    {
        #region Init
        public static void Init()
        {
            Console.WriteLine("Initializing FileSystems...");
            var FileSys = new Sys.FileSystem.CosmosVFS();
            Sys.FileSystem.VFS.VFSManager.RegisterVFS(FileSys);
            FileSys.Initialize();
            GetCoreInfo.CoreInfo();
            Console.WriteLine("Filesystems Initialized.");
            Console.WriteLine("System dir separator: " + Sys.FileSystem.VFS.VFSManager.GetDirectorySeparatorChar());
            Console.Clear();
            if (!Directory.Exists(@"0:\"))
            {
                osvars.livesession = true;
                ErrorHandler.Warning(2, "fsnf");
                Console.WriteLine("Chocolate could not find a valid FAT Filesystem on your main hard drive.");
                Console.WriteLine("Some Applications may not work, so for full functionality");
                Console.WriteLine("use a hard drive partition tool to create one.");
                Console.WriteLine("Chocolate will run in Live mode.");
            }
            else
            {
                Console.WriteLine("Chocolate has detected a FAT FileSystem.");
                Console.WriteLine("You now have the choice to run Chocolate as a Live user or a fully featured user.");
                Console.WriteLine("Live users are only good for debugging purposes.");
                Console.WriteLine("Do you wish to run as a full user?");
                string uclive = Console.ReadLine();
                if (uclive == "yes")
                {
                    if (!Directory.Exists(usrs_dir))
                    {
                        Directory.CreateDirectory(usrs_dir);
                    }
                    osvars.livesession = false;
                }
                else if (uclive == "no")
                {
                    osvars.livesession = true;
                    current_user = "liveuser";
                }
            }
        }
        #endregion
        #region strings and bools
        public static string current_user;
        public static Sys.FileSystem.CosmosVFS FileSys = null;
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
        
        public const string root_directory = @"0:\";
        public static string usrs_dir = root_directory + "usr";
        public static string user_directory = usrs_dir + current_user;
        public static string current_directory = user_directory;
        #endregion

        public static void Setup()
        {
            Init();
            if(osvars.livesession == false)
            {
                UsrMgmt.CheckUser();
            }
            else if (osvars.livesession == true)
            {

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
            Console.Write(current_user + "@chocolate" + " $");
            try
            {
                string input = Console.ReadLine();
                SendCommand(input);
                Cosmos.Debug.Kernel.Plugs.Debugger.DoSend("whats going on");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey(true);
            }

            Start();
        }
        public static void SendCommand(string input)
        {
            var lower = input.ToLower();
            #region corecommands
            if (lower == "reboot")
            {
                AdvConsole.WriteLine("The system is going down for reboot NOW");
                Sys.Power.Reboot();
            }
            if (lower == "cocoashell")
            {
                CocoaShell.Cocoashell.Run();
            }
            else if (lower == "help")
            {
                Applications.Help.fullhelp();
            }
            else if (lower.StartsWith("help "))
            {
                Applications.Help.Specific(lower.Remove(0, 5));
            }
            else if (lower == "cls")
            {
                Console.Clear();
            }
            else if (lower.StartsWith("sfgc "))
            {
                Applications.ColorChanger.ChangeFGC(lower.Remove(0, 5));
            }
            else if (lower.StartsWith("sbgc "))
            {
                Applications.ColorChanger.ChangeBGC(lower.Remove(0, 5));
            }
            #endregion
#region CD
/*
else if (lower.StartsWith("cd "))
{


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
            var pdir = cdir.mParent.mName;
            if (cdir == FileSys.GetDirectory(root_directory))
            {
                Console.WriteLine("You are currently in the root directory.");
                Console.WriteLine("You cannot perform this operation in the root directory.");
            }
            else
            {
                cdir = pdir;
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

}*/
 #endregion
else if (lower.StartsWith("cocoaview "))
{
    #region cocoaview
    if (osvars.livesession == true)
    {
        ErrorHandler.Warning(2, "live");
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
            ErrorHandler.Warning(2, "fsnf");
        }
    }
    #endregion
}
else if (lower.StartsWith("dir"))
{
    #region DIR
    if (osvars.livesession == true)
    {
        ErrorHandler.Warning(2, "live");
    }
    else
    {
                    Console.WriteLine("Item Type\tTitle");
                    foreach (var dir in Directory.GetDirectories(current_directory))
                    {
                        try
                        {
                            Console.WriteLine("<Directory>\t" + dir);
                        }
                        catch
                        {

                        }
                    }
                    foreach (var dir in Directory.GetFiles(current_directory))
                    {
                        try
                        {
                            string[] sp = dir.Split(new[] { "." }, StringSplitOptions.RemoveEmptyEntries);
                            Console.WriteLine(sp[sp.Length - 1] + "\t" + dir);
                        }
                        catch
                        {

                        }
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
    #region ls
    if (osvars.livesession == true)
    {
        ErrorHandler.Warning(2, "live");
    }
    else
    {
        string[] files = Directory.GetFiles(current_directory);

        foreach (var file in files)
        {
            Console.WriteLine(file);
        }
    }
    #endregion
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
