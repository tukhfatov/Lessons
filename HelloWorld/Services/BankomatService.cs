using HelloWorld.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace HelloWorld.Services
{
    public class BankomatService
    {
        private List<BankAccount> _bankAccounts;
        private const string _filePath = @"C:\Agu\bankomat.txt";

        public void Init()
        {
            Console.WriteLine("ATM Initializing..... ");
            _bankAccounts = new List<BankAccount>();

            string[] inputs = File.ReadAllLines(_filePath);

            foreach(string input in inputs)
            {
                string[] items = input.Split(';');

                _bankAccounts.Add(new BankAccount
                {
                    Balance = Int32.Parse(items[3]),
                    Name = items[2],
                    Pin = items[1],
                    Number = items[0]
                });
            }

            Console.WriteLine("ATM ready to use.");
        }

        public bool IfAccountExist(string number)
        {
            return _bankAccounts.Any(b => b.Number == number);
        }

        public void Start()
        {
        Start:
            Console.WriteLine("Welcome to AGU Bank");
            Console.WriteLine("Please enter account number: ");
            string number = Console.ReadLine();

            if (!IfAccountExist(number))
            {
                PrintError("Account doesn't exist");
                goto Start;
            }

            BankAccount ba = _bankAccounts.FirstOrDefault(b => b.Number == number);
            Console.WriteLine($"Hi {ba.Name}");
            Console.WriteLine("Please enter pin: ");
            string pin = Console.ReadLine();

            if (!ba.Pin.Equals(pin))
            {
                PrintError("Incorrect pin");
                goto Start;
            }

        Action:
            Console.WriteLine("Please select action: ");
            Console.WriteLine("1: Withdraw ");
            Console.WriteLine("2: Deposit ");
            Console.WriteLine("3: Check balance ");
            Console.WriteLine("4: Exit");

            int action = Int32.Parse(Console.ReadLine());

            switch (action)
            {
                case 1:
                    Withdraw(ba);
                    goto Action;
                case 2:
                    Deposit(ba);
                    goto Action;
                case 3:
                    PrintBalance(ba);
                    goto Action;
                case 4:
                    UpdateFile();
                    goto Start;

            }
        }

        private void UpdateFile()
        {
            string[] outputs = new string[_bankAccounts.Count];
            int i = 0;
            foreach(var bankAccount in _bankAccounts)
            {
                outputs[i] = $"{bankAccount.Number};{bankAccount.Pin};{bankAccount.Name};{bankAccount.Balance}";
                i++;
            }

            File.WriteAllLines(_filePath, outputs);
        }

        private void PrintError(string text)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(text);
            Console.ForegroundColor = ConsoleColor.White;
        }

        private void PrintSuccess(string text)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(text);
            Console.ForegroundColor = ConsoleColor.White;
        }

        private void PrintBalance(BankAccount ba)
        {
            PrintSuccess($"Your balance: {ba.Balance}");
        }

        private void Deposit(BankAccount ba)
        {
            Console.WriteLine("Enter amount to deposit: ");
            long amount = Int64.Parse(Console.ReadLine());
            ba.Deposit(amount);
            PrintSuccess("Thank you. Deposit saved");
        }

        private void Withdraw(BankAccount ba)
        {
            Console.WriteLine("Enter amount to withdraw: ");
            long amount = Int64.Parse(Console.ReadLine());
            if ( amount > ba.Balance)
            {
                PrintError("Balance too low");
                return;
            }
            ba.Withdraw(amount);
            PrintSuccess("Thank you. Printing your money");
        }
    }
}
