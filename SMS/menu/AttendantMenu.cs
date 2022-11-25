using SMS.interfaces;
using SMS.implementation;
using SMS.model;
namespace SMS.menu
{
    public class AttendantMenu
    {
        private readonly IAttendantManager _iAttendantManager = new AttendantManager();
        private readonly ITransactionManager _iTransactionManager = new TransactionManager();
        private readonly IProductManager _iProductManager = new ProductManager();
        public void RegisterAttendantPage()
        {
            Console.WriteLine("\nEnter Valid Details..");
            Console.WriteLine("\nRegister Attendant..");
            Console.Write("Email: ");
            var email = Console.ReadLine();
            var attendant = _iAttendantManager.GetAttendant(email);
            if (attendant == null)
            {
                Console.Write("First name: ");
                var firstName = Console.ReadLine();
                Console.Write("Last name: ");
                var lastName = Console.ReadLine();
                Console.Write("Phone Number: ");
                var phoneNumber = Console.ReadLine();
                Console.Write("pin: ");
                var pin = Console.ReadLine();
                Console.Write("Post: ");
                var post = Console.ReadLine();
                _iAttendantManager.CreateAttendant(firstName, lastName, email, phoneNumber, pin, post);
                // LoginAdminMenu();
            }
            else
            {
                Console.WriteLine("Email already exist..");
            }
            var adminMenu = new AdminMenu();
            adminMenu.AdminSubMenu();
        }
        public void DeleteProduct()
        {
            Console.Write("Enter the Barcode of the Product to be deleted.");
            var customerId = Console.ReadLine();
            //call delete method
        }
        public void LoginAttendantMenu()
        {
            Console.WriteLine("\nWelcome.\nEnter your Staff ID and Password to login ");
            Console.Write("Staff ID: ");
            var staffId = "AYO715570";// Console.ReadLine();
            Console.Write("Pin: ");
            var pin = "password";//Console.ReadLine();
            var attendant = _iAttendantManager.Login(staffId, pin);
            if (attendant != null)
            {
                Console.WriteLine($"Welcome {attendant.FirstName}, you've successfully Logged in!");
                AttendantSubMenu(attendant);
            }
            else
            {
                Console.WriteLine($"Wrong Email or Password!.");
            }
        }

        private void AttendantSubMenu(Attendant attendant)
        {
            int choice;
            do
            {
                // Console.Clear();
                Console.WriteLine("\n...Logged >> Attendant >>");
                Console.WriteLine("AZ Sales Management System. \nEnter valid option.");
                Console.WriteLine("Enter 1 to Record Sales.\nEnter 2 view all products.\nEnter 3 to Update personal Details.\nEnter 4 view product below range.  \nEnter 5 to View Sales history.\nEnter 6 view out of stock products.\nEnter 6 to Logout.\nEnter 0 to Close.");
                var chk = false;
                do
                {
                    chk = int.TryParse(Console.ReadLine(), out choice);
                    Console.WriteLine(chk ? "" : "Invalid Input.");

                } while (!chk);

                switch (choice)
                {
                    // while (!int.TryParse(Console.ReadLine(), out choice))
                    // {
                    //     // Console.Clear();
                    //     Console.WriteLine("Invalid Input\n");
                    //     AttendantSubMenu(attendant);
                    // }
                    case 1:
                        // Record Sales
                        MakeProductPayment();
                        AttendantSubMenu(attendant);
                        break;
                    case 2:
                        break;
                    case 3:
                    {
                        // Update detail
                        Console.WriteLine("\nWelcome.");
                        Console.Write("First Name: ");
                        var firstName = Console.ReadLine();
                        Console.Write("Last Name: ");
                        var lastName = Console.ReadLine();
                        Console.Write("Phone Number: ");
                        var phoneNumber = Console.ReadLine();
                        _iAttendantManager.UpdateAttendant(attendant.StaffId, firstName, lastName, phoneNumber);
                        break;
                    }
                    case 4:
                        ViewAllProductRanges();
                        AttendantSubMenu(attendant);
                        break;
                    case 5:
                        Console.WriteLine("\nID\tSTAFF\tFIRST NAME\tLAST NAME\tEMAIL\tPHONE NO");
                        _iTransactionManager.GetAllTransactions();
                        break;
                    case 6:
                    {
                        // logout
                        // LoginAttendantMenu();
                        var mainMenu = new MainMenu();
                        mainMenu.LoginMenu();
                        break;
                    }
                }
            } while (choice != 0);
        }

        private void ViewAllProductRanges()
        {
            Console.WriteLine("\nWelcome.\nView product below price range.");
            Console.Write("Enter highest Price range: ");
            var price = double.Parse(Console.ReadLine());
            _iProductManager.ViewProductBelow(price);
        }

        private void MakeProductPayment()
        {
            // Customer Details
            Console.WriteLine("...Logged >> Attendant >> Payment Page");
            var dateTime = new DateTime();
            dateTime = DateTime.UtcNow;
            Console.Write("CustomerName: ");
            var customerId = Console.ReadLine();
            Console.Write("Enter Product Barcode: ");
            var barCode = Console.ReadLine();
            Console.Write("Quantity: ");
            int quantity;
            while (!int.TryParse(Console.ReadLine(), out quantity))
            {
                Console.WriteLine("wrong input.. Try again.");
            }
            var product = _iProductManager.GetProduct(barCode);
            Console.WriteLine($"Amount to be Paid: {quantity * product.Price}");
            Console.Write("Cash Tender: ");
            double cashTender;
            while (!double.TryParse(Console.ReadLine(), out cashTender))
            {
                Console.WriteLine("wrong input.. Try again.");
            }
            _iTransactionManager.CreateTransaction(barCode, quantity, customerId, cashTender);
        }


        // public void ZZCustomerCart()
        // {
        //     Console.Write("Enter Porduct Barcode: ");
        //     string barCode = Console.ReadLine();
        //     Console.Write("Quantity: ");
        //     int quantity;
        //     while (!int.TryParse(Console.ReadLine(), out quantity))
        //     {
        //         System.Console.WriteLine("wrong input.. Try again.");
        //     }
        // }
    }
}