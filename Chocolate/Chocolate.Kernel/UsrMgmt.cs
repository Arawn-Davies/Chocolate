using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Chocolate
{
    class UsrMgmt
    {
        public static void NewUser(string usrname)
        {
            Directory.CreateDirectory(Terminal.usrs_dir + usrname);
            Console.WriteLine("Created new user directory: " + Terminal.usrs_dir + usrname);
            Console.WriteLine();
            Console.Write("Adding new user to user list...");
            File.Create(Terminal.root_directory + usrname);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("     Done!");
            Console.ForegroundColor = ConsoleColor.White;
            Terminal.current_user = usrname;
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey(true);
        }
        public static void PermCheck()
        {
            if (Terminal.current_user != Terminal.user_directory)
            {
                SystemRing.ErrorHandler.Warning(1, "oud");
            }
            else
            {

            }
        }
        public static void CheckUser()
        {
            Console.Clear();
            Console.WriteLine("User Logon:");
            Console.CursorTop = 5;
            Console.WriteLine("You can either log in as an existing user or create a new one.\n");
            Console.Write(">");
            string usrlogon = Console.ReadLine();
            if (!Directory.Exists(Terminal.usrs_dir + usrlogon))
            {
                NewUser(usrlogon);
            }
            else if (Directory.Exists(Terminal.usrs_dir + usrlogon))
            {
                Terminal.current_user = usrlogon;
            }
        }
        
    }
}
