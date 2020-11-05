using System;
using System.Collections.Generic;
using System.Text;

namespace BankLibrary.Interfaces
{
    public interface IEventNotifier
    {
        void NotifyAboutEventByType(EventType type, string message);
        string GetLastNotification();
    }

    public enum EventType
    {
        Withdraw,
        AccountOpen,
        AccountClose,
        MoneyPut,
        MoneyCalculation
    }
}
