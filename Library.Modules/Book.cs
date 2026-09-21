namespace Library.Models
{
    // Simple book class with properties and a constructor
    public class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public bool IsAvailable { get; set; }
        public DateTime DateAdded { get; set; }

        // Constructor
        public Book(int bookId, string title, string author, bool isAvailable, DateTime dateAdded)
        {
            BookId = bookId;
            Title = title;
            Author = author;
            IsAvailable = isAvailable;
            DateAdded = dateAdded;
        }
        // Method to display book information
        public void DisplayInfo()
        {
            Console.WriteLine($"Book ID: {BookId}");
            Console.WriteLine($"Title: {Title}");
            Console.WriteLine($"Author: {Author}");
            Console.WriteLine($"Available: {IsAvailable}");
            Console.WriteLine($"Date Added: {DateAdded}");
        }
    }


}
