using SMS.model;
using SMS.interfaces;
namespace SMS.implementation
{
    public class AdminManager : IAdminManager
    {
        ITransactionManager iTransactionManager = new TransactionManager();
        public static List<Admin> listOfAdmin = new List<Admin>();
        public string adminFilePath = @"./Files/admin.txt";
        public string fileDirect = @"./Files";
        public void CreateAdmin(string firstName, string lastName, string email, string phoneNumber, string pin, string post)
        {
            int id = listOfAdmin.Count() + 1;
            // string staffId = "AZ" + new Random(new Random().Next(1000)).Next(1100000).ToString();

            Admin admin = new Admin(id, User.GenerateRandomId(), firstName, lastName, email, phoneNumber, pin, post);
            listOfAdmin.Add(admin);
            using (StreamWriter streamWriter = new StreamWriter(adminFilePath, append: true))
            {
                streamWriter.WriteLine(admin.WriteToFIle());
            }
            Console.WriteLine($"Dear {firstName}, Registration Successful! \nYour Staff Identity Number is {admin.StaffId}, \nKeep it Safe.\n");
        }
        public void DeleteAdmin(string staffId)
        {
            Admin admin = GetAdmin(staffId);
            if (admin != null)
            {
                Console.WriteLine($"{admin.FirstName} {admin.LastName} Successfully deleted. ");
                listOfAdmin.Remove(admin);
                ReWriteToFile();
            }
            else
            {
                Console.WriteLine("User not found.");
            }
        }
        public Admin GetAdmin(string staffId)
        {
            foreach (var item in listOfAdmin)
            {
                if (item.StaffId == staffId)
                {
                    return item;
                }
            }
            return null;
        }
        public Admin GetAdmin(string staffId, string email)
        {
            foreach (var item in listOfAdmin)
            {
                if (item.StaffId == staffId || item.Email == email)
                {
                    return item;
                }
            }
            return null;
        }
        public Admin Login(string staffId, string pin)
        {
            foreach (var item in listOfAdmin)
            {
                if (item.StaffId == staffId.ToUpper() && item.Pin == pin)
                {
                    return item;
                }
            }
            return null;
        }
        public void UpdateAdmin(string staffId, string firstName, string lastName, string phoneNumber, string post)
        {
            Admin admin = GetAdmin(staffId);
            if (admin != null)
            {
                admin.FirstName = firstName;
                admin.LastName = lastName;
                admin.PhoneNumber = phoneNumber;
                admin.Post = post;
                ReWriteToFile();
            }
            else
            {
                Console.WriteLine("User not found.");
            }
        }

        public void ReWriteToFile()
        {
            File.WriteAllText(adminFilePath, string.Empty);
            using (StreamWriter streamWriter = new StreamWriter(adminFilePath, append: false))
            {
                foreach (var item in listOfAdmin)
                {
                    streamWriter.WriteLine(item.WriteToFIle());
                }
            }
        }
        public void ReadFromFile()
        {
            if (!Directory.Exists(fileDirect)) Directory.CreateDirectory(fileDirect);

            if (!File.Exists(adminFilePath))
            {
                var fileStream = new FileStream(adminFilePath, FileMode.CreateNew);
                fileStream.Close();
            }
            using (var streamReader = new StreamReader(adminFilePath))
            {
                while (streamReader.Peek() != -1)
                {
                    var adminManager = streamReader.ReadLine();
                    listOfAdmin.Add(Admin.ConvertToAdmin(adminManager));
                }
            }
        }
    }
}