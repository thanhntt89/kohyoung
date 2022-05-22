using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lab2
{
    class Program
    {

        static void Main(string[] args)
        {
            //Create thread 
            Thread tOdd = new Thread(Odd);
            Thread tEven = new Thread(Even);

            //Run thread Odd
            tOdd.Start();
            //Run thread Even
            tEven.Start();

            //Waiting for all thread ending
            tOdd.Join();
            tEven.Join();

            Console.WriteLine("The program has been terminated!!!");

            //Keep open console
            Console.ReadLine();
        }

        /// <summary>
        /// Thread Even
        /// </summary>
        static void Even()
        {
            for (int i = 1; i <= 10; i++)
            {
                //Check value is even
                if (i % 2 == 0)
                {
                    Console.WriteLine("Even:" + i);
                }
                //Reduce cpu
                Thread.Sleep(1);
            }
        }

        /// <summary>
        /// Thread Odd
        /// </summary>
        static void Odd()
        {
            for (int i = 1; i <= 10; i++)
            {
                // When value equals 5 break 
                if (i == 5)
                    break;

                //Check value is odd
                if (i % 2 != 0)
                {
                    Console.WriteLine("Odd:" + i);
                }

                //Reduce cpu
                Thread.Sleep(1);
            }
        }
    }
}
