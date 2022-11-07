using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Identity;
using static System.Reflection.Metadata.BlobBuilder;

namespace WebApplication5
{
    [Route("librarysystem")]
    [ApiController]
    public class myController : ControllerBase
    {

        private Interface logger;
        public myController(Interface logger)
        {
            this.logger = logger;
        }

        //Task 3: Write an API that fetches the list of all books 
        [HttpGet("getbooknames")]
        public IActionResult GetName()
        {
            List<Book_Class> controller_books = logger.FetchBookList();

            var names = from book in controller_books select book.book_name;
            return Ok(names);
        }

        //Task 5: Write an API that fetches a detail of a book
        [HttpGet("getbookdetail/{id}")]

        public IActionResult getbookdetail(int id)
        {
            List<Book_Class> controller_books = logger.FetchBookList();
            Book_Class bookDetail = (from book in controller_books where book.book_id == id select book).FirstOrDefault();
            if (bookDetail == null)
            {
                return NotFound("Book is issued by a user");
            }
            else
            {
                return Ok(bookDetail);
            }

        }


        //Task 7: Write an endpoint to add a new book to the repository
        [HttpPost("addbook")]

        public IActionResult bookaddition([FromBody] Book_Class newbook)
        {

            List<Book_Class> controller_books = logger.FetchBookList();
            //logger.Add(newbook);
            controller_books.Add(newbook);

            return Ok(controller_books);
        }

        //Task 9: Another endpoint is required to update the book record
        [HttpPut("updatebook/{id}")]

        public List<Book_Class> bookupdate(int id, [FromBody] Book_Class upbook)
        {
            List<Book_Class> controller_books = logger.FetchBookList();

            Book_Class booktoupdate = (from book in controller_books where book.book_id == id select book).FirstOrDefault();

            controller_books[controller_books.IndexOf(booktoupdate)].book_category = upbook.book_category;
            controller_books[controller_books.IndexOf(booktoupdate)].book_name = upbook.book_name;
            controller_books[controller_books.IndexOf(booktoupdate)].book_shelf = upbook.book_shelf;
            controller_books[controller_books.IndexOf(booktoupdate)].book_price = upbook.book_price;



            return controller_books;

        }

        //Task 12: Finally write endpoint to remove a book from the repository by its name
        [HttpDelete("deletebook/{id}")]

        public IActionResult delete(int id)
        {
            List<Book_Class> controller_books = logger.FetchBookList();
            List<User_Class> controller_users = logger.FetchUserList();
            Book_Class bookDetail = (from book in controller_books where book.book_id == id select book).FirstOrDefault();

            User_Class check = (from user in controller_users where user.bookIssuedList!=null where user.bookIssuedList.Any(book=>book.book_id==id) select user).FirstOrDefault();

            if (bookDetail == null && check == null)
            {

                return NotFound("The book doesn't exist");
            }
            else if (check != null)
            {
                
                
                Book_Class booko = (from book in check.bookIssuedList where book.book_id == id select book).FirstOrDefault();
                int index2 = controller_users.IndexOf(check);
                controller_users[index2].bookIssuedList.Remove(booko);

                return Ok(controller_users);
            }
            else
            {
                controller_books.Remove(controller_books[controller_books.IndexOf(bookDetail)]);
                return Ok("removed from books");
            }

        }

        //end of books___________________________________________________

        //task4 and task8
        [HttpPost("adduser")]

        public List<User_Class> useraddition([FromBody] User_Class newuser)

        {                   

            List<Book_Class> controller_books = logger.FetchBookList();

            List<User_Class> controller_users = logger.FetchUserList();

            if (newuser.user_book_ids != null)
            {
                foreach (int item in newuser.user_book_ids)
                {
                    newuser.bookIssuedList.Add((from book in controller_books where book.book_id == item select book).FirstOrDefault());
                }

            }


            User_Class userobj = new User_Class
            {
                user_name = newuser.user_name,
                user_id = controller_users.Count() + 1,
                bookIssuedList = newuser.bookIssuedList

            };


            controller_users.Add(userobj);

            return controller_users;
        }

        //task10 + task11
        [HttpPut("updateuser/{userid}")]

        public IActionResult bookupdate(int userid, [FromBody] User_Class upuser)
        {
            List<User_Class> controller_users = logger.FetchUserList();
            List<Book_Class> controller_books = logger.FetchBookList();

            User_Class usertoupdate = (from user in controller_users where user.user_id == userid select user).FirstOrDefault();
            
            if (upuser.user_book_ids != null)
            {
                //add in library
                List<Book_Class> userupbooks = new List<Book_Class>();

                if(controller_users[controller_users.IndexOf(usertoupdate)].bookIssuedList != null)
                {
                    foreach (var item in controller_users[controller_users.IndexOf(usertoupdate)].bookIssuedList)
                    {
                        controller_books.Add(item);
                    }
                    controller_users[controller_users.IndexOf(usertoupdate)].bookIssuedList = null;
                }

                
                int count = 0;
                foreach (int item in upuser.user_book_ids)
                {
                    
                    Book_Class addUserRemLibrary = (from book in controller_books where book.book_id == item select book).FirstOrDefault();
                    if (addUserRemLibrary == null)
                    {
                        count++;
                    }
                    else
                    {
                        userupbooks.Add(addUserRemLibrary);
                        controller_books.Remove(addUserRemLibrary);
                    }
                    
                }
                if (count == upuser.user_book_ids.Count)
                {
                    return NotFound("All books are already issued by user");
                }
                controller_users[controller_users.IndexOf(usertoupdate)].bookIssuedList = userupbooks;


            }
            controller_users[controller_users.IndexOf(usertoupdate)].user_name = upuser.user_name;
            

            return Ok(controller_books);

        }
        //task6

        [HttpGet("getusers/{userid}")]

        public IActionResult getuser(int userid)
        {

            List<User_Class> controller_users = logger.FetchUserList();
            var oneuser = from user in controller_users where user.user_id == userid select user;


            return Ok(oneuser);
        }

        //task13
        [HttpDelete("deluser/{userid}")]

        public IActionResult delUser(int userid)
        {

            List<User_Class> controller_users = logger.FetchUserList();
            User_Class oneuser = (from user in controller_users where user.user_id == userid select user).FirstOrDefault();
            if (oneuser.bookIssuedList == null)
            {
                controller_users.Remove(oneuser);
                return Ok(controller_users);
            }
            else
            {
                return NotFound("This user has some books");
            }
        }

        //Task 14: Write an API that remove the book from the bookIssuedList when the issued book is returned back to the library
        [HttpDelete("removebook")]

        public IActionResult delBook(int bookid)
        {
            List<Book_Class> controller_books = logger.FetchBookList();
            List<User_Class> controller_users = logger.FetchUserList();
            //user->booklist->id

                
                {
                    User_Class check = (from user in controller_users where user.bookIssuedList != null where user.bookIssuedList.Any(book => book.book_id == bookid) select user).FirstOrDefault();
                int index = controller_users.IndexOf(check);

                Book_Class  returnedbook =  controller_users[index].bookIssuedList.Find(book => book.book_id == bookid);
                controller_users[index].bookIssuedList.Remove(returnedbook);
                //controller_users[1].bookIssuedList.Remove((from book in controller_users where item == bookid select book).FirstOrDefault());

                // users user select based on ke uske pas book hai
                // 
                return Ok(controller_books);
                }

            

        }


    }

}