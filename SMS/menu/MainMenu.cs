using SMS.implementation;
using SMS.interfaces;

namespace SMS.menu
{
    public class MainMenu
    {
        public int choice;
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
                bool chk = false;
                do
                {
                    Console.Write("Enter Operation No: ");
                    chk = int.TryParse(Console.ReadLine(), out choice);
                    Console.WriteLine(chk ? "" : "Invalid Input.");
                } while (!chk);
                if (choice == 1)
                {
                    // Register
                    RegistrationMenu();
                }
                else if (choice == 2)
                {
                    // Login
                    Console.WriteLine("\nMain Menu >> Login >> ");
                    LoginMenu();
                }
                else
                {
                    // Invalid Choice
                    // Console.Clear();
                    Console.Write("Invalid Input.");
                }
            } while (choice != 0);

        }
        public void RegistrationMenu()
        {
            do
            {
                Console.WriteLine("\nHome >> Register >>");
                Console.WriteLine("\tEnter 1 Go back to Go Home. or \n\tEnter Your OneTime Registration Code for  Newly Employed Manager..");
                bool chk = false;
                do
                {
                    Console.Write("Enter Operation No: ");
                    chk = int.TryParse(Console.ReadLine(), out choice);
                    Console.WriteLine(chk ? "" : "Invalid Input.");

                } while (!chk);
                if (choice == 2546)
                {
                    // Admin
                    AdminMenu adminMenu = new AdminMenu();
                    adminMenu.RegisterAdminPage();
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
                else if (choice == 1)
                {
                    // Go Back
                    AllMainMenu();
                }
                else
                {
                    // Invalid Choice
                    // Console.Clear();
                    Console.WriteLine("Invalid Input.\n");
                    RegistrationMenu();
                }

            } while (choice != 0);
        }
        public void LoginMenu()
        {
            do
            {
                Console.WriteLine("\n\tHome>> Login >> ");
                Console.WriteLine("\tEnter 1 for Admin.\n\tEnter 2 for Attendant. \n\tEnter 3 for Customer. \n\tEnter 4 to go back to Main Menu.\n\tEnter 0 to Close");
                bool chk = false;
                do
                {
                    Console.Write("Enter Operation No: ");
                    chk = int.TryParse(Console.ReadLine(), out choice);
                    Console.WriteLine(chk ? "" : "Invalid Input.");
                } while (!chk);
                if (choice == 1)
                {
                    // Admin
                    Console.WriteLine("\nHome >> Login >> Admin");
                    AdminMenu adminMenu = new AdminMenu();
                    adminMenu.LoginAdminMenu();
                }
                else if (choice == 2)
                {
                    // Attendant
                    Console.WriteLine("\nMain Menu >> Login >> Attendant");
                    AttendantMenu attendantMenu = new AttendantMenu();
                    attendantMenu.LoginAttendantMenu();
                }
                else if (choice == 3)
                {
                    /* OUT OFF THE PROGRAM FOR SUSTOMER
                    // Customer
                    Console.WriteLine("\nMain Menu >> Login >> Customer");
                    CustomerMenu customerMenu = new CustomerMenu();
                    customerMenu.LoginCUstomerMenu();
                    */
                }
                else if (choice == 4)
                {
                    // Go Back
                    AllMainMenu();
                }
                else
                {
                    // Invalid Choice
                    // Console.Clear();
                    Console.WriteLine("Invalid Input.\n");
                    LoginMenu();
                }

            } while (choice != 0);
            Console.WriteLine();
        }
    }
}
