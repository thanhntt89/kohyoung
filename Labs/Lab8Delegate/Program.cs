using System;

namespace Lab8Delegate
{
    class Program
    {
        //Define delegate 
        public delegate void DelegateShowMessage(string message);

        static void Main(string[] args)
        {
            //Instantiate the delegate.
            DelegateShowMessage showMessage = ShowMessage;           
            //Call delegate
            showMessage("Call delegate");

            //Call deleaget from function callback
            MethodWithCallback("Call delegate by callback!!!", showMessage);

            Console.ReadLine();
        }

        /// <summary>
        /// Define method for delegate
        /// </summary>
        /// <param name="message">message</param>
        static void ShowMessage(string message)
        {
            Console.WriteLine(message);
        }

        /// <summary>
        /// Method callback using for delegate
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="showMessage">Delegate</param>
        static void MethodWithCallback(string message, DelegateShowMessage showMessage)
        {
            showMessage(message);
        }
    }
}
