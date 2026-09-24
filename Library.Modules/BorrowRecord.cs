using System.Net;
using System.Runtime.CompilerServices;

namespace Library.Models
{
    public class BorrowRecord
        {
            public int RecordId { get; set; }
            public int BookId { get; set; }
            public int MemberId { get; set; }
            public DateTime BorrowDate { get; set; }
            public DateTime? ReturnDate { get; set; }
            public bool IsReturned { get; set; }

        public BorrowRecord(int recordId, int bookId, int memberId)
        {
            RecordId = recordId;
            BookId = bookId;
            MemberId = memberId;
            BorrowDate = DateTime.Now; 
            ReturnDate = null;          
        }

        public BorrowRecord(int recordId, int bookId, int memberId, DateTime borrowDate, DateTime? returnDate, bool isReturned)
        {
            RecordId = recordId;
            BookId = bookId;
            MemberId = memberId;
            BorrowDate = borrowDate;
            ReturnDate = returnDate;
            IsReturned = isReturned;
        }
        }
            

        }
