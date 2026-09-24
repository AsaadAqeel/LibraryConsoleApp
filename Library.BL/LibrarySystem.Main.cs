using Library.DAL;
using Library.DAL.Entities;
using Library.Models;

namespace Library.BL
{


    public partial class LibrarySystem
    {
        public event Action<string>? Notification;
        private readonly LibraryRepository _repository = new LibraryRepository();
        private readonly CsvDataHandler _csvHandler = new CsvDataHandler();


        public void RegisterMember(string email)
        {
            if (string.IsNullOrEmpty(email) || !email.Contains("@"))
            {
                throw new ArgumentException("Invalid email adress.");
            }
        }
        public LibrarySystem()
        {
            var data = _csvHandler.LoadData();

            _repository.Books = data.books;
            _repository.Members = data.members;
            _repository.BorrowRecords = data.records;
            _repository.NextBookId = data.nextBookId;
            _repository.NextMemberId = data.nextMemberId;
            _repository.NextRecordId = data.nextRecordId;
        }
        public void SaveChanges()
        {
            _csvHandler.SaveData(
                _repository.Books,
                _repository.Members,
                _repository.BorrowRecords,
                _repository.NextBookId,
                _repository.NextMemberId,
                _repository.NextRecordId
            );
        }
        


    }


}
