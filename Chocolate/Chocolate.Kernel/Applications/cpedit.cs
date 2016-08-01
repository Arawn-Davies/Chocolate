using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chocolate;
using System.IO;

namespace CocoaShell
{
    class cpedit
    {
        public static string text = "";
        public static string savedtext = "";
        public static void Run(string file)
        {
            Console.WriteLine("--|:Welcome to Cocoapad Editor:|--");
            Console.WriteLine("Cocoapad is a multi line text editor you can use to create many files.");
            Console.WriteLine("Once you have finished you can type 'save' to save your file or 'end' to close without saving.");
            text = "";
            string line;
            var num = 1;
            while (true)
            {
                Console.Write(num + ":");
                num = num + 1;
                line = Console.ReadLine();
                if (line == "end")
                {
                    Console.WriteLine("Would you like to save first?");
                    string notsaved = Console.ReadLine();
                    if (notsaved == "y")
                    {
                        File.WriteAllText(Terminal.current_directory + file, text);
                        savedtext = text;
                        Cocoashell.Run();
                    }
                    else if (notsaved == "n") 
                    {
                        Cocoashell.Run();
                    }
                    else
                    {

                    }
                    
                }
                if (line == "save")
                {
                    File.CreateText(Terminal.current_directory + @"\" + file);
                    File.WriteAllText(Terminal.current_directory + @"\" + file, text);
                    cpedit.savedtext = cpedit.text;
                    Cocoashell.Run();
                }
                cpedit.text = cpedit.text + (Environment.NewLine + line);
            }

        }
    }
}