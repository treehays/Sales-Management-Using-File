using SMS.interfaces;
using SMS.model;
namespace SMS.implementation
{
    public class AttendantManager : IAttendantManager
    {
        public static List<Attendant> listOfAttendant = new List<Attendant>();
        public string attendantFilePath = @"./Files/attendant.txt";
        public void CreateAttendant(string firstName, string lastName, string email, string phoneNumber, string pin, string post)
        {
            int id = listOfAttendant.Count() + 1;
            // string staffId = "AT" + new Random(id).Next(100000).ToString();
            Attendant attendant = new Attendant(id, User.GenerateRandomId(), firstName, lastName, email, phoneNumber, pin, post);
            //    Verifying Attendant Email
            if (GetAttendant(attendant.StaffId, email) == null)
            {
                listOfAttendant.Add(attendant);
                using (StreamWriter streamWriter = new StreamWriter(attendantFilePath, append: true))
                {
                    streamWriter.WriteLine(attendant.WriteToFIle());
                }
                Console.WriteLine($"Attendant Creation was Successful! \nThe Staff Identity Number is {attendant.StaffId} and pint {pin}, \nKeep it Safe.");
            }
            else
            {
                Console.WriteLine("Attendant already exist. \nKindly Go to Update to Update the Attendant Details");
            }

            // End
        }
        public void DeleteAttendant(string staffId)
        {
            Attendant attendant = GetAttendant(staffId);
            if (attendant != null)
            {
                Console.WriteLine($"{attendant.FirstName} {attendant.LastName} Successfully deleted. ");
                listOfAttendant.Remove(attendant);
                ReWriteToFile();
            }
            else
            {
                Console.WriteLine("User not found.");
            }
        }
        public Attendant GetAttendant(string staffId)
        {
            foreach (var item in listOfAttendant)
            {
                if (item.StaffId.ToUpper() == staffId.ToUpper())
                {
                    return item;
                }
            }
            return null;
        }
        public Attendant GetAttendant(string staffId, string email)
        {
            foreach (var item in listOfAttendant)
            {
                if (item.StaffId.ToUpper() == staffId.ToUpper() || item.Email.ToUpper() == email.ToUpper())
                {
                    return item;
                }
            }
            return null;
        }
        public void ViewAttendant(string staffId)
        {
            foreach (var item in listOfAttendant)
            {
                Console.WriteLine($"{item.FirstName}\t{item.LastName}\t{item.Email}\t{item.StaffId}\t{item.Post}");
            }
        }
        public Attendant Login(string staffId, string pin)
        {
            foreach (var item in listOfAttendant)
            {
                if (item.StaffId.ToUpper() == staffId.ToUpper() && item.Pin == pin)
                {
                    return item;
                }
            }
            return null;
        }
        public void UpdateAttendant(string staffId, string firstName, string lastName, string phoneNumber)
        {
            Attendant attendant = GetAttendant(staffId);
            if (attendant != null)
            {
                attendant.FirstName = firstName;
                attendant.LastName = lastName;
                attendant.PhoneNumber = phoneNumber;
            }
            else
            {
                Console.WriteLine("User not found.");
            }
        }
        public void ViewAllAttendants()
        {
            foreach (var item in listOfAttendant)
            {
                Console.WriteLine($"{item.StaffId} {item.LastName} {item.FirstName} {item.Email} {item.PhoneNumber}");
            }
        }
        public void ReWriteToFile()
        {
            File.WriteAllText(attendantFilePath, string.Empty);
            using (StreamWriter streamWriter = new StreamWriter(attendantFilePath))
            {
                foreach (var item in listOfAttendant)
                {
                    streamWriter.WriteLine(item.WriteToFIle());
                }
            }
        }
        public void ReadFromFile()
        {
            if (!File.Exists(attendantFilePath))
            {
                FileStream fileStream = new FileStream(attendantFilePath, FileMode.CreateNew);
                fileStream.Close();
            }
            using (StreamReader streamReader = new StreamReader(attendantFilePath))
            {
                while (streamReader.Peek() != -1)
                {
                    // if (streamReader.Peek() == -1)
                    // {
                        string attendantManager = streamReader.ReadLine();
                        listOfAttendant.Add(Attendant.ConvertToAttendant(attendantManager));
                    // }
                }
            }
        }
    }
}