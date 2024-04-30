using LMS.BackendApi.Models;

namespace LMS.BackendApi.Repository.Interfaces
{
    public interface IAuthor
    {
        int InsertAuthor(Author author);
        List<Author> GetAuthorsAll();
    }
}
