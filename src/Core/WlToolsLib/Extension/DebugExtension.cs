using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace WlToolsLib.Extension
{
    public static class Debug
    {
        public static void TimeOfShell(this Action action)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            if (action.NotNull())
            {
                action();
            }
            watch.Stop();
            Console.WriteLine("Elapsed: {0}", watch.Elapsed);
            Console.WriteLine("In milliseconds: {0}", watch.ElapsedMilliseconds);
            Console.WriteLine("In timer ticks: {0}", watch.ElapsedTicks);
        }
    }
}
