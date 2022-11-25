namespace SMS.model
{
    public class Wallet
    {
        private int Id { get; set; }
        private double WalletRemainingBalance { get; set; }
        private double WalletTotal { get; set; }
        private double WalletWithdrawal { get; set; }

        public Wallet(int id, double walletTotal, double walletWithdrawal, double walletRemainingBalance)
        {
            Id = id;
            WalletTotal = walletTotal;
            WalletWithdrawal = walletWithdrawal;
            WalletRemainingBalance = WalletWithdrawal;
        }
    }
}