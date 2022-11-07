
using System.Collections.Generic;
namespace WebApplication5
{
    public class User_Class
    {
        public int? user_id { get; set; }
        public string user_name { get; set; }

        public List<int>? user_book_ids { get; set; } //empty by default
        public List<Book_Class>? bookIssuedList { get; set; } //empty by default

    }

}