using LMS.BackendApi.Models;

namespace LMS.BackendApi.Repository.Interfaces
{
    public interface IBorrowedBooks
    {
        int Insert(BorrowedBook borrowed);
        int Update(BorrowedBook borrowed);
        BorrowedBook GetBorrowedBooks(int memberId, int bookId);
    }
}
