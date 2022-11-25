using System.Transactions;
using SMS.interfaces;
using SMS.model;
using Transaction = SMS.model.Transaction;

namespace SMS.implementation
{
    public class TransactionManager : ITransactionManager
    {
        private readonly string _transactionFilePath = @"./Files/transaction.txt";
        public static List<Transaction> ListOfTransaction = new List<Transaction>();
        // public static List<Transactiona> listOfCart = new List<Transactiona>();
        private readonly IProductManager _iProductManager = new ProductManager();
        public void CreateTransaction(string barCode, int quantity, string customerId, double cashTender)
        {
            var product = _iProductManager.GetProduct(barCode);
            var id = ListOfTransaction.Count + 1;
            var receiptNo = "ref" + new Random(id).Next(2323, 1000000).ToString();
            var total = product.Price * quantity;
            var expectedChange = cashTender - total;
            var dateTime = DateTime.Now;
            if (expectedChange < 0)
            {
                Console.WriteLine($"You can't pay lower than {total}");
            }
            else
            {
                var transaction = new Transaction(id, receiptNo, barCode, quantity, total, customerId, dateTime, cashTender);
                var isAvailable = _iProductManager.UpdateProductInventory(barCode, quantity);
                if (isAvailable)
                {
                    ListOfTransaction.Add(transaction);
                    using (var streamWriter = new StreamWriter(_transactionFilePath, append: true))
                    {
                        streamWriter.WriteLine(transaction.WriteToFIle());
                    }
                    Console.WriteLine($"Transaction Date: {dateTime} \tReceipt No: {receiptNo} \nBarcode: {product.BarCode} \nPrice Per Unit: {product.Price} \nQuantity:{quantity} \nTotal: {product.Price * quantity}\nCustomer ID:{customerId}.\nCustomer Change: {expectedChange}");
                }
                else
                {
                    Console.WriteLine($"Out of stock!!!. \nStock remaining {product.ProductQuantity}");
                }
            }

        }
        public double CalculateTotalSales()
        {
            return ListOfTransaction.Aggregate<Transaction, double>(0, (current, item) => item.Total + current);
        }
        public void GetAllTransactions()
        {
            Console.WriteLine("\nID\t\tTRANS. DATE \tCUSTOMER NAME\tTOTAL AMOUNT\tBARCODE\tRECEIPT NO");

            foreach (var item in ListOfTransaction)
            {
                Console.WriteLine($"{item.Id}\t{item.Datetime.ToString("d")}\t{item.CustomerId}\t{item.BarCode}\t{item.ReceiptNo}\t{item.Quantity}\t{item.Total}");
            }
        }

        public double GetAllTransactionsAdmin()
        {
            double cumulativeSum = 0;
            foreach (var item in ListOfTransaction)
            {
                Console.WriteLine($"{item.Id}\t{item.Datetime.ToString("d")}\t{item.CustomerId}\t{item.BarCode}\t{item.ReceiptNo}\t{item.Quantity}\t{item.Total}\t{cumulativeSum += item.Total}");
            }
            return cumulativeSum;
        }
        public void ReWriteToFile()
        {
            File.WriteAllText(_transactionFilePath, string.Empty);
            using (var streamWriter = new StreamWriter(_transactionFilePath))
            {
                foreach (var item in ListOfTransaction)
                {
                    streamWriter.WriteLine(item.WriteToFIle());
                }
            }
        }
        public void ReadFromFile()
        {
            if (!File.Exists(_transactionFilePath))
            {
                var fileStream = new FileStream(_transactionFilePath, FileMode.CreateNew);
                fileStream.Close();
            }
            using (var streamReader = new StreamReader(_transactionFilePath))
            {
                while (streamReader.Peek() != -1)
                {
                    var transactionManager = streamReader.ReadLine();
                    ListOfTransaction.Add(Transaction.ConvertToTransaction(transactionManager));
                }
            }
        }
    }
}