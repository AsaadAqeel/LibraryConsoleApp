using System;
using System.Collections.Generic;
using System.Text;
using Library.Models;
using Library.DAL;



namespace Library.BL
{
    public partial class LibrarySystem
    {
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
                var existingBook = _repository.Books.FirstOrDefault(b => b.Title.Equals(title, StringComparison.OrdinalIgnoreCase) && b.Author.Equals(author, StringComparison.OrdinalIgnoreCase));
                if (existingBook != null)
                {
                    throw new InvalidOperationException("A book with the same title and author already exists in the library.");
                   
                }
                // Create a new book and add it to the list
                var newBook = new Book(_repository.NextBookId, title, author, true, DateTime.Now);
                _repository.Books.Add(newBook);
                _repository.NextBookId++;

                Notification?.Invoke($"Book '{title}' by {author} added successfully with ID: {_repository.NextBookId}.");

                Console.WriteLine($"Book added successfully!");

            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            SaveChanges();

        }

        //Method to list all available books using LINQ
        public List<Book> SearchBooks(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return new List<Book>();
            }

            return _repository.Books.Where(b => b.Title.ToLower().Contains(searchTerm.ToLower()) || b.Author.ToLower().Contains(searchTerm.ToLower())).ToList();
        }



            public void ListAvailableBooks()
            {
                var AvailableBooks = _repository.Books.Where(b => b.IsAvailable).ToList();

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
            SaveChanges();
        }
        public void ShowLibraryStatistics()
        {
            Console.WriteLine("\n--- Library Statistics ---");
            Console.WriteLine($"Total Books: {_repository.Books.Count}");
            Console.WriteLine($"Available Books: {_repository.Books.Count(b => b.IsAvailable)}");
            Console.WriteLine($"Borrowed Books: {_repository.Books.Count(b => !b.IsAvailable)}");
            Console.WriteLine($"Total Members: {_repository.Members.Count}");
            Console.WriteLine($"Active Borrows: {_repository.BorrowRecords.Count(br => !br.IsReturned)}");

            if (_repository.Books.Count == 0)
            {
                var PopularAuthor = _repository.Books.GroupBy(b => b.Author).OrderByDescending(g => g.Count()).First();

                Console.WriteLine($"Most Popular Author: {PopularAuthor.Key} ({PopularAuthor.Count()} books)");
            }
            SaveChanges();
        }
    }


   
}

