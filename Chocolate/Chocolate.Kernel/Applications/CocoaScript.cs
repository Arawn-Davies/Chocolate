
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Chocolate;

namespace CocoaShell
{
    class CocoaScript
    {
        public static void Execute(string scriptname)
        {
            if (scriptname.EndsWith(".ccs"))
            {
                string[] lines = File.ReadAllLines(Terminal.current_directory + scriptname);
                foreach (string line in lines)
                {
                    Cocoashell.SendCMD(line);
                }
            }
            else
            {
                Console.WriteLine("Not a valid Cocoascript file.");
            }

            Cocoashell.isscript = false;
        }
    }
}