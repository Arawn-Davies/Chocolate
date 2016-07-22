using System;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;
using System.IO;
using Chocolate.SystemRing;

namespace Chocolate
{
    
    public class osvars
    {
        public static bool livesession;
        const string osver = "Beta 5.0";
        const string osname = "Echo";
        public const string oslogo = @"
________________________________________
    ___ _               _      _       
   / __| |_  ___ __ ___| |__ _| |_ ___ 
  | (__| ' \/ _ / _/ _ | / _` |  _/ -_)
   \___|_||_\___\__\___|_\__,_|\__\___|
________________________________________
            Powered by Cosmos, 
      The Operating System Toolkit
";
    }
    public class Kernel : Sys.Kernel
    {
        
        protected override void BeforeRun()
        {
            Console.Clear();
            AdvConsole.WriteLineEx("Welcome to", ConsoleColor.White, ConsoleColor.Blue, true, true);
            AdvConsole.WriteLineEx(osvars.oslogo, ConsoleColor.White, ConsoleColor.Blue, true, true);
            Console.WriteLine("Press any key to continue!");
        }
        
        protected override void Run()
        {
            Console.ReadKey(true);
            Terminal.Setup();
        }
    }
}
