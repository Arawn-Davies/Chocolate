using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Sys = Cosmos.System;

namespace ROS
{
    public class Kernel : Sys.Kernel
    {
        string current_directory = "0:\\";

        protected override void BeforeRun()
        {
            Console.WriteLine("Initializing FileSystems...");
            var FS = new Sys.FileSystem.CosmosVFS();
            Sys.FileSystem.VFS.VFSManager.RegisterVFS(FS);
            FS.Initialize();
            Console.WriteLine("Welcome to R-OS");
            Console.WriteLine("Use 'help' to see a list of commands.");
            Console.WriteLine("System dir separator: " + Sys.FileSystem.VFS.VFSManager.GetDirectorySeparatorChar());
        }

        protected override void Run()
        {
            Console.Write(current_directory + "/>");
            var input = Console.ReadLine();
            Console.Write("\n" + input);
            if (input == "help")
            {
                Console.WriteLine("Help:");
                Console.WriteLine("'echo' args --> Prints the text in place of 'args' onto the console.");
                Console.WriteLine("'help'      --> Displays this help table.");
                Console.WriteLine("'reboot'    --> Reboots R-OS.");
            }
            else if (input == "dir")
            {
                Console.WriteLine("dir : Coming soon to R-OS!");
            }
            else if (seperate(input, 5) == "mkdir")
            {
                if (input == "mkdir")
                {
                    Console.Write("mkdir takes 1 argument");
                }
                else
                {
                    var arg = rseperate(input, 6, (input.Length - 1));
                    System.IO.Directory.CreateDirectory(arg);
                }
            }
            else if (input == "reboot")
            {
                Sys.Power.Reboot();
            }
            else if (seperate(input, 4) == "echo")
            {
                if (input == "echo")
                {
                    Console.Write("Echo takes 1 argument");
                }
                else
                {
                    var arg = rseperate(input, 5, (input.Length - 1));
                    echo(arg);
                }
            }
        }
        public static void echo(string arg)
        {
            Console.Write("\n" +arg + "\n");
        }
        public static string seperate(string input, int length)
        {
            var count = 0;
            string character;
            string output = "";
            while (count < length)
            {
                character = input[count].ToString();
                output = output + character;
                count = count + 1;
            }
            return output;
        }
        public static string rseperate(string input, int start, int end)
        {
            int count = 0;
            string character = "";
            string output = "";
            while (end >= start)
            {
                character = input[start].ToString();
                output = output.Insert(count, character);
                start = start + 1;
                count = count + 1;
            }
            return output;
        }

    }
}
