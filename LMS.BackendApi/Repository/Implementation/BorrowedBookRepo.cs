using LMS.BackendApi.Data;
using LMS.BackendApi.Models;
using LMS.BackendApi.Repository.Interfaces;

namespace LMS.BackendApi.Repository.Implementation
{
    public class BorrowedBookRepo : IBorrowedBooks
    {
        private LmsDbContext _context;

        public BorrowedBookRepo(LmsDbContext _context)
        {
            this._context = _context ?? throw new ArgumentNullException(nameof(_context));
        }

        public int Insert(BorrowedBook borrowed)
        {
            try
            {
                _context.BorrowedBooks.Add(borrowed);
                _context.SaveChanges();
                return 0;
            }
            catch (Exception)
            {
                return 1;
            }
        }

        public int Update(BorrowedBook borrowed)
        {
            try
            {
                _context.BorrowedBooks.Update(borrowed);
                _context.SaveChanges();
                return 0;
            }
            catch (Exception)
            {
                return 1;
            }
        }

        public BorrowedBook? GetBorrowedBooks(int memberId, int bookId)
        {
            try
            {
                BorrowedBook book = _context.BorrowedBooks.Where(x => x.MemberId == memberId && x.BookId == bookId && x.Status == "Borrowed").OrderBy(x => x.BorrowDate).FirstOrDefault();
                return book == null ? null : book;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}