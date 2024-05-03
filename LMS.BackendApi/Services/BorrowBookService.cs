using LMS.BackendApi.Models;
using LMS.BackendApi.Repository.Implementation;
using LMS.BackendApi.Repository.Interfaces;
using LMS.BackendApi.Services.Interfaces;

namespace LMS.BackendApi.Services
{
    public class BorrowBookService : IBorrowBookService
    {

        private readonly IBook _booksRepo;
        private readonly IBorrowedBooks _borrowedBooksRepo;
        public BorrowBookService(IBook _booksRepo, IBorrowedBooks _borrowedBooksRepo)
        {
            this._booksRepo = _booksRepo ?? throw new ArgumentNullException(nameof(_booksRepo)); ;
            this._borrowedBooksRepo = _borrowedBooksRepo ?? throw new ArgumentNullException(nameof(_borrowedBooksRepo)); ;
        }
        public int Insert(BorrowedBook borrowedBooks)
        {
            try
            {
                borrowedBooks.BorrowDate = DateTime.Now;
                borrowedBooks.Status = "Borrowed";
                _borrowedBooksRepo.Insert(borrowedBooks);

                var book = _booksRepo.GetBookById(borrowedBooks.BookId);

                book.AvailableCopies -= 1;

                _booksRepo.UpdateBook(book);

                return 0;
            }
            catch (Exception)
            {
                return 1;
            }
        }

        public int Update(BorrowedBook borrowedBooks)
        {
            BorrowedBook borrowed = _borrowedBooksRepo.GetBorrowedBooks(borrowedBooks.MemberId, borrowedBooks.BookId);

            if (borrowed != null)
            {
                borrowed.ReturnDate = DateTime.Now;
                borrowed.Status = "Returned";
                _borrowedBooksRepo.Update(borrowed);

                var book = _booksRepo.GetBookById(borrowed.BookId);

                book.AvailableCopies += 1;

                _booksRepo.UpdateBook(book);

                return 0;
            }
            else return 1;
        }
    }
}
