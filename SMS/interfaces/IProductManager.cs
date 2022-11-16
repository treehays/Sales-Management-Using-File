using SMS.model;

namespace SMS.interfaces
{
    public interface IProductManager
    {
        void CreateProduct(string barCode, string productName, double price, int productQuantity);
        Product GetProduct(string barCode);
        void UpdateProduct(string barCode, string productName, double price,int quantity);
        void DeleteProduct(string barCode);
        void ViewAllProduct();
        void ViewProductBelow(double price);
        // void ViewProductBelow(double price);
        bool UpdateProductInventory(string barCode, int quantity);
        void ReadFromFile();
        void ReWriteToFile();
    }
}