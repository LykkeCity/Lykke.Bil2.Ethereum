using Lykke.Bil2.Ethereum.Models;

namespace Lykke.Bil2.Ethereum.Utils
{
    public static class TransactionReceiptResultExtensions
    {
        public static bool IsFailed(
            this TransactionReceipt transactionReceipt)
        {
            return transactionReceipt.Status == 0;
        }
        
        public static bool IsSucceeded(
            this TransactionReceipt transactionReceipt)
        {
            return transactionReceipt.Status == 1;
        }
    }
}
