using System;
using System.Collections.Generic;
using System.Text;
using Library.Models;
using Library.DAL;

namespace Library.BL
{
    public partial class LibrarySystem
    {
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
                if (_repository.Members.Any(m => m.Email.Equals(email, StringComparison.OrdinalIgnoreCase)))
                {
                    throw new InvalidOperationException("A member with the same email already exists in the library.");
                }
                // Create a new member and add it to the list
                var newMember = new Member(_repository.NextMemberId++, name, email, DateTime.Now);
                _repository.Members.Add(newMember);

                Notification?.Invoke($"Member '{name}' added successfully with ID: {newMember.MemberId}.");
                Console.WriteLine($"{name} addedsuccessfully!");

            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

        }

        //Show Members Activity using LINQ
        public void ShowMemberActivity()
        {
            if (_repository.Members.Count == 0)
            {
                Console.WriteLine("No members found");
                return;
            }
            Console.WriteLine("\n--- Member Activity Report ---");
            Console.WriteLine($"{"Name",-20} {"Email",-25} {"Books Borrowed",-15}");
            Console.WriteLine(new string('-', 65));

            //Sort members by number of borrewed books using LINQ
            var SortedMembers = _repository.Members.OrderByDescending(m => m.BorrowedBookIds.Count);

            foreach (var member in SortedMembers)
            {
                Console.WriteLine($"{member.Name,-20} {member.Email,-25} {member.BorrowedBookIds.Count,-15}");

            }
        }
    }
}
