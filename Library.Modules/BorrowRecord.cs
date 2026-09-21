namespace Library.Models
{
    public class BorrowRecord
        {
            public int RecordId { get; set; }
            public int bookId { get; set; }
            public int memberId { get; set; }
            public DateTime BorrowDate { get; set; }
            public DateTime? ReturnDate { get; set; }

            // Property to check if the book has been returned
            public bool IsReturned => ReturnDate.HasValue;

            public BorrowRecord(int recordId, int bookId, int memberId)
            {
                RecordId = recordId;
                this.bookId = bookId;
                this.memberId = memberId;
                BorrowDate = DateTime.Now;
                ReturnDate = null;
            }
        }
            

        }
