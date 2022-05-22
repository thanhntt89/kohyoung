using System;
using System.Threading;

namespace Lab12
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
