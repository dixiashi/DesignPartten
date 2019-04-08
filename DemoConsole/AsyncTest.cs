using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoConsole
{
    class AsyncTest
    {
        static void Main01()
        {
            TestTask();

            Console.ReadLine();
        }

        public static async Task Task1()
        {
            await Task.Factory.StartNew(() =>
            {
                Console.WriteLine($"Start: {nameof(Task1)} {DateTime.Now.ToString("yyyyMMddHHmmsss.fff")}");
                System.Threading.Thread.Sleep(10 * 1000);
                Console.WriteLine($"End: {nameof(Task1)} {DateTime.Now.ToString("yyyyMMddHHmmsss.fff")}");

                throw new Exception("*********");
            });
        }

        public static async Task Task2()
        {
            await Task.Factory.StartNew(() =>
            {
                Console.WriteLine($"Start: {nameof(Task2)} {DateTime.Now.ToString("yyyyMMddHHmmsss.fff")}");
                System.Threading.Thread.Sleep(10 * 1000);
                Console.WriteLine($"End: {nameof(Task2)} {DateTime.Now.ToString("yyyyMMddHHmmsss.fff")}");

                throw new Exception("*********");

            });
        }

        public static void TestTask()
        {
            Console.WriteLine($"Start TestTask");

            Task1();
            Task2();

            Console.WriteLine($"End TestTask");
        }
    }
}
