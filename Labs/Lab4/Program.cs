using System;
using System.IO;
using System.Reflection;

namespace Lab4
{
    class Program
    {
        //Path file log
        static string logPath = string.Format("{0}\\test.log", Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
        static void Main(string[] args)
        {
            //Check file not exist
            if (!File.Exists(logPath))
            {
                Console.WriteLine(string.Format("File: {0} not exist!!!!", logPath));
                Console.ReadLine();
                return;
            }

            try
            {
                //Reading all data from file
                string allData = File.ReadAllText(logPath);
                //Print data
                Console.WriteLine(allData);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            Console.ReadLine();
        }
    }
}
