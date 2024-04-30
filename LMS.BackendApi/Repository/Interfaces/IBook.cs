using LMS.BackendApi.Models;
using LMS.BackendApi.Models.ViewModels;

namespace LMS.BackendApi.Repository.Interfaces
{
    public interface IBook
    {
        List<Book> GetBooksAll();
        Book GetBookById(int id);
        int InsertBook(Book book);
        int UpdateBook(Book book);
        Task<List<BookDetailVM>> GetBookDetails();
    }
}
