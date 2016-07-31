using System;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;
using System.IO;
using Chocolate.SystemRing;

namespace Chocolate
{
    
    public class osvars
    {
        public const string confdir = Terminal.root_directory + "conf";
        public static bool livesession = false;
        public const string osver = "Beta 2.5.1";
        public const string osname = "Foxtrot";
        public const string oslogo = @"
________________________________________
    ___ _               _      _       
   / __| |_  ___ __ ___| |__ _| |_ ___ 
  | (__| ' \/ _ / _/ _ | / _` |  _/ -_)
   \___|_||_\___\__\___|_\__,_|\__\___|
________________________________________
            Powered by Cosmos, 
      The Operating System Toolkit
";
    }
    public class Kernel : Sys.Kernel
    {
        protected override void BeforeRun()
        {
            Console.Clear();
            AdvConsole.WriteEx("Welcome to\n" + osvars.oslogo, ConsoleColor.White, ConsoleColor.Blue, true, true);
            Console.CursorLeft = (Console.WindowWidth - osvars.oslogo.Length) / 2;
            Console.CursorTop = Console.WindowHeight / 2;
            Console.Write(osvars.oslogo);
            Console.WriteLine(osvars.osver + ", " + osvars.osname);
            Console.WriteLine("Press any key to continue!");
        }
        public static void FullInit()
        {
            if (osvars.livesession == false)
            {
                if (!Directory.Exists(osvars.confdir))
                {
                    Directory.CreateDirectory(osvars.confdir);
                    Console.WriteLine("Your configuration directory is empty, run 'config' to create one if you wish");
                }
                else
                {
                    string[] lines = System.IO.File.ReadAllLines(osvars.confdir + @"/conf.txt");
                    foreach (string line in lines)
                    {
                        if (line.StartsWith("sbgc "))
                        {
                            Applications.ColorChanger.ChangeBGC(line.Remove(0, 5));
                        }
                        else if (line.StartsWith("sfgc "))
                        {
                            Applications.ColorChanger.ChangeFGC(line.Remove(0, 5));
                        }
                        else if (line.StartsWith("echo "))
                        {
                            Console.WriteLine(line.Remove(0, 5));
                        }

                    }
                }
            }
            else
            {
                Console.WriteLine("Chocolate is running live. Skipping config usage.");
            }

        }
        protected override void Run()
        {
            Console.ReadKey(true);
            Console.Clear();
            Terminal.Setup();
        }
    }
}
