using SMS.implementation;
using SMS.interfaces;

namespace SMS.menu
{
    public class MainMenu
    {
        private int _choice;
        public void AllMainMenu()
        {
            IAdminManager adminManager = new AdminManager();
            IAttendantManager attendantManager = new AttendantManager();
            IProductManager productManager = new ProductManager();
            ITransactionManager transactionManager = new TransactionManager();
            adminManager.ReadFromFile();
            attendantManager.ReadFromFile();
            productManager.ReadFromFile();
            transactionManager.ReadFromFile();
            do
            {
                // Console.Clear();
                // Console.WriteLine("\n>>Main Menu");
                Console.WriteLine(@"
################################################################################
####>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>####
####________________________________________________________________________####
####    Welcome to AZ Sales Management System. Enter valid option.          ####
####------------------------------------------------------------------------####
####>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>####
################################################################################");
                Console.WriteLine("\tHome>>");
                Console.WriteLine("\tEnter 1 to Register.\n\tEnter 2 to Login.\n\tEnter 0 to Close.");
                bool chk;
                do
                {
                    Console.Write("Enter Operation No: ");
                    chk = int.TryParse(Console.ReadLine(), out _choice);
                    Console.WriteLine(chk ? "" : "Invalid Input.");
                } while (!chk);
                switch (_choice)
                {
                    case 1:
                        // Register
                        RegistrationMenu();
                        break;
                    case 2:
                        // Login
                        Console.WriteLine("\nMain Menu >> Login >> ");
                        LoginMenu();
                        break;
                    default:
                        // Invalid Choice
                        // Console.Clear();
                        Console.Write("Invalid Input.");
                        break;
                }
            } while (_choice != 0);

        }

        private void RegistrationMenu()
        {
            do
            {
                Console.WriteLine("\nHome >> Register >>");
                Console.WriteLine("\tEnter 1 Go back to Go Home. or \n\tEnter Your OneTime Registration Code for  Newly Employed Manager..");
                bool chk;
                do
                {
                    Console.Write("Enter Operation No: ");
                    chk = int.TryParse(Console.ReadLine(), out _choice);
                    Console.WriteLine(chk ? "" : "Invalid Input.");

                } while (!chk);
                switch (_choice)
                {
                    case 2546:
                    {
                        // Admin
                        var adminMenu = new AdminMenu();
                        adminMenu.RegisterAdminPage();
                        break;
                    }
                    // else if (choice == 2)
                    // // {
                    // //     // Attendant
                    // //     Console.WriteLine("\nMain Menu >> Register >> Attendant >>");
                    // //     AttendantMenu attendantMenu = new AttendantMenu();
                    // //     attendantMenu.RegisterAttendantPage();
                    // // }
                    // // else if (choice == 3)
                    // // {
                    // //     /*
                    // //     // Customer
                    // //     Console.WriteLine("\nMain Menu >> Register >> Customer >>");
                    // //     CustomerMenu customerMenu = new CustomerMenu();
                    // //     customerMenu.RegisterCustomerPage();
                    // //     */
                    // // }
                    case 1:
                        // Go Back
                        AllMainMenu();
                        break;
                    default:
                        // Invalid Choice
                        // Console.Clear();
                        Console.WriteLine("Invalid Input.\n");
                        RegistrationMenu();
                        break;
                }

            } while (_choice != 0);
        }
        public void LoginMenu()
        {
            do
            {
                Console.WriteLine("\n\tHome>> Login >> ");
                Console.WriteLine("\tEnter 1 for Admin.\n\tEnter 2 for Attendant. \n\tEnter 3 for Customer. \n\tEnter 4 to go back to Main Menu.\n\tEnter 0 to Close");
                bool chk;
                do
                {
                    Console.Write("Enter Operation No: ");
                    chk = int.TryParse(Console.ReadLine(), out _choice);
                    Console.WriteLine(chk ? "" : "Invalid Input.");
                } while (!chk);
                switch (_choice)
                {
                    case 1:
                    {
                        // Admin
                        Console.WriteLine("\nHome >> Login >> Admin");
                        var adminMenu = new AdminMenu();
                        adminMenu.LoginAdminMenu();
                        break;
                    }
                    case 2:
                    {
                        // Attendant
                        Console.WriteLine("\nMain Menu >> Login >> Attendant");
                        var attendantMenu = new AttendantMenu();
                        attendantMenu.LoginAttendantMenu();
                        break;
                    }
                    case 3:
                        /* OUT OFF THE PROGRAM FOR SUSTOMER
                    // Customer
                    Console.WriteLine("\nMain Menu >> Login >> Customer");
                    CustomerMenu customerMenu = new CustomerMenu();
                    customerMenu.LoginCUstomerMenu();
                    */
                        break;
                    case 4:
                        // Go Back
                        AllMainMenu();
                        break;
                    default:
                        // Invalid Choice
                        // Console.Clear();
                        Console.WriteLine("Invalid Input.\n");
                        LoginMenu();
                        break;
                }

            } while (_choice != 0);
            Console.WriteLine();
        }
    }
}
