using System.Collections.Generic;
namespace WebApplication5
{
    public interface Interface
    {
        public List<Book_Class> FetchBookList();
        public void Add(Book_Class addedbook);
        public List<User_Class> FetchUserList();

    }
}