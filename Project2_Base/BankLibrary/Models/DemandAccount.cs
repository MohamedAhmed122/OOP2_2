using BankLibrary.Interfaces;
using BankLibrary.Services;

namespace BankLibrary.Models
{
	public class DemandAccount : Account
	{
		public DemandAccount(decimal sum, int percentage, IEventNotifier notifier) 
            : base(sum, percentage, notifier)
		{
		}

		protected internal override void Open()
		{
            Notifier.NotifyAboutEventByType(EventType.AccountOpen, $"Opened new classic account! Id: {this.id}");
        }
	}
}
