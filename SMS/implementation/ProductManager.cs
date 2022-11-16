using SMS.interfaces;
using SMS.model;
namespace SMS.implementation
{
    public class ProductManager : IProductManager
    {
        public static List<Product> listOfProduct = new List<Product>();
        public string productFilePath = @"./Files/product.txt";
        public void CreateProduct(string barCode, string productName, double price, int productQuantity)
        {
            int id = listOfProduct.Count() + 1;
            Product product = new Product(id, barCode, productName, price, productQuantity);
            if (GetProduct(barCode) == null)
            {
                listOfProduct.Add(product);
                using (StreamWriter streamWriter = new StreamWriter(productFilePath, append: true))
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
            Product product = GetProduct(barCode);
            if (product != null)
            {
                Console.WriteLine($"{product.ProductName} Successfully deleted. ");
                listOfProduct.Remove(product);
                ReWriteToFile();
            }
            else
            {
                Console.WriteLine("Product not found.");
            }
        }
        public Product GetProduct(string barCode)
        {
            foreach (var item in listOfProduct)
            {
                if (item.BarCode == barCode)
                {
                    return item;
                }
            }
            return null;
        }
        public void UpdateProduct(string barCode, string productName, double price, int quantity)
        {
            Product product = GetProduct(barCode);
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
            Product product = GetProduct(barCode);

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
            foreach (var item in listOfProduct)
            {
                Console.WriteLine($"{item.Id}\t{item.ProductName}\t{item.BarCode}\t{item.Price}\t{item.ProductQuantity}");
            }
        }
        public void ViewProductBelow(double price)
        {
            var listOfProductBelow = from x in listOfProduct where x.Price < price select new { barcode = x.BarCode, productName = x.ProductName, price = x.Price };
            foreach (var item in listOfProductBelow)
            {
                int i = 1;
                Console.WriteLine($"{i++}\t{item.barcode}\t{item.productName}\t{item.price}");
            }
        }
        public void ReWriteToFile()
        {
            File.WriteAllText(productFilePath, string.Empty);
            using (StreamWriter streamWriter = new StreamWriter(productFilePath))
            {
                foreach (var item in listOfProduct)
                {
                    streamWriter.WriteLine(item.WriteToFIle());
                }
            }
        }
        public void ReadFromFile()
        {
            if (!File.Exists(productFilePath))
            {
                FileStream fileStream = new FileStream(productFilePath, FileMode.CreateNew);
                fileStream.Close();
            }
            using (StreamReader streamReader = new StreamReader(productFilePath))
            {
                while (streamReader.Peek() != -1)
                {
                    string productManager = streamReader.ReadLine();
                    listOfProduct.Add(Product.ConvertToProduct(productManager));
                }
            }
        }
    }
}