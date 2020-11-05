using System;
using BankLibrary.Exceptions;
using BankLibrary.Interfaces;

namespace BankLibrary.Models
{
	public class Bank<T> where T : Account
	{
		T[] accounts;

		public string Name { get; private set; }
		private IEventNotifier Notifier { get; }
		public Bank(string name, IEventNotifier notifier)
		{
			this.Name = name;
			Notifier = notifier;

		}

		// Open new account in bank
		public void Open(AccountType accountType, decimal sum, Person person)
		{
			T newAccount = null;
			switch (accountType)
			{
				case AccountType.Ordinary:
					newAccount = new DemandAccount(sum, 1, person, Notifier) as T;
					break;
				case AccountType.Deposit:
					newAccount = new DepositAccount(sum, 40, person, Notifier) as T;
					break;
			}

			if (newAccount == null)
				throw new Exception("Errors during account creation");

			// add new account to accounts collections     
			if (accounts == null)
				accounts = new T[] { newAccount };
			else
			{
				T[] tempAccounts = new T[accounts.Length + 1];
				for (int i = 0; i < accounts.Length; i++)
					tempAccounts[i] = accounts[i];
				tempAccounts[^1] = newAccount;
				accounts = tempAccounts;
			}

			newAccount.Open();
		}
		//добавление средств на счет
		public void Put(decimal sum, int id)
		{
			T account = FindAccount(id);

			if (account == null)
			{
				throw new Exception("404. Account not found!");
			}

			account.Put(sum);
		}
		// вывод средств
		public void Withdraw(decimal sum, int id)
		{
			T account = FindAccount(id);
			if (account == null)
				throw new Exception("404. Account not found!");
			account.Withdraw(sum);
		}
		// закрытие счета
		public void Close(int id)
		{
			int index;
			T account = FindAccount(id, out index);

			if (account == null)
			{
				throw new Exception("404. Account not found!");
			}


			account.Close();

			if (accounts.Length <= 1)
				accounts = null;
			else
			{
				T[] tempAccounts = new T[accounts.Length - 1];
				for (int i = 0, j = 0; i < accounts.Length; i++)
				{
					if (i != index)
						tempAccounts[j++] = accounts[i];
				}
				accounts = tempAccounts;
			}
		}

		public void CalculatePercentage()
		{
			if (accounts == null)
			{
				return;
			}

			for (int i = 0; i < accounts.Length; i++)
			{
				T account = accounts[i];
				account.IncrementDays();
				account.Calculate();
			}
		}

		// Find account by id
		public T FindAccount(int id)
		{
			for (int i = 0; i < accounts.Length; i++)
			{
				if (accounts[i].Id == id)
					return accounts[i];
			}
			return null;
		}

		public T FindAccount(int id, out int index)
		{
			for (int i = 0; i < accounts.Length; i++)
			{
				if (accounts[i].Id == id)
				{
					index = i;
					return accounts[i];
				}
			}
			index = -1;
			return null;
		}


		public T FindAccount(string accNum)
		{
			for (int i = 0; i < accounts.Length; i++)
			{
				if (accounts[i].AccNum == accNum)
					return accounts[i];
			}
			throw new AccountNotFound(accNum);
		}

		public string FindAccountOwner(string accNum)
		{
			for (int i = 0; i < accounts.Length; i++)
			{
				if (accounts[i].AccNum == accNum)
					return accounts[i].Person.Name;
			}
			throw new AccountNotFound(accNum);
		}

		public void ShowAllAcoounts()
		{
			for (int i = 0; i < accounts.Length; i++)
			{
				Console.WriteLine("\n\n------------------------------------------------------");
				Console.WriteLine($"Accont ID         : {accounts[i].Id}");
				Console.WriteLine($"Accont Number     : {accounts[i].AccNum}");
				Console.WriteLine($"Accont Owner      : {accounts[i].Person.Name}");
				Console.WriteLine($"Accont Percentage : {accounts[i].Percentage}");
				Console.WriteLine("------------------------------------------------------\n\n");
			}
		}
	}
}
