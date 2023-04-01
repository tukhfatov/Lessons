using HelloWorld.Models;
using HelloWorld.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace HelloWorld.Services
{
    public class BankomatService
    {
        private List<BankAccount> _bankAccounts;
        private const string _filePath = @"C:\Agu\bankomat.txt";
        private const string _jsonFilePath = @"C:\Agu\bankomat.json";

        public void Init()
        {
            Console.WriteLine("ATM Initializing..... ");
            _bankAccounts = new List<BankAccount>();

            string input = File.ReadAllText(_jsonFilePath);

            _bankAccounts = JsonSerializer.Deserialize<List<BankAccount>>(input);
            
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
                ConsoleHelper.PrintError("Account doesn't exist");
                goto Start;
            }

            BankAccount ba = _bankAccounts.FirstOrDefault(b => b.Number == number);
            Console.WriteLine($"Hi {ba.Name}");
            Console.WriteLine("Please enter pin: ");
            string pin = Console.ReadLine();

            if (!ba.Pin.Equals(pin))
            {
                ConsoleHelper.PrintError("Incorrect pin");
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
            string output = JsonSerializer.Serialize<List<BankAccount>>(_bankAccounts);
            File.WriteAllText(_jsonFilePath, output);
        }

        

        private void PrintBalance(BankAccount ba)
        {
            ConsoleHelper.PrintSuccess($"Your balance: {ba.Balance}");
        }

        private void Deposit(BankAccount ba)
        {
            Console.WriteLine("Enter amount to deposit: ");
            long amount = Int64.Parse(Console.ReadLine());
            ba.Deposit(amount);
            ConsoleHelper.PrintSuccess("Thank you. Deposit saved");
        }

        private void Withdraw(BankAccount ba)
        {
            Console.WriteLine("Enter amount to withdraw: ");
            long amount = Int64.Parse(Console.ReadLine());
            if ( amount > ba.Balance)
            {
                ConsoleHelper.PrintError("Balance too low");
                return;
            }
            ba.Withdraw(amount);
            ConsoleHelper.PrintSuccess("Thank you. Printing your money");
        }
    }
}
