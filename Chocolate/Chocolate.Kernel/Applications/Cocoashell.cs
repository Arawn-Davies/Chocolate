using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CocoaShell
{
    class Cocoashell
    {
        public static string usrinput = "";
        public static bool isscript = false;
        public static void Run()
        {
            bool ready = true;
            while (ready == true)
            {
                if (isscript == false)
                {
                    Console.Write("/>");
                    string command = Console.ReadLine();
                    SendCMD(command);
                }
                else
                {
                   
                    
                }
            }
        }
        public static void SendCMD(string args)
        {
            string cmd = args.ToLower();
            if (cmd == "exit")
            {
                isscript = false;
                Chocolate.Terminal.Start();
            }
            if (cmd.StartsWith("cpedit "))
            {
                cpedit.Run(cmd.Remove(0, 7));
            }
            else if (cmd == "cpedit")
            {
                cpedit.Run("untitled.txt");
            }
            else if (cmd == "cpview")
            {
                cpview.Run();
            }
            
            else if (cmd.StartsWith("run "))
            {
                isscript = true;
                CocoaScript.Execute(cmd.Remove(0, 4));
            }
            
            else if (cmd.StartsWith("echo "))
            {
                try
                {
                    Console.WriteLine(args.Remove(0, 5));
                }
                catch (Exception ex)
                {
                    Console.WriteLine("echo: " + ex.Message);
                }
            }
            else if (cmd.StartsWith("ask strusr"))
            {
                usrinput = Console.ReadLine();
            }
            else if (cmd.StartsWith("set strusr "))
            {
                usrinput = cmd.Remove(0, 11);
            }
            else if (cmd == ".echo strusr")
            {
                Console.WriteLine(usrinput);
            }
            else if (cmd == "pause")
            {
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey(true);
            }
            else if (cmd.StartsWith("help "))
            {
                Help(cmd.Remove(0, 5));
            }
            else if (cmd == "clear")
            {
                Console.Clear();
            }
            else if (cmd == "help")
            {
                Console.WriteLine("Echo\tPrints out the specified text to the console.");
                Console.WriteLine("Clear\tClears the console of all text.");
                Console.WriteLine("cpedit\tRuns Cocoapad Editor, See 'help cplite' for more info.");
                Console.WriteLine("cpview\tRuns Cocoapad Viewer, See 'help cvlite' for more info.");
            }
            else if (cmd == ""){}
            else
            {
                Console.WriteLine(cmd + ":\tInvalid Command.");
            }
        }
        public static void Help(string topic)
        {
            if (topic == "echo")
            {
                Console.WriteLine("echo\tPrints out the specified text to the console.");
                Console.WriteLine("Usage:\techo <text>");
            }
            else if (topic == "clear")
            {
                Console.WriteLine("clear\tClears the console of all text.");
                Console.WriteLine("Usage:\tclear");
            }
            else if (topic == "cpedit")
            {
                Console.WriteLine("cpedit\tCocoapad Editor is a multi line texteditor you can use to create many files.");
                Console.WriteLine("If no filename is specified, cpedit will use'untitled.txt' as it's filename");
                Console.WriteLine("Usage:\tcpedit <filename>");
            }
            else if (topic == "cpview")
            {
                Console.WriteLine("cpview\tCocoapad Viewer is used to view files created using Cocoapad Editor.");
                Console.WriteLine("Usage:\tcpview");
            }
            else
            {
                Console.WriteLine(topic + ":\tCommand not found. See 'help' for a list.");
            }
        }
    }
}
