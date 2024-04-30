using LMS.BackendApi.Data;
using LMS.BackendApi.Models;
using LMS.BackendApi.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LMS.BackendApi.Repository.Implementation
{
    public class AuthorRepo : IAuthor
    {
        private LmsDbContext _context;

        //ck
        public AuthorRepo(LmsDbContext _context)
        {
            this._context = _context ?? throw new ArgumentNullException(nameof(_context));
        }
        public List<Author> GetAuthorsAll()
        {
            try
            {
                List<Author> authors = _context.Authors.ToList();
                return authors;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int InsertAuthor(Author author)
        {
            try
            {
                _context.Authors.Add(author);
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
