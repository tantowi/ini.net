using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tantowi.Ini.Test
{
    class MainClass
    {

        static void Main(string[] args)
        {
            try
            {
                int n = args.Length;
                IniFileTest test = new IniFileTest();
                //test.WriteTest1();
                //test.ReadTest1();
                //test.ReadTest2();
                //test.ReadTest3();

                //test.UnicodeTest1();
            }
            catch (AssertionException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
