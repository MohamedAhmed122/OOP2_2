namespace BankLibrary.Interfaces
{
	public interface IAccount
	{
		// Add money to account
		void Put(decimal sum);

		// Get money
		decimal Withdraw(decimal sum);
	}
}
