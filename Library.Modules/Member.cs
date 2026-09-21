namespace Library.Models
{
    // Simple member class with List<T>
    public class Member
    {
        public int MemberId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime DateJoined { get; set; }

        public List<int> BorrowedBookIds { get; set; }

        public Member(int memberId, string name, string email, DateTime dateJoined)
        {
            MemberId = memberId;
            Name = name;
            Email = email;
            DateJoined = dateJoined;
            BorrowedBookIds = new List<int>();

        }

        // Method to display member information
        public void DisplayInfo()
        {
            Console.WriteLine($"Member ID: {MemberId} | Name: {Name} | Email: {Email} | Date Joined: {DateJoined:yyyy/MM/dd}");
            Console.WriteLine($"BorrowedBooks: {BorrowedBookIds.Count}");
        }

        // record of tracking book borrowing 
    }
            

        }
