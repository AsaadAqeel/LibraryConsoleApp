using System;
using System.Collections.Generic;
using Library.Models;
using Library.BL;


namespace Library.CMD
{
    class LibraryUISystem
    {
        private static LibrarySystem Library;
        private static Dictionary<string, Action> menuActions;

        static void Main(string[] args)
        {

            Library = new LibrarySystem();
            Library.Notification += message => Console.WriteLine($"📢 Notification: {message}");


            SetupMenuActions();
            
            RunMainLoop();

        }

        static void SetupMenuActions()
        {
            menuActions = new Dictionary<string, Action>
            {
                ["1"] = () => AddBookMenu(),
                ["2"] = () => AddMemberMenu(),
                ["3"] = () => BorrowBookMenu(),
                ["4"] = () => ReturnBookMenu(),
                ["5"] = () => SearchBooksMenu(),
                ["6"] = () => Library.ListAvailableBooks(),
                ["7"] = () => Library.ShowBorrowedBooks(),
                ["8"] = () => Library.ShowMemberActivity(),
                ["9"] = () => Library.ShowLibraryStatistics(),
                ["0"] = () =>
                {
                    Library.SaveChanges();
                    Console.WriteLine("All data saved successfully. See you later :)");
                    Environment.Exit(0);
                }
              
            };
        }

        private static void RunMainLoop()
        {
            while (true)
            {
                try
                {
                    DisplayMenu();
                    var choice = Console.ReadLine()?.Trim();

                    if (menuActions.ContainsKey(choice))
                    {
                        menuActions[choice].Invoke();
                    }
                    else
                    {
                        Console.WriteLine("Invalid choice! Please try again.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
                Console.Clear();
            }
        }

        private static void DisplayMenu()
        {

            Console.WriteLine("\n╔════════════════════════════════════╗");
            Console.WriteLine("║        Library Management          ║");
            Console.WriteLine("╚════════════════════════════════════╝");
            Console.WriteLine("1. Add New Book");
            Console.WriteLine("2. Add New Member");
            Console.WriteLine("3. Borrow Book");
            Console.WriteLine("4. Return Book");
            Console.WriteLine("5. Search Books");
            Console.WriteLine("6. List Available Books");
            Console.WriteLine("7. Show Borrowed Books");
            Console.WriteLine("8. Member Activity Report");
            Console.WriteLine("9. Library Statistics");
            Console.WriteLine("0. Exit");
            Console.Write("\nEnter your choice: ");
        }

        private static void AddBookMenu()
        {
            Console.WriteLine("\n--- Add New Book ---");
            Console.Write("Enter title: ");
            var title = Console.ReadLine();

            Console.Write("Enter author: ");
            var author = Console.ReadLine();

            Library.Addbook(title, author);
        }



        private static void BorrowBookMenu()
        {
            Console.WriteLine("\n--- Borrow Book ---");
            Console.Write("Enter member ID: ");
            if (int.TryParse(Console.ReadLine(), out int memberId))
            {
                Console.Write("Enter book ID: ");
                if (int.TryParse(Console.ReadLine(), out int bookId))
                {
                    Library.Borrowbook(memberId, bookId);
                }
                else
                {
                    Console.WriteLine("Invalid book ID!");

                }
            }
            else
            {
                Console.WriteLine("Invalid member ID!");

            }
        }

        
        
       private static void AddMemberMenu()
        {
            Console.WriteLine("\n--- Add New Member ---");
            Console.Write("Enter member name: ");
            string name = Console.ReadLine();

            string email;
            while (true)
            {
                Console.Write("Enter member email: ");
                email = Console.ReadLine();
                if (LibrarySystem.IsValidEmail(email))
                {
                    break;
                }
                Console.WriteLine("❌ Invalid email format! Please try again (e.g., name@domain.com).");
            }
           
            try
            {
                Library.AddMember(name, email);

                Console.WriteLine("✅ Member added successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error: {ex.Message}");
            }
        }

        private static void ReturnBookMenu()
        {
            Console.WriteLine("\n--- Return Book ---");

            Console.Write("Enter book ID: ");
            if (int.TryParse(Console.ReadLine(), out int memberId))
            {

                Console.Write("Enter member ID: ");
                if (int.TryParse(Console.ReadLine(), out int bookId))
                {
                    try
                    {

                        Library.ReturnBook(memberId, bookId);

                        Console.WriteLine("✅ Book returned successfully!");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"❌ Error: {ex.Message}");
                    }
                }
                else
                {
                    Console.WriteLine("❌ Invalid Book ID format.");
                }
            }
            else
            {
                Console.WriteLine("❌ Invalid Member ID format.");
            }
        }


        private static void SearchBooksMenu()
        {
            Console.WriteLine("\n--- Search Book ---");

            Console.Write("Enter book title or Keyword: ");
            string keyword = Console.ReadLine();
            try
            {
                var foundbooks = Library.SearchBooks(keyword);
               
                if (foundbooks == null || foundbooks.Count == 0)
                {
                    Console.WriteLine("❌ No books found matching your search.");
                    return;
                }

                Console.WriteLine($"\n✅ Found {foundbooks.Count} Book(s):");
                Console.WriteLine(new string('_', 50));

                foreach (var book in foundbooks)
                {
                    book.DisplayInfo();
                    Console.WriteLine();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error : {ex.Message}");
            }
        }

    }
}


