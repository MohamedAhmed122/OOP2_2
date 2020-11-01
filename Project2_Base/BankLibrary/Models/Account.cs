using BankLibrary.Interfaces;

namespace BankLibrary.Models
{
	public abstract class Account : IAccount
	{
        protected readonly int id;
        protected readonly IEventNotifier Notifier;
		static int Counter = 0;
        protected decimal Sum;
		protected readonly int percentage;

		protected int Days = 0; // Days since account is created

		public Account(decimal sum, int percentage, IEventNotifier notifier)
		{
			Sum = sum;
			this.percentage = percentage;
			id = ++Counter;
            Notifier = notifier;
        }

		// Current money amount
		public decimal CurrentSum => Sum;

        public int Percentage => percentage;

        public int Id => id;

        public virtual void Put(decimal sum)
		{
			Sum += sum;
			Notifier.NotifyAboutEventByType(EventType.MoneyPut, $"Added to account {sum}");
        }
		public virtual decimal Withdraw(decimal sum)
		{
			decimal result = 0;
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
	}
}
