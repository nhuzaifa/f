using System.Collections.Generic;
using System.Data;
using WebApplication5;
using static System.Reflection.Metadata.BlobBuilder;

namespace WebApplication5
{
    public class LibraryData : Interface
    {
        List<Book_Class> Library_books= new List<Book_Class>
        {
            //Task1: Add 20 books to the system
            new Book_Class{book_id=3, book_category="fiction", book_name="book3", book_shelf=3, book_price=380},
            new Book_Class{book_id=4, book_category="history", book_name="book4", book_shelf=3, book_price=590},
            new Book_Class{book_id=5, book_category="gothic", book_name="book5", book_shelf=3, book_price=520},
            new Book_Class{book_id=6, book_category="poetry", book_name="book6", book_shelf=3, book_price=70},
            new Book_Class{book_id=7, book_category="thriller", book_name="book7", book_shelf=6, book_price=210},
            new Book_Class{book_id=8, book_category="moral", book_name="book8", book_shelf=6, book_price=350},
            new Book_Class{book_id=9, book_category="gothic", book_name="book9", book_shelf=6, book_price=450},
            new Book_Class{book_id=10, book_category="fiction", book_name="book10", book_shelf=6, book_price=700},
            new Book_Class{book_id=11, book_category="moral", book_name="book11", book_shelf=9, book_price=650},
            new Book_Class{book_id=12, book_category="thriller", book_name="book12", book_shelf=9, book_price=550},
            new Book_Class{book_id=13, book_category="fiction", book_name="book13", book_shelf=9, book_price=680},
            new Book_Class{book_id=14, book_category="moral", book_name="book14", book_shelf=9, book_price=720},
            new Book_Class{book_id=15, book_category="poetry", book_name="book15", book_shelf=2, book_price=810},
            new Book_Class{book_id=16, book_category="moral", book_name="book16", book_shelf=2, book_price=900},
            new Book_Class{book_id=17, book_category="gothic", book_name="book17", book_shelf=2, book_price=930},
            new Book_Class{book_id=18, book_category="moral", book_name="book18", book_shelf=2, book_price=340},
            new Book_Class{book_id=19, book_category="fiction", book_name="book19", book_shelf=1, book_price=1900},
            new Book_Class{book_id=20, book_category="history", book_name="book20", book_shelf=1, book_price=280}
        };

        public List<Book_Class> takenbooks = new List<Book_Class>
        {
            new Book_Class{book_id=1, book_category="moral", book_name="book1", book_shelf=1, book_price=110},
            new Book_Class{book_id=2, book_category="history", book_name="book2", book_shelf=1, book_price=250}
        }; 

        public List<Book_Class> FetchBookList()

        {

            return Library_books;

        }
        public void Add(Book_Class addedbook)
        {
            Library_books.Add(addedbook);

        }

        List<User_Class> User_List = new List<User_Class>

        {
            //Task2: Add 10 users to the system
         new User_Class{user_id=1, user_name="Jibran"},
         new User_Class{user_id=2, user_name="Huzaifa"},
         new User_Class{user_id=3, user_name="Waseem"},
         new User_Class{user_id=4, user_name="Zahid"},
         new User_Class{user_id=5, user_name="Ammar"},
         new User_Class{user_id=5, user_name="Wasif"},
         new User_Class{user_id=5, user_name="Abdullah"},
         new User_Class{user_id=5, user_name="Asad"},
         new User_Class{user_id=5, user_name="Ahmad"},
         new User_Class{user_id=5, user_name="Zulfiqar"}
         
        };

        public List<User_Class> FetchUserList()
        {
            User_List[0].bookIssuedList = takenbooks;

            return User_List;
        }

    }

}
