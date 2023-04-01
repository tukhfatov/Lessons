using HelloWorld.Services;
using System;

namespace HelloWorld
{
    class Program
    {
        static void Main(string[] args)
        {

            BankomatService bs = new BankomatService();
            bs.Init();
            bs.Start();


        }
    }
}
