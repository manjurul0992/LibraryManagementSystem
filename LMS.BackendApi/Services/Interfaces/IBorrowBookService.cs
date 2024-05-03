using LMS.BackendApi.Models;

namespace LMS.BackendApi.Services.Interfaces
{
    public interface IBorrowBookService
    {
        int Insert(BorrowedBook borrowedBooks);
        int Update(BorrowedBook borrowedBooks);
    }
}
