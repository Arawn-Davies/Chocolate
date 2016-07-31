/* 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CocoaShell
{
    class CocoaScript
    {
        public static void Execute(string scriptname)
        {
            if (scriptname.EndsWith(".cscript"))
            {
                foreach (string line in File.ReadLines(@"C:/Users/azama/Desktop/" + scriptname))
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
*/