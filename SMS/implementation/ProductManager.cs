using SMS.interfaces;
using SMS.model;
namespace SMS.implementation
{
    public class ProductManager : IProductManager
    {
        private static readonly List<Product> ListOfProduct = new List<Product>();
        private readonly string _productFilePath = @"./Files/product.txt";
        public void CreateProduct(string barCode, string productName, double price, int productQuantity)
        {
            var id = ListOfProduct.Count + 1;
            var product = new Product(id, barCode, productName, price, productQuantity);
            if (GetProduct(barCode) == null)
            {
                ListOfProduct.Add(product);
                using (var streamWriter = new StreamWriter(_productFilePath, append: true))
                {
                    streamWriter.WriteLine(product.WriteToFIle());
                }

                Console.WriteLine($"Product Added Successfully. \nThere are total of {id} product's in the store.");
            }
            else
            {
                Console.WriteLine("Product already exist. \nKindly Go to Update to Update Available Quantity");
            }
        }
        public void DeleteProduct(string barCode)
        {
            var product = GetProduct(barCode);
            if (product != null)
            {
                Console.WriteLine($"{product.ProductName} Successfully deleted. ");
                ListOfProduct.Remove(product);
                ReWriteToFile();
            }
            else
            {
                Console.WriteLine("Product not found.");
            }
        }
        public Product GetProduct(string barCode)
        {
            return ListOfProduct.FirstOrDefault(item => item.BarCode == barCode);
        }
        public void UpdateProduct(string barCode, string productName, double price, int quantity)
        {
            var product = GetProduct(barCode);
            if (product != null)
            {
                // product.BarCode  = barCode;
                product.ProductName = productName;
                product.Price = price;
                ReWriteToFile();
            }
            else
            {
                Console.WriteLine("Product not found.");
            }
        }
        public bool UpdateProductInventory(string barCode, int quantity)
        {
            var product = GetProduct(barCode);

            if (product.ProductQuantity - quantity >= 0)
            {
                product.ProductQuantity = product.ProductQuantity - quantity;
                ReWriteToFile();
                return true;
            }
            // else
            // {
            //     Console.WriteLine($"Out of stock!!!. \nStock remaining {product.ProductQuantity}");
            //     return false;
            // }
            return false;
        }
        public void ViewAllProduct()
        {
            foreach (var item in ListOfProduct)
            {
                Console.WriteLine($"{item.Id}\t{item.ProductName}\t{item.BarCode}\t{item.Price}\t{item.ProductQuantity}");
            }
        }
        public void ViewProductBelow(double price)
        {
            var listOfProductBelow = from x in ListOfProduct where x.Price < price select new { barcode = x.BarCode, productName = x.ProductName, price = x.Price };
            foreach (var item in listOfProductBelow)
            {
                var i = 1;
                Console.WriteLine($"{i++}\t{item.barcode}\t{item.productName}\t{item.price}");
            }
        }
        public void ReWriteToFile()
        {
            File.WriteAllText(_productFilePath, string.Empty);
            using (var streamWriter = new StreamWriter(_productFilePath))
            {
                foreach (var item in ListOfProduct)
                {
                    streamWriter.WriteLine(item.WriteToFIle());
                }
            }
        }
        public void ReadFromFile()
        {
            if (!File.Exists(_productFilePath))
            {
                var fileStream = new FileStream(_productFilePath, FileMode.CreateNew);
                fileStream.Close();
            }
            using (var streamReader = new StreamReader(_productFilePath))
            {
                while (streamReader.Peek() != -1)
                {
                    var productManager = streamReader.ReadLine();
                    ListOfProduct.Add(Product.ConvertToProduct(productManager));
                }
            }
        }
    }
}