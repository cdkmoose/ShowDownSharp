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

            //int result = ldr.ProcessFile(@"D:\github-repos\showdownsharp\showdown retro database modified.csv", false);
            //int res2 = ldr.ProcessPUErrors(@"D:\github-repos\showdownsharp\corrected 02.csv");
            int result = ldr.ProcessFile(@"D:\github-repos\showdownsharp\MLB Showdown 2014.csv", true);

            Console.WriteLine(result.ToString() + " records.");

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
