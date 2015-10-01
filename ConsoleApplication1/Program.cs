using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using DS.Showdown.DbLibrary;


namespace DS.Showdown.CardLoader
{
    class Program
    {
        static void Main(string[] args)
        {
            Loader ldr = new Loader();

            //int result = ldr.ProcessFile(@"f:\projects\showdownsharp\showdown retro database modified.csv");
            int res2 = ldr.ProcessPUErrors(@"c:\projects\showdownsharp\corrected 02.csv");

            Console.WriteLine(res2.ToString() + " records.");

            Console.ReadLine();
        }

    }

    struct Range
    {
        public int low;
        public int high;

        public Range(int l, int h)
        {
            low = l;
            high = h;
        }
    }
}
