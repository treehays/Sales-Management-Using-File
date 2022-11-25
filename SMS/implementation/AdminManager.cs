using SMS.model;
using SMS.interfaces;
namespace SMS.implementation
{
    public class AdminManager : IAdminManager
    {
        private ITransactionManager _iTransactionManager = new TransactionManager();
        private static readonly List<Admin> _listOfAdmin = new List<Admin>();
        private readonly string _adminFilePath = @"./Files/admin.txt";
        private readonly string _fileDirect = @"./Files";
        public void CreateAdmin(string firstName, string lastName, string email, string phoneNumber, string pin, string post)
        {
            var id = _listOfAdmin.Count() + 1;
            // string staffId = "AZ" + new Random(new Random().Next(1000)).Next(1100000).ToString();

            var admin = new Admin(id, User.GenerateRandomId(), firstName, lastName, email, phoneNumber, pin, post);
            _listOfAdmin.Add(admin);
            using (var streamWriter = new StreamWriter(_adminFilePath, append: true))
            {
                streamWriter.WriteLine(admin.WriteToFIle());
            }
            Console.WriteLine($"Dear {firstName}, Registration Successful! \nYour Staff Identity Number is {admin.StaffId}, \nKeep it Safe.\n");
        }
        public void DeleteAdmin(string staffId)
        {
            var admin = GetAdmin(staffId);
            if (admin != null)
            {
                Console.WriteLine($"{admin.FirstName} {admin.LastName} Successfully deleted. ");
                _listOfAdmin.Remove(admin);
                ReWriteToFile();
            }
            else
            {
                Console.WriteLine("User not found.");
            }
        }
        public Admin GetAdmin(string staffId)
        {
            return _listOfAdmin.FirstOrDefault(item => item.StaffId == staffId);
        }
        public Admin GetAdmin(string staffId, string email)
        {
            return _listOfAdmin.FirstOrDefault(item => item.StaffId == staffId || item.Email == email);
        }
        public Admin Login(string staffId, string pin)
        {
            return _listOfAdmin.FirstOrDefault(item => item.StaffId == staffId.ToUpper() && item.Pin == pin);
        }
        public void UpdateAdmin(string staffId, string firstName, string lastName, string phoneNumber, string post)
        {
            var admin = GetAdmin(staffId);
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
            File.WriteAllText(_adminFilePath, string.Empty);
            using (var streamWriter = new StreamWriter(_adminFilePath, append: false))
            {
                foreach (var item in _listOfAdmin)
                {
                    streamWriter.WriteLine(item.WriteToFIle());
                }
            }
        }
        public void ReadFromFile()
        {
            if (!Directory.Exists(_fileDirect)) Directory.CreateDirectory(_fileDirect);

            if (!File.Exists(_adminFilePath))
            {
                var fileStream = new FileStream(_adminFilePath, FileMode.CreateNew);
                fileStream.Close();
            }
            using (var streamReader = new StreamReader(_adminFilePath))
            {
                while (streamReader.Peek() != -1)
                {
                    var adminManager = streamReader.ReadLine();
                    _listOfAdmin.Add(Admin.ConvertToAdmin(adminManager));
                }
            }
        }
    }
}