using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace ConsoleApplication2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(DateTime.Now.ToString("dd-MMM-yyyy HH:mm:ss.fff"));
            Console.ReadKey();
        }
    }
}
