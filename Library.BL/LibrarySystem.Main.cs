using Library.Models;
using Library.DAL;

namespace Library.BL
{


    public partial class LibrarySystem
    {
        public event Action<string>? Notification;
        private readonly LibraryRepository _repository = new LibraryRepository();
        
        public void RegisterMember(string email)
        {
            if (string.IsNullOrEmpty(email) || !email.Contains("@"))
            {
                throw new ArgumentException("Invalid email adress.");
            }
        }
        
       
    }


}
