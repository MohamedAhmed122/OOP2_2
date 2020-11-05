using System;
using System.Runtime.InteropServices;
using BankLibrary.Models;
using BankLibrary.Services;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.InkML;

namespace Project2_Base
{
	class Program
	{
		private static readonly ConsoleNotifier Notifier = new ConsoleNotifier();
		static void Main(string[] args)
		{

			var bank = new Bank<Account>("UnitBank", Notifier);
			var bank2 = new Bank<Account>("Alpha Bank", Notifier);
		
			//
			var person = new BankLibrary.Models.Person("Mohamed Youssef", "23", "123456789", "mohamed@mohamed.com", "0245678");
			var outerloopAlive = true;
			while (outerloopAlive)
			{
				var color = Console.ForegroundColor;
				Console.ForegroundColor = ConsoleColor.DarkGreen; // commands with blue color
				Console.WriteLine($"1. To Enter Bank : {bank.Name}");
				Console.WriteLine($"2. To Enter Bank : {bank2.Name}");
				Console.WriteLine($"3. EXit");
				Console.WriteLine("Enter action number:");
				Console.ForegroundColor = color;
				var alive = true;
				int command;
				try
				{
					int choice = Convert.ToInt32(Console.ReadLine());
					switch (choice)
					{
						case 1:
							alive = true;
							while (alive)
							{
								color = Console.ForegroundColor;
								Console.ForegroundColor = ConsoleColor.DarkGreen; // commands with green color
								Console.WriteLine("1. Open account \t 2. Withdraw funds  \t 3. Add money");
								Console.WriteLine("4. Close account \t 5. SkipDay \t 6. Close program");
								Console.WriteLine("Enter action number:");
								Console.ForegroundColor = color;
								try
								{
									command = Convert.ToInt32(Console.ReadLine());

									switch (command)
									{
										case 1:
											OpenAccount(bank, person);
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
							break;
						case 2:
							alive = true;
							while (alive)
							{
								color = Console.ForegroundColor;
								Console.ForegroundColor = ConsoleColor.DarkGreen; // commands with green color
								Console.WriteLine("1. Open account \t 2. Withdraw funds  \t 3. Add money");
								Console.WriteLine("4. Close account \t 5. SkipDay \t 6. Close program");
								Console.WriteLine("Enter action number:");
								Console.ForegroundColor = color;
								try
								{
									command = Convert.ToInt32(Console.ReadLine());

									switch (command)
									{
										case 1:
											OpenAccount(bank2, person);
											break;
										case 2:
											Withdraw(bank2);
											break;
										case 3:
											Put(bank2);
											break;
										case 4:
											CloseAccount(bank2);
											break;
										case 5:
											break;
										case 6:
											alive = false;
											continue;
									}
									bank2.CalculatePercentage();
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
							break;
						case 3:
							outerloopAlive = false;
							break;
					}
				}
				catch (Exception e)
				{
					color = Console.ForegroundColor;
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine(e.Message);
					Console.ForegroundColor = color;
				}




				/*
				var alive = true;
				while (alive)
				{
					color = Console.ForegroundColor;
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
				*/
			}

		}

		private static object Prompt<T>(T v, ConsoleColor yellow)
		{
			throw new NotImplementedException();
		}

		private static void OpenAccount(Bank<Account> bank, BankLibrary.Models.Person person)
		{
			Console.WriteLine("Enter money amount for creation account:");

			var sum = Convert.ToDecimal(Console.ReadLine());
			Console.WriteLine("Choose account type: 1. Classic  2. Deposit");

			var type = Convert.ToInt32(Console.ReadLine());

			var accountType = type == 2 ? AccountType.Deposit : AccountType.Ordinary;

			bank.Open(accountType, sum, person);
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
