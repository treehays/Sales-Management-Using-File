using SMS.implementation;
using SMS.interfaces;
using SMS.model;

namespace SMS.menu
{
    public class AdminMenu
    {
        IAdminManager iAdminManager = new AdminManager();
        IAttendantManager iAttendantManager = new AttendantManager();
        IProductManager iProductManager = new ProductManager();
        ITransactionManager iTransactionManager = new TransactionManager();
        public int choice;
        public void RegisterAdminPage()
        {
            Console.WriteLine("\n\tHome >> Register >> Admin");
            // Console.WriteLine("Welcome...");
            Console.Write("\tFirst name: ");
            string firstName = Console.ReadLine();
            Console.Write("\tLast name: ");
            string lastName = Console.ReadLine();
            Console.Write("\tEmail: ");
            string email = Console.ReadLine();
            Console.Write("\tPhone Number: ");
            string phoneNumber = Console.ReadLine();
            Console.Write("\tpin: ");
            string pin = Console.ReadLine();
            Console.Write("\tPost: ");
            string post = Console.ReadLine();
            iAdminManager.CreateAdmin(firstName, lastName, email, phoneNumber, pin, post);
            // LoginAdminMenu();
            MainMenu mainMenu = new MainMenu();
            mainMenu.LoginMenu();
        }
        public void DeleteAttendantMenu()
        {
            Console.Write("Enter Staff ID of the Attendant: ");
            string staffId = Console.ReadLine();
            iAttendantManager.DeleteAttendant(staffId);
            ManageAttendantSubMenu();
        }
        public void LoginAdminMenu()
        {
            Console.WriteLine("\tWelcome.\n\tEnter your Staff ID and Password to login ");
            Console.Write("\tStaff ID: ");
            string staffId = Console.ReadLine();
            Console.Write("\tPin: ");
            string pin = Console.ReadLine();
            // iAdminManager.Login(staffId,pin); waht is this doing not part of the code
            Admin admin = iAdminManager.Login(staffId, pin);
            if (admin != null)
            {
                Console.WriteLine($"Welcome {admin.FirstName}, you've successfully Logged in!");
                AdminSubMenu();
            }
            else
            {
                Console.WriteLine($"Wrong Staff ID or Password!.");
                MainMenu mainMenu = new MainMenu();
                mainMenu.LoginMenu();
            }
        }
        public void AdminSubMenu()
        {
            // int choice;
            // Console.Clear();
            Console.WriteLine(@"

################################################################################
####>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>####
####________________________________________________________________________####
####    Welcome to AZ Sales Management System. Enter valid option.          ####
####------------------------------------------------------------------------####
####>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>####
################################################################################");
            Console.WriteLine("\nHome >> Login >> Admin >>");
            // Console.WriteLine("\nAZ Sales Management System. \nEnter valid option.");
            Console.WriteLine("\tEnter 1 to Manage Attendant.\n\tEnter 2 to Manage Products \n\tEnter 3 to Update My Details. \n\tEnter 4 to View sales Records.\n\tEnter 5 to check Wallet. \n\tEnter 6 to Logout.\n\tEnter 0 to Close.");
            bool chk = false;
            do
            {
                Console.Write("Enter Operation No: ");
                chk = int.TryParse(Console.ReadLine(), out choice);
                Console.WriteLine(chk ? "" : "Invalid Input.");

            } while (!chk);
            switch (choice)
            {
                case 0:
                    System.Console.WriteLine("Closed.");
                    break;
                case 1:
                    // Manage Attendant
                    ManageAttendantSubMenu();

                    break;
                case 2:
                    // Manage Products 
                    ManageProductSubMenu();

                    break;
                case 3:
                    // Update detail
                    UpdateAdmin();
                    break;
                case 4:
                    // View Sales Records
                    Console.WriteLine("\nID\t TRANS. DATE \tCUSTOMER NAME\tAMOUNT\tBARCODE\tRECEIPT NO\tQTY\tTOTAL\tBALANCE");

                    Console.WriteLine($"Current Wallet Ballance: {iTransactionManager.GetAllTransactionsAdmin()}");
                    // iTransactionManager.GetAllTransactionsAdmin();
                    break;
                case 5:
                    //Check Balance

                    Console.WriteLine($"Booked Balance: {iTransactionManager.CalculateTotalSales()}");
                    break;
                case 6:
                    // logout
                    MainMenu mainMenu = new MainMenu();
                    mainMenu.LoginMenu();
                    break;
                default:
                    AdminSubMenu();
                    break;
            }
        }
        public void ManageAttendantSubMenu()
        {
            Console.WriteLine("\n...>> Admin >> Manage Attendants >>");
            // Console.WriteLine("\nAZn Sales Management System. \nEnter valid option.");
            Console.WriteLine("\tEnter 1 to Create Attendant.\n\tEnter 2 to View all attendants. \n\tEnter 3 to Delete Attendant.\n\tEnter 4 to Logout.\n\tEnter 0 to Close.\n\tEnter Any key to goback.");
            bool chk = false;
            do
            {
                Console.Write("Enter Operation No: ");
                chk = int.TryParse(Console.ReadLine(), out choice);
                Console.WriteLine(chk ? "" : "Invalid Input.");
            } while (!chk);
            switch (choice)
            {
                case 0:
                    Console.WriteLine("Closed.");
                    return;
                // break;
                case 1:
                    // Create Attendant
                    AttendantMenu attendantMenu = new AttendantMenu();
                    attendantMenu.RegisterAttendantPage();
                    AdminSubMenu();
                    break;
                case 2:
                    Console.WriteLine("\nID\tSTAFF\tFIRST NAME\tLAST NAME\tEMAIL\tPHONE NO");
                    iAttendantManager.ViewAllAttendants();
                    AdminSubMenu();
                    break;
                case 3:
                    // Delete Attendants
                    DeleteAttendantMenu();
                    break;
                case 4:
                    // logout
                    MainMenu mainMenu = new MainMenu();
                    mainMenu.LoginMenu();
                    break;
                default:
                    ManageAttendantSubMenu();
                    break;
            }
        }
        public void DeleteProductMenu()
        {
            Console.Write("Enter Product BarCode: ");
            string barCode = Console.ReadLine();
            iProductManager.DeleteProduct(barCode);
        }
        public void UpdateAdmin()
        {
            Console.Write("Enter StaffId: ");
            string staffId = Console.ReadLine().Trim();
           Admin admin = iAdminManager.GetAdmin(staffId);
            if (admin != null)
            {
                Console.Write("Update First Name: ");
                string firstName = Console.ReadLine();
                
                Console.Write("Update Last Name: ");
                string lastName = Console.ReadLine();
                
                Console.Write("Enter new Post: ");
                string post = Console.ReadLine();
                
                Console.Write("Update Phone number: ");
                string phoneNumber = Console.ReadLine();
                
                iAdminManager.UpdateAdmin(staffId,firstName,lastName,phoneNumber,post);
                // product.ProductName = productName;
                // adminToUpdate.LastName = lastName;
                Console.WriteLine($"{firstName} successfully updated. ");
            }
            else
            {
                Console.WriteLine($"{staffId} not found");
            }
        }
        public void UpdateProduct()
        {
            Console.Write("Enter Barcode of thte product: ");
            string barCode = Console.ReadLine().Trim();
            Product product = iProductManager.GetProduct(barCode);
            if (product != null)
            {
                Console.Write("Update Product Name: ");
                string productName = Console.ReadLine();
                Console.Write("Update Price: ");
                double price = double.Parse(Console.ReadLine());
                // Console.Write("Update Quantity: ");
                // barCode = Console.ReadLine();
                iProductManager.UpdateProduct(barCode, productName, price);
                // product.ProductName = productName;
                // adminToUpdate.LastName = lastName;
                Console.WriteLine($"{productName} successfully updated. ");
            }
            else
            {
                Console.WriteLine($"{barCode} not found");
            }
        }
        public void AddProduct()
        {
            Console.Write("Product Name: ");
            string productName = Console.ReadLine();
            Console.Write("Barcode(Product ID): ");
            string barCode = Console.ReadLine();
            Console.Write("Price: ");
            double price;
            while (!double.TryParse(Console.ReadLine(), out price))
            {
                System.Console.WriteLine("wrong input.. Try again.");
            }
            Console.Write("Quantity: ");

            int ProductQuantity;
            while (!int.TryParse(Console.ReadLine(), out ProductQuantity))
            {
                System.Console.WriteLine("wrong input.. Try again.");
            }
            iProductManager.CreateProduct(barCode, productName, price, ProductQuantity);
        }
        public void ManageProductSubMenu()
        {
            Console.WriteLine("\n...>> Admin >> Manage Product >>");
            Console.WriteLine("\nAZ Sales Management System. \nEnter valid option.");
            Console.WriteLine("Enter 1 to Add a product1. \nEnter 2 to Update Product details. \nEnter 3  to View all Products. \nEnter 4 to Delete Product.\nEnter 5 to Go Back to Admin Menu\nEnter 6 to Logout.\nEnter 0 to Close.");

            bool chk = false;
            do
            {
                chk = int.TryParse(Console.ReadLine(), out choice);
                Console.WriteLine(chk ? "" : "Invalid Input.");

            } while (!chk);

            // int choice;
            // while (!int.TryParse(Console.ReadLine(), out choice))
            // {
            //     // Console.Clear();
            //     Console.WriteLine("Invalid Input\n");
            //     ManageProductSubMenu();
            // }
            switch (choice)
            {
                case 0:
                    System.Console.WriteLine("Closed.");
                    return;
                // break;
                case 1:
                    // Add Product
                    AddProduct();
                    ManageProductSubMenu();
                    break;
                case 2:
                    // Modify product
                    Console.WriteLine("Update Product details.");
                    UpdateProduct();
                    ManageProductSubMenu();
                    break;

                // break;
                case 3:
                    // View All products
                    // iAdminManager.DeleteAdmin();
                    Console.WriteLine("\nID\tPRODUCT NAME\tBARCODE\tPRICE\tQTY\t");
                    iProductManager.ViewAllProduct();
                    ManageProductSubMenu();

                    break;
                case 4:
                    DeleteProductMenu();
                    ManageProductSubMenu();
                    break;
                case 5:
                    AdminSubMenu();

                    break;
                case 6:
                    // logout
                    MainMenu mainMenu = new MainMenu();
                    mainMenu.LoginMenu();
                    break;
                default:
                    ManageProductSubMenu();
                    break;
            }
        }
    }
}
