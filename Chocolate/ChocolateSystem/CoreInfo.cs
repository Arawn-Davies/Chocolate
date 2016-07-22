using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cosmos.HAL;
using Sys = Cosmos.System;

namespace Chocolate.SystemRing
{
    public class GetCoreInfo
    {
        public static void CoreInfo()
        {
            Console.WriteLine($@"System Info:
:Number of FAT Partitions: {Sys.FileSystem.VFS.VFSManager.GetVolumes().Count}
:Current date and time is: {SystemInfo.Time.ToString()}");
        }
    }
    public class DateTime
    {
        public int Second { get; set; }
        public int Minute { get; set; }
        public int Hour { get; set; }
        public int Day { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public int DayOfTheWeek { get; set; }

        public string MonthName
        {
            get
            {
                string[] months = { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
                return months[Month - 1];
            }
        }

        public override string ToString()
        {
            return $"{MonthName} {Day}, {Year} at {Hour}:{Minute}:{Second}.";
        }
    }

    public static class SystemInfo
    {
        public static DateTime Time
        {
            get
            {
                var dt = new DateTime();
                dt.Second = RTC.Second;
                dt.Minute = RTC.Minute;
                dt.Hour = RTC.Hour;
                dt.Month = RTC.Month;
                dt.Year = RTC.Year;
                dt.Day = RTC.DayOfTheMonth;
                dt.DayOfTheWeek = RTC.DayOfTheWeek;
                return dt;
            }
        }
    }
}
