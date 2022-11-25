using SMS.implementation;
using SMS.interfaces;
using SMS.model;

namespace SMS.menu
{
    public class AdminMenu
    {
        private readonly IAdminManager _iAdminManager = new AdminManager();
        private readonly IAttendantManager _iAttendantManager = new AttendantManager();
        private readonly IProductManager _iProductManager = new ProductManager();
        private readonly ITransactionManager _iTransactionManager = new TransactionManager();
        private int _choice;
        public void RegisterAdminPage()
        {
            Console.WriteLine("\n\tHome >> Register >> Admin");
            // Console.WriteLine("Welcome...");
            Console.Write("\tEmail: ");
            var email = Console.ReadLine();
            var adminManager = _iAdminManager.GetAdmin(email);

            if (adminManager == null)
            {
                Console.Write("\tFirst name: ");
                var firstName = Console.ReadLine();
                Console.Write("\tLast name: ");
                var lastName = Console.ReadLine();
                Console.Write("\tPhone Number: ");
                var phoneNumber = Console.ReadLine();
                Console.Write("\tpin: ");
                var pin = Console.ReadLine();
                Console.Write("\tPost: ");
                var post = Console.ReadLine();
                _iAdminManager.CreateAdmin(firstName, lastName, email, phoneNumber, pin, post);
                // LoginAdminMenu();
            }
            else
            {
                Console.WriteLine("Email already exist..");
            }
            var mainMenu = new MainMenu();
            mainMenu.LoginMenu();
        }

        private void DeleteAttendantMenu()
        {
            Console.Write("Enter Staff ID of the Attendant: ");
            var staffId = Console.ReadLine();
            _iAttendantManager.DeleteAttendant(staffId);
            ManageAttendantSubMenu();
        }
        public void LoginAdminMenu()
        {
            Console.WriteLine("\tWelcome.\n\tEnter your Staff ID and Password to login ");
            Console.Write("\tStaff ID: ");
            var staffId = Console.ReadLine();
            Console.Write("\tPin: ");
            var pin = Console.ReadLine();
            // iAdminManager.Login(staffId,pin); waht is this doing not part of the code
            var admin = _iAdminManager.Login(staffId, pin);
            if (admin != null)
            {
                Console.WriteLine($"Welcome {admin.FirstName}, you've successfully Logged in!");
                AdminSubMenu();
            }
            else
            {
                Console.WriteLine($"Wrong Staff ID or Password!.");
                var mainMenu = new MainMenu();
                mainMenu.LoginMenu();
            }
        }

        public void AdminSubMenu()
        {
            while (true)
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
                var chk = false;
                do
                {
                    Console.Write("Enter Operation No: ");
                    chk = int.TryParse(Console.ReadLine(), out _choice);
                    Console.WriteLine(chk ? "" : "Invalid Input.");
                } while (!chk);

                switch (_choice)
                {
                    case 0:
                        Console.WriteLine("Closed.");
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
                        Console.WriteLine("Update details.");
                        UpdateAdmin();
                        continue;
                    case 4:
                        // View Sales Records
                        Console.WriteLine("\nID\t TRANS. DATE \tCUSTOMER NAME\tAMOUNT\tBARCODE\tRECEIPT NO\tQTY\tTOTAL\tBALANCE");

                        Console.WriteLine($"Current Wallet Balance: {_iTransactionManager.GetAllTransactionsAdmin()}");
                        // iTransactionManager.GetAllTransactionsAdmin();
                        break;
                    case 5:
                        //Check Balance

                        Console.WriteLine($"Booked Balance: {_iTransactionManager.CalculateTotalSales()}");
                        break;
                    case 6:
                        // logout
                        var mainMenu = new MainMenu();
                        mainMenu.LoginMenu();
                        break;
                    default:
                        continue;
                }

                break;
            }
        }

        private void ManageAttendantSubMenu()
        {
            while (true)
            {
                Console.WriteLine("\n...>> Admin >> Manage Attendants >>");
                // Console.WriteLine("\nAZn Sales Management System. \nEnter valid option.");
                Console.WriteLine("\tEnter 1 to Create Attendant.\n\tEnter 2 to View all attendants. \n\tEnter 3 to Delete Attendant.\n\tEnter 4 to Logout.\n\tEnter 5 to go back. \n\tEnter 0 to Close.\n\tEnter Any key to goback.");
                var chk = false;
                do
                {
                    Console.Write("Enter Operation No: ");
                    chk = int.TryParse(Console.ReadLine(), out _choice);
                    Console.WriteLine(chk ? "" : "Invalid Input.");
                } while (!chk);

                switch (_choice)
                {
                    case 0:
                        Console.WriteLine("Closed.");
                        return;
                    // break;
                    case 1:
                        // Create Attendant
                        var attendantMenu = new AttendantMenu();
                        attendantMenu.RegisterAttendantPage();
                        AdminSubMenu();
                        break;
                    case 2:
                        Console.WriteLine("\nID\tSTAFF\tFIRST NAME\tLAST NAME\tEMAIL\tPHONE NO");
                        _iAttendantManager.ViewAllAttendants();
                        AdminSubMenu();
                        break;
                    case 3:
                        // Delete Attendants
                        DeleteAttendantMenu();
                        break;

                    case 4:
                        // logout
                        var mainMenu = new MainMenu();
                        mainMenu.LoginMenu();
                        break;

                    case 5:
                        AdminSubMenu();
                        break;
                    default:
                        continue;
                }

                break;
            }
        }

        private void DeleteProductMenu()
        {
            Console.Write("Enter Product BarCode: ");
            var barCode = Console.ReadLine();
            _iProductManager.DeleteProduct(barCode);
        }

        private void UpdateAdmin()
        {
            Console.Write("Enter StaffId: ");
            var staffId = Console.ReadLine().Trim();
            var admin = _iAdminManager.GetAdmin(staffId);
            if (admin != null)
            {
                Console.Write("Update First Name: ");
                var firstName = Console.ReadLine();

                Console.Write("Update Last Name: ");
                var lastName = Console.ReadLine();

                Console.Write("Enter new Post: ");
                var post = Console.ReadLine();

                Console.Write("Update Phone number: ");
                var phoneNumber = Console.ReadLine();

                _iAdminManager.UpdateAdmin(staffId, firstName, lastName, phoneNumber, post);
                // product.ProductName = productName;
                // adminToUpdate.LastName = lastName;
                Console.WriteLine($"{firstName} successfully updated. ");
                // AdminSubMenu();
            }
            else
            {
                Console.WriteLine($"{staffId} not found");
                // AdminSubMenu();
            }
        }

        private void UpdateProduct()
        {
            Console.Write("Enter Barcode of the product: ");
            var barCode = Console.ReadLine().Trim();
            var product = _iProductManager.GetProduct(barCode);
            if (product != null)
            {
                Console.Write("Update Product Name: ");
                var productName = Console.ReadLine();
                Console.Write("Update Price: ");
                var price = double.Parse(Console.ReadLine());
                Console.Write("Update Quantity: ");
                var quantity = int.Parse(Console.ReadLine());
                // barCode = Console.ReadLine();
                _iProductManager.UpdateProduct(barCode, productName, price, quantity);
                // product.ProductName = productName;
                // adminToUpdate.LastName = lastName;
                Console.WriteLine($"{productName} successfully updated. ");
                // ManageProductSubMenu();
            }
            else
            {
                Console.WriteLine($"{barCode} not found");
                // ManageProductSubMenu();
            }
        }

        private void AddProduct()
        {
            Console.Write("Barcode(Product ID): ");
            var barCode = Console.ReadLine();
            var product = _iProductManager.GetProduct(barCode);
            if (product == null)
            {
                Console.Write("Product Name: ");
                var productName = Console.ReadLine();
                Console.Write("Price: ");
                double price;
                while (!double.TryParse(Console.ReadLine(), out price))
                {
                    Console.WriteLine("wrong input.. Try again.");
                }
                Console.Write("Quantity: ");

                int productQuantity;
                while (!int.TryParse(Console.ReadLine(), out productQuantity))
                {
                    Console.WriteLine("wrong input.. Try again.");
                }
                _iProductManager.CreateProduct(barCode, productName, price, productQuantity);
            }
            else
            {
                Console.WriteLine("Product already exist...\nGo to Inventory to update product...");
            }
        }

        private void ManageProductSubMenu()
        {
            while (true)
            {
                Console.WriteLine("\n...>> Admin >> Manage Product >>");
                Console.WriteLine("\nAZ Sales Management System. \nEnter valid option.");
                Console.WriteLine("Enter 1 to Add a product1. \nEnter 2 to Update Product details. \nEnter 3  to View all Products. \nEnter 4 to Delete Product.\nEnter 5 to Go Back to Admin Menu\nEnter 6 to Logout.\nEnter 0 to Close.");

                var chk = false;
                do
                {
                    chk = int.TryParse(Console.ReadLine(), out _choice);
                    Console.WriteLine(chk ? "" : "Invalid Input.");
                } while (!chk);

                // int choice;
                // while (!int.TryParse(Console.ReadLine(), out choice))
                // {
                //     // Console.Clear();
                //     Console.WriteLine("Invalid Input\n");
                //     ManageProductSubMenu();
                // }
                switch (_choice)
                {
                    case 0:
                        Console.WriteLine("Closed.");
                        return;
                    // break;
                    case 1:
                        // Add Product
                        AddProduct();
                        continue;
                    case 2:
                        // Modify product
                        Console.WriteLine("Update Product details.");
                        UpdateProduct();
                        continue;

                    // break;
                    case 3:
                        // View All products
                        // iAdminManager.DeleteAdmin();
                        Console.WriteLine("\nID\tPRODUCT NAME\tBARCODE\tPRICE\tQTY\t");
                        _iProductManager.ViewAllProduct();
                        continue;

                    case 4:
                        DeleteProductMenu();
                        continue;
                    case 5:
                        AdminSubMenu();

                        break;
                    case 6:
                        // logout
                        var mainMenu = new MainMenu();
                        mainMenu.LoginMenu();
                        break;
                    default:
                        continue;
                }

                break;
            }
        }
    }
}
