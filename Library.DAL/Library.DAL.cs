using System.Collections.Generic;
using Library.Models;


namespace Library.DAL
{
    public class LibraryRepository
    {
        public List<Book> Books { get; set; } = new List<Book>();
        public List<Member> Members { get; set; } = new List<Member>();
        public List<BorrowRecord> BorrowRecords { get; set; } = new List<BorrowRecord>();

        public int NextBookId { get; set; } = 1;
        public int NextMemberId { get; set; } = 1;
        public int NextRecordId { get; set; } = 1;
    }
}
