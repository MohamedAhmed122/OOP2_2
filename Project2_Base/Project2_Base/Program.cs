using System;
using System.Runtime.InteropServices;
using BankLibrary.Models;
using BankLibrary.Services;
using DocumentFormat.OpenXml.Bibliography;

namespace Project2_Base
{
    class Program
    {
        private static readonly ConsoleNotifier Notifier = new ConsoleNotifier();
        static void Main(string[] args)
        {

            var bank = new Bank<Account>("UnitBank", Notifier);
            var alive = true;
            while (alive)
            {
                var color = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.DarkGreen; // commands with green color
                Console.WriteLine("1. Open account \t 2. Withdraw funds  \t 3. Add money");
                Console.WriteLine("4. Close account \t 5. SkipDay \t 6. Close program");
                Console.WriteLine("Enter action number:");
                Console.ForegroundColor = color;
                try
                {
                    var command = Convert.ToInt32(Console.ReadLine());

                    switch (command)
                    {
                        case 1:
                            OpenAccount(bank);
                            break;
                        case 2:
                            Withdraw(bank);
                            break;
                        case 3:
                            Put(bank);
                            break;
                        case 4:
                            CloseAccount(bank);
                            break;
                        case 5:
                            break;
                        case 6:
                            alive = false;
                            continue;
                    }
                    bank.CalculatePercentage();
                }
                catch (Exception ex)
                {
                    // Error by red color
                    color = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ex.Message);
                    Console.ForegroundColor = color;
                }
            }
            Console.ReadLine();
        }

        private static object Prompt<T>(T v, ConsoleColor yellow)
        {
            throw new NotImplementedException();
        }

        private static void OpenAccount(Bank<Account> bank)
		{
			Console.WriteLine("Enter money amount for creation account:");

			var sum = Convert.ToDecimal(Console.ReadLine());
			Console.WriteLine("Choose account type: 1. Classic  2. Deposit");

            var type = Convert.ToInt32(Console.ReadLine());

			var accountType = type == 2 ? AccountType.Deposit : AccountType.Ordinary;

			bank.Open(accountType, sum); 
		}

		private static void Withdraw(Bank<Account> bank)
		{
			Console.WriteLine("Enter sum for withdrawing:");

			var sum = Convert.ToDecimal(Console.ReadLine());
			Console.WriteLine("Enter account id:");
			var id = Convert.ToInt32(Console.ReadLine());

			bank.Withdraw(sum, id);
		}

		private static void Put(Bank<Account> bank)
		{
			Console.WriteLine("Enter sum which need to add:");
			var sum = Convert.ToDecimal(Console.ReadLine());
			Console.WriteLine("Enter account id:");
			var id = Convert.ToInt32(Console.ReadLine());
			bank.Put(sum, id);
		}

		private static void CloseAccount(Bank<Account> bank)
		{
			Console.WriteLine("Enter account id, which need to close:");
			var id = Convert.ToInt32(Console.ReadLine());

			bank.Close(id);
		}
       

    }
}
