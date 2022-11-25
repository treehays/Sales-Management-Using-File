namespace SMS.model
{
    public class User
    {
        protected int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Pin { get; set; }
        public readonly string StaffId;

        protected User(int id, string staffId, string firstName, string lastName, string email, string phoneNumber, string pin)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            StaffId = staffId;
            Email = email;
            PhoneNumber = phoneNumber;
            Pin = pin;
        }
         public static string GenerateRandomId()
        {
            var alphabet = "abcdefghijklmnopqrstuvwxyz".ToUpper();
            var r1 = new Random().Next(25);
            var r2 = new Random().Next(25);
            var staffId = $"A{alphabet[r1]}{alphabet[r2]}" + new Random().Next(1100000).ToString();
            return staffId;
        }
    }
}