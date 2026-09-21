using System;
using System.Collections.Generic;
using System.Text;
using Library.Models;
using Library.DAL;

namespace Library.BL
{
    public partial class LibrarySystem
    {
        //Method to borrow a book
        public void Borrowbook(int memberId, int bookId)
        {

            try
            {
                var member = _repository.Members.FirstOrDefault(m => m.MemberId == memberId);
                var book = _repository.Books.FirstOrDefault(b => b.BookId == bookId);

                if (member == null)
                {
                    Console.WriteLine("Member not found");
                    return;
                }
                if (book == null)
                {
                    Console.WriteLine("Book not found");
                    return;
                }
                if (!book.IsAvailable)
                {
                    Console.WriteLine("Book is not available for borrowing!");
                    return;
                }
                if (member.BorrowedBookIds.Count >= 3)
                {
                    Console.WriteLine("Member cannot borrow more than 3 books!");
                    return;
                }
                //Create Borrwo Record
                var borrowRecord = new BorrowRecord(_repository.NextRecordId++, bookId, memberId);
                _repository.BorrowRecords.Add(borrowRecord);

                //update book and member
                book.IsAvailable = false;
                member.BorrowedBookIds.Add(bookId);

                Notification?.Invoke($"{member.Name} borrowed '{book.Title}'");
                Console.WriteLine($"Book {book.Title} borrowed successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error borrowing book: {ex.Message}");
            }
        }

        //Method to return a book
        public void ReturnBook(int memberId, int bookId)
        {
            try
            {
                var member = _repository.Members.FirstOrDefault(m => m.MemberId == memberId);
                var book = _repository.Books.FirstOrDefault(b => b.BookId == bookId);

                if (member == null || book == null)
                {
                    Console.WriteLine("Member or book not found!");
                    return;
                }
                if (!member.BorrowedBookIds.Contains(bookId))
                {
                    Console.WriteLine("This member did not borrow this book!");
                    return;
                }
                //find borrow record using LINQ
                var borrowRecord = _repository.BorrowRecords.FirstOrDefault(br =>
                br.memberId == memberId &&
             br.bookId == bookId &&
             !br.IsReturned);

                if (borrowRecord == null)
                {
                    Console.WriteLine("No Active borrow record found!");
                    return;
                }

                //update records
                borrowRecord.ReturnDate = DateTime.Now;
                book.IsAvailable = true;
                member.BorrowedBookIds.Remove(bookId);

                Notification?.Invoke($"{member.Name} Returned '{book.Title}'");
                Console.WriteLine($"Book {book.Title} Returned successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Returning book: {ex.Message}");
            }

        }
        //Showing Borrowed books using LINQ
        public void ShowBorrowedBooks()
        {
            var activeBorrows = _repository.BorrowRecords.Where (br => !br.IsReturned).ToList();

            if (activeBorrows.Count == 0)
            {
                Console.WriteLine("No books are currently borrowed.");
                return;
            }
            Console.WriteLine("\n---Currently Borrowd Books---");
            Console.WriteLine($"{"Member",-20} {"Book Title",-30} {"Borrow Date",-12}");
            Console.WriteLine(new string('-', 65));
            
            foreach (var borrow in activeBorrows)
            {
                var member = _repository.Members.FirstOrDefault(m => m.MemberId == borrow.memberId);
                var book = _repository.Books.FirstOrDefault(b => b.BookId == borrow.bookId);

                if (member != null && book != null)
                {
                    Console.WriteLine($"{member.Name,-20} {book.Title,-30} {borrow.BorrowDate:MM/dd/yyyy}");
                }

            }
        }
    }
}
