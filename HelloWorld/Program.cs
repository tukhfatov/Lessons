using HelloWorld.Services;
using HelloWorld.Utils;
using System;

namespace HelloWorld
{
    class Program
    {
        static void Main(string[] args)
        {

            ConsoleHelper.PrintGreeting();

            BankomatService bs = new BankomatService();
            bs.Init();
            bs.Start();


        }
    }
}
