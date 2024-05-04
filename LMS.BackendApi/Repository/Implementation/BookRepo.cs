using LMS.BackendApi.Data;
using LMS.BackendApi.Models;
using LMS.BackendApi.Models.ViewModels;
using LMS.BackendApi.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Data;


namespace LMS.BackendApi.Repository.Implementation
{
    public class BookRepo : IBook
    {
        private LmsDbContext _context;
        public BookRepo(LmsDbContext _context)
        {
            this._context = _context ?? throw new ArgumentNullException(nameof(_context));
        }
        
        public Book GetBookById(int id)
        {
            try
            {
                var book = _context.Books.Where(x => x.BookId == id).FirstOrDefault();
                return book;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<List<BookDetailVM>> GetBookDetails()
        {
            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "CALL GetBookDetails;";
                command.CommandType = CommandType.Text;
                _context.Database.OpenConnection();

                using (var result =  command.ExecuteReader())
                {
                    var bookDetails = new List<BookDetailVM>();
                    while (await result.ReadAsync())
                    {
                        bookDetails.Add(new BookDetailVM
                        {
                            BookId = result.GetInt32("BookId"),
                            Title = result.GetString("Title"),
                            AuthorName = result.GetString("AuthorName"),
                            PublishedDate = result.GetDateTime("PublishedDate"),
                            ISBN = result.GetString("ISBN"),
                            AvailableCopies = result.GetString("AvailableCopies")
                        });
                    }

                    return bookDetails;
                }
            }
        }

        public List<Book> GetBooksAll()
        {
            try
            {
                List<Book> books = _context.Books.ToList();
                return books;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<BorrowedBookVM>> GetBooksMemberCount()
        {
            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "CALL GetMemberBorrowedBooks;";
                command.CommandType = CommandType.Text;
                _context.Database.OpenConnection();

                using (var result = await command.ExecuteReaderAsync())
                {
                    var memberChor = new List<BorrowedBookVM>();
                    while (await result.ReadAsync())
                    {
                        memberChor.Add(new BorrowedBookVM
                        {
                            MemberId = result.GetInt32("MemberId"),
                            FirstName = result.GetString("FirstName"),
                            LastName = result.GetString("LastName"),
                            TotalBooksBorrowed = result.GetInt32("TotalBooksBorrowed"),
                            BooksBorrowed = result.GetInt32("BooksBorrowed"),
                            BooksReturned = result.GetInt32("BooksReturned"),

                        });
                    }

                    return memberChor;
                }
            }
        }

        public int InsertBook(Book book)
        {
            try
            {
                _context.Books.Add(book);
                _context.SaveChanges();
                return 0;
            }
            catch (Exception)
            {
                return 1;
            }
        }

        public int UpdateBook(Book book)
        {
            try
            {
                _context.Books.Update(book);
                _context.SaveChanges();
                return 0;
            }
            catch (Exception)
            {
                return 1;
            }
        }


    }
}
