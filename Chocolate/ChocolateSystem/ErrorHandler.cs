using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chocolate;

namespace Chocolate.SystemRing
{
    public class ErrorHandler
    {
        public static string message = "";
        public static string specific = "";
        public static void SpitOut()
        {
            const string errmsg = @"
_________________________________
|                                |
|       Message from system!     |
|    Something bad may happen!   |
|________________________________|
";
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(errmsg);
        }
        public static void Warning(int warnlevel, string type)
        {
            
            if (warnlevel == 1)
            {
                message = "No Permission rights";
                specific = "The currently logged in user is\nunable to perform this action.";
            }
            else if (warnlevel == 2)
            {
                message = "FAT FileSystem Error";
                if (type == "fsnf")
                {
                    specific = "File/Directory not found!";
                }
                else if (type == "live")
                {
                    specific = "You are running Chocolate as a Live user\nFileSystem Commands are disabled.";
                }
            }
            else if (warnlevel == 3)
            {

            }
            
            const string warn = @"
_________________________________
|                                |
|            Warning!            |
|    Something bad may happen!   |
|________________________________|
";
            Console.BackgroundColor = ConsoleColor.Cyan;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine(warn);
            Console.WriteLine(":" + ErrorHandler.message + ":");
            Console.WriteLine(specific);
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey(true);
        }
        public static void Critical(string critical)
        {
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.ForegroundColor = ConsoleColor.White;
            const string fatal = @"
_________________________________
|                                |
|              FATAL  !          |
|   Something bad has happened!  |
|________________________________|
";
            Console.WriteLine(fatal);
            Console.WriteLine(":" + critical + ":");
            Console.WriteLine("Press any key to reboot...");
            Console.ReadKey(true);
            Cosmos.System.Power.Reboot();
        }
    }
}
