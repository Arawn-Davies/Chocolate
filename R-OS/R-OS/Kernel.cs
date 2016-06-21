using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Sys = Cosmos.System;
using ROS;
using R_OS;

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
            Console.WriteLine("Filesystems Initialized.");
            Console.WriteLine("Welcome to R-OS");
            Console.WriteLine("Use 'help' to see a list of commands.");
            Console.WriteLine("System dir separator: " + Sys.FileSystem.VFS.VFSManager.GetDirectorySeparatorChar());
        }

        protected override void Run()
        {
            Console.Write(current_directory + " >");
            string input = Console.ReadLine();
            SendCommand(input);
        }
        /*
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
        */


        public void SendCommand(string input)
        {
            string[] args = input.Split(' ');
            if (input.StartsWith("reboot"))
            {
                Sys.Power.Reboot();
            }
            if (input.StartsWith("help"))
            {
                Console.WriteLine("Help:");
                Console.WriteLine("'echo' args  --> Prints the text in place of 'args' onto the console.");
                Console.WriteLine("'help'       --> Displays this help table.");
                Console.WriteLine("'reboot'     --> Reboots R-OS.");
                Console.WriteLine("'dir'        --> Prints a list of folders in the current directory");
                Console.WriteLine("'mkdir' args --> Creates a new directory in the current folder with the name 'args'");
                Console.WriteLine("'ls'         --> Prints a list of files in the current directory");

            }
            if (input.StartsWith("cd"))
            {
                if (input == "cd")
                {
                    Console.WriteLine(current_directory);
                }
                else
                {
                    var arg = input.Remove(0, 3);
                    if (System.IO.Directory.Exists(current_directory + arg) == true)
                    {
                        System.IO.Directory.SetCurrentDirectory(current_directory + arg);
                        current_directory = System.IO.Directory.GetCurrentDirectory();
                    }
                    else if (System.IO.Directory.Exists(current_directory + arg) == false)
                    {
                        Console.WriteLine("Directory: " + arg + " : does not exist");
                    }
                }
            }
            else if (input.StartsWith("dir"))
            {
                string[] folders = Directory.GetDirectories(current_directory);

                foreach (var folder in folders)
                {
                    Console.WriteLine(folder);
                }
            }
            else if (input.StartsWith("mkdir "))
            {
                string dirname = input.Remove(0, 6);
                System.IO.Directory.CreateDirectory(current_directory + dirname);
            }
            else if (input.StartsWith("ls"))
            {
                string[] files = Directory.GetFiles(current_directory);

                foreach (var file in files)
                {
                    Console.WriteLine(file);
                }
            }
            else if (input.StartsWith("echo"))
            {
                try
                {
                    Console.WriteLine(input.Remove(0, 5));
                }
                catch (Exception ex)
                {
                    Console.WriteLine("echo: " + ex.Message);
                }
            }
            else if (input == "")
            {
            }
            else
            {
                Console.WriteLine("Error: " + input + " is not a recognised command, please see the help command.");
            }
        }

    }
}
