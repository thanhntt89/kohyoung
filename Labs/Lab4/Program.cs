using System;
using System.IO;

namespace Lab4
{
    class Program
    {
        //Path file log
        static string logPath = string.Format("{0}\\test.log", Path.GetDirectoryName(
System.Reflection.Assembly.GetExecutingAssembly().Location));
        static void Main(string[] args)
        {
            //Check file not exist
            if (!File.Exists(logPath))
            {
                Console.WriteLine(string.Format("File: {0} not exist!!!!", logPath));
                Console.ReadLine();
                return;
            }

            //Reading all data from file
            string allData = File.ReadAllText(logPath);
            //Print data
            Console.WriteLine(allData);
            Console.ReadLine();
        }
    }
}
