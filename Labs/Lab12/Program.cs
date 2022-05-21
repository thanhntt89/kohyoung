using System;
using System.Threading;

namespace Lab12
{
    class Program
    {
        static void Main(string[] args)
        {
            //Create thread Odd
            Thread tOdd = new Thread(Odd);
            Thread tEven = new Thread(Even);
            //Run thread Odd
            tOdd.Start();
            //Run thread print Even
            tEven.Start();
            //Wait for all thread finished
            tEven.Join();
            tOdd.Join();          

            Console.WriteLine("All thread terminated!!!");

            //Keep open console
            Console.ReadLine();
        }

        /// <summary>
        /// Thread print Even
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
                Thread.Sleep(1000);
            }
        }

        /// <summary>
        /// Thread print Odd
        /// </summary>
        static void Odd()
        {
            for (int i = 1; i <= 10; i++)
            {
                //Lab 2: Break thread when value equal 5
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
