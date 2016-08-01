using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CocoaShell
{
    class cpview
    {
        public static void Run()
        {
            Console.WriteLine("Cocoapad Viewer\n");
            Console.Write(cpedit.savedtext);
            Console.CursorTop = Console.CursorTop + 1;
            Console.CursorLeft = 0;
        }
    }
}
