using Library.Models;
using Library.DAL;

namespace Library.Business
{


    public partial class LibrarySystem
    {
        public event Action<string>? Notification;
        private readonly LibraryRepository _repository = new LibraryRepository();
        
        public void RegisterMember(string email)
        {
            if (string.IsNullOrEmpty(email) || !email.Contains("@"))
            {
                throw new ArgumentException("Invalid email adress.");
            }
        }
        public LibrarySystem()
        {
            _repository.Books = new List<Book>();
            _repository.Members = new List<Member>();
            _repository.BorrowRecords = new List<BorrowRecord>();
            _repository.NextBookId = 1;
            _repository.NextMemberId = 1;
            _repository.NextRecordId = 1;
        }

       
       
    }


}
