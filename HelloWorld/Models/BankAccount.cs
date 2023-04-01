using System;

namespace HelloWorld.Models
{
    public class BankAccount
    {

        public string Number { get; set; }

        public string Name { get; set; }

        public string Pin { get; set; }

        public long Balance { get; set; }

        public void Deposit(long amount)
        {
            Balance += amount;
        }

        public void Withdraw(long amount)
        {
            Balance -= amount;
        }
    }
}
