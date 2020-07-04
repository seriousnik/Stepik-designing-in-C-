using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memory.Timers
{
    public static class Timer
    {
        public static string Report;
        public static int counter = 0;
        private static int spaces = 0;
        public static Report Start()
        {
            counter++;
            Report += "                   : 0\n"; //19_  new string(' ', 19);  //15
            return new Report();
        }
        public static Report Start(string str)
        {
            counter++;
            Report += new string(' ', spaces) + str + new string(' ', 19 - spaces - str.Length) + ": " + str + "!" + "\n";
            spaces += 4;
            return new Report(str);
        }

    }
    public class Report : IDisposable
    {

        private string name;

        public Stopwatch stopWatch = new Stopwatch();
        public Report(string str)
        {
            name = str;
            stopWatch.Start();
        }
        public Report()
        {
            stopWatch.Start();
        }

        private bool isDisposed = false;
        ~Report()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool fromDisposeMethod)
        {
            if (!isDisposed)
            {
                stopWatch.Stop();
                TimeSpan ts = stopWatch.Elapsed;
                if (!String.IsNullOrEmpty(name))
                    Timer.Report = Timer.Report.Replace(name + "!", ts.Milliseconds.ToString());

                isDisposed = true;
            }
        }

    }


}
