using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chocolate;

namespace Chocolate.Applications
{
    class Help
    {
        public static void help()
        {
            Console.Clear();
            Console.WriteLine("'echo' args  --> Prints the specified text onto the console.");
            Console.WriteLine("'help'       --> Displays this help table.");
            Console.WriteLine("'reboot'     --> Reboots Chocolate.");
            Console.WriteLine("'cls'        --> Clears the screen.");
            Console.WriteLine("'sfgc' args   --> Sets the screen foreground color");
            Console.WriteLine("'sbgc' args   --> Sets the screen background color");
            Console.WriteLine("Enter lowercase: Possible colors are :");
            const string ccolors = @"
black, blue, cyan, darkblue, darkcyan, darkgray
darkgreen, darkmagenta, darkred, darkyellow
gray, green, magenta, red, white, yellow";
            Console.WriteLine(ccolors);
            Console.WriteLine();
            if (Terminal.current_user != "liveuser")
            {
                Console.WriteLine("'dir'        --> Prints a list of folders in the current directory.");
                Console.WriteLine("'mkdir' args --> Creates a new directory with the specified name.");
                Console.WriteLine("'ls'         --> Prints a list of files in the current directory.");
                Console.WriteLine("'cd' args    --> Enters the specified directory.");
            }
            else
            {
                //Proceed
            }
        }
    }
}
