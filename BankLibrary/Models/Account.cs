using BankLibrary.Interfaces;
using System;
using System.Reflection.PortableExecutable;
using BankLibrary.Exceptions;

namespace BankLibrary.Models
{
	public abstract class Account : IAccount
	{
		protected readonly int id;
		protected readonly IEventNotifier Notifier;
		static int Counter = 0;
		static int AccNumCounter = 1;
		protected decimal Sum;
		protected readonly int percentage;
		private static string accNumint = "A0000001";
		protected readonly string accNum;
		protected readonly Person Owner;

		protected int Days = 0; // Days since account is created

		public Account(decimal sum, int percentage, Person owner, IEventNotifier notifier)
		{
			if (sum < 0)
            {
                throw new NegativeValue(nameof(sum));
            }

			Sum = sum;
			this.percentage = percentage;
			id = ++Counter;
			Notifier = notifier;


			Owner = owner;
			accNum = accNumint;
			GetNextAccNumber();
		}

		// Current money amount
		public decimal CurrentSum => Sum;

		public int Percentage => percentage;

		public int Id => id;

		public string AccNum => accNum;

		public Person Person => owner;

		public virtual void Put(decimal sum)
		{
			 if (sum < 0)
            {
                throw new NegativeValue(nameof(sum));
            }
			Sum += sum;
			Notifier.NotifyAboutEventByType(EventType.MoneyPut, $"Added to account {sum}");
		}
		public virtual decimal Withdraw(decimal sum)
		{
			decimal result = 0;
			  if (sum < 0)
            {
                throw new NegativeValue(nameof(sum));
            }
			if (sum <= Sum)
			{
				Sum -= sum;
				result = sum;
				Notifier.NotifyAboutEventByType(EventType.Withdraw, $"Amount {sum} withdrawed from account {id}");
			}
			else
			{
				Notifier.NotifyAboutEventByType(EventType.Withdraw, $"Not enough money in account {id}");
			}
			return result;
		}

		// Opening account
		protected internal virtual void Open()
		{
			Notifier.NotifyAboutEventByType(EventType.AccountOpen, $"New account is opened! Id: {id}");
		}
		// Closing account
		protected internal virtual void Close()
		{
			Notifier.NotifyAboutEventByType(EventType.AccountClose, $"Account {id} is closed. Total amount: {CurrentSum}");
		}

		protected internal void IncrementDays()
		{
			Days++;
		}

		// Percentage
		protected internal virtual void Calculate()
		{
			var increment = Sum * percentage / 100;
			Sum = Sum + increment;
			Notifier.NotifyAboutEventByType(EventType.MoneyCalculation, $"Money added after percentages calculations: {increment}");
		}


		//nextAccountNumber
		private void GetNextAccNumber()
		{
			if (AccNumCounter >= Math.Pow(10, 7) - 1)
			{
				AccNumCounter = 1;
				if (accNumint[0] == 'Z')
				{
					accNumint = accNumint.Remove(0, 1);
					accNumint = accNumint.Insert(0, "A");
				}
				else
				{
					char a = accNumint[0];
					accNumint = "";
					accNumint = accNumint.Insert(0, (Convert.ToChar(Convert.ToInt32(a) + 1)).ToString());
					accNumint += "0000001";
				}
			}
			else
			{
				AccNumCounter++;
				accNumint = accNumint[0].ToString();
				for (int i = 0; i < 7 - AccNumCounter.ToString().Length; i++)
				{
					accNumint += '0';
				}
				accNumint += AccNumCounter.ToString();

			}
		}
	}
}
