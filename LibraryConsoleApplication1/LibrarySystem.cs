using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace LibraryConsoleApplication1
{



    

    public class LibrarySystem
    {
        private List<Book> books = new List<Book>();
        private List<Member> members = new List<Member>();
        private List<BorrowRecord> borrowRecords = new List<BorrowRecord>();

        private int nextbookId;
        private int nextmemberId;
        private int nextRecordId;

        public event Action<string> Notification;
        public LibrarySystem()
        {
            books = new List<Book>();
            members = new List<Member>();
            borrowRecords = new List<BorrowRecord>();
            nextbookId = 1;
            nextmemberId = 1;
            nextRecordId = 1;
        }
        // Method to add a new book to the library
        public void Addbook(string title, string author)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(author))
                {
                    throw new ArgumentException("Title and Author cannot be empty.");
                }
                // check if the book already exists in the library
                var existingBook = books.FirstOrDefault(b => b.Title.Equals(title, StringComparison.OrdinalIgnoreCase) && b.Author.Equals(author, StringComparison.OrdinalIgnoreCase));
                if (existingBook != null)
                {
                    throw new InvalidOperationException("A book with the same title and author already exists in the library.");
                    return;
                }
                // Create a new book and add it to the list
                var newBook = new Book(nextbookId.ToString(), title, author, true, DateTime.Now);
                books.Add(newBook);
                nextbookId++;

                Notification?.Invoke($"Book '{title}' by {author} added successfully with ID: {nextbookId}.");
                Console.WriteLine($"Book added successfully!");

            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
         

        }
        // Method to add a new member to the library
        public void AddMember(string name, string email)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(email))
                {
                    throw new ArgumentException("Name and Email cannot be empty.");
                }
                // check if the member already exists in the library using LINQ
                if (members.Any(m => m.Email.Equals(email, StringComparison.OrdinalIgnoreCase)))
                {
                    throw new InvalidOperationException("A member with the same email already exists in the library.");
                }
                // Create a new member and add it to the list
                var newMember = new Member(nextmemberId++, name, email, DateTime.Now);
                members.Add(newMember);

                Notification?.Invoke($"Member '{name}' added successfully with ID: {newMember.MemberId}.");
                Console.WriteLine($"{name} addedsuccessfully!");

            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

        }
        //Method to list all available books using LINQ
        public void SearchBooks(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                Console.WriteLine("Please Enter a search term");
                return;
            }

            var foundBooks = books.Where(b =>
                b.Title.ToLower().Contains(searchTerm.ToLower()) ||
                b.Author.ToLower().Contains(searchTerm.ToLower())
            ).ToList();
            if (foundBooks.Count == 0)
            {
                Console.WriteLine("No books found matching your search.");
                return;
            }
            Console.WriteLine($"\nfound {foundBooks.Count} Book(s):");
            Console.WriteLine(new string('_', 50));

            //use foreach to display results
            foreach (var book in foundBooks)
            {
                book.DisplayInfo();
                Console.WriteLine();
            }
        }

        public void ListAvailableBooks()
        {
            var AvailableBooks = books.Where(b => b.IsAvailable).ToList();
            
            if (AvailableBooks.Count == 0)
            {
                Console.WriteLine("No available books in the library");
                return;
            }
            Console.WriteLine("\n---Available Books---");
            foreach (var book in AvailableBooks)
            {
                book.DisplayInfo();
                Console.WriteLine();
            }
        }
     

        //Method to borrow a book
        public void Borrowbook(int memberId, int bookId)
        {

            try
            {
                var member = members.FirstOrDefault(m => m.MemberId == memberId);
                var book = books.FirstOrDefault(b => int.TryParse(b.BookId, out var id) && id == bookId);

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
                var borrowRecord = new BorrowRecord(nextRecordId++, bookId, memberId);
                borrowRecords.Add(borrowRecord);

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
                var member = members.FirstOrDefault(m => m.MemberId == memberId);
                var book = books.FirstOrDefault(b => int.TryParse(b.BookId, out var id) && id == bookId);

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
                var borrowRecord = borrowRecords.FirstOrDefault(br =>
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

            //method to show borrowed books using LINQ
        
        }
            

        }
