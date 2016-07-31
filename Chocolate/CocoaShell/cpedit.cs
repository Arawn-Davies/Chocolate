using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CocoaShell
{
    class cpedit
    {
        public static string text = "";
        public static string textSVG = "";
        public static void Run(string file)
        {
            Console.WriteLine("--|:Welcome to Cocoapad Editor:|--");
            Console.WriteLine("Cocoapad is a multi line text editor you can use to create many files.");
            Console.WriteLine("Once you have finished you can type 'save' to save your file or 'end' to close without saving.");
            cpedit.text = "";
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
                    if (notsaved == "y" | notsaved == "Yes" | notsaved == "yes" | notsaved == "Y")
                    {
                        File.WriteAllText(@"C:/Users/azama/Desktop/" + file, text);
                        cpedit.textSVG = cpedit.text;
                        break;
                    }
                    else if (notsaved == "n" | notsaved == "No" | notsaved == "no" | notsaved == "N") 
                    {
                        break;
                    }
                    else
                    {

                    }
                    
                }
                if (line == "save")
                {
                    File.WriteAllText(@"C:/Users/azama/Desktop/" + file, text);
                    cpedit.textSVG = cpedit.text;
                    break;
                }
                cpedit.text = cpedit.text + (Environment.NewLine + line);
            }

        }
    }
}