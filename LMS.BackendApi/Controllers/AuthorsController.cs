using LMS.BackendApi.Models;
using LMS.BackendApi.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS.BackendApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {

        private readonly IAuthor _author;
        public AuthorsController(IAuthor author)
        {
            this._author = author;
        }

        [HttpGet]
        [Route("GetAuthors")]
        public List<Author> Get()
        {
            return _author.GetAuthorsAll();
        }

        [HttpPost]
        [Route("InsertAuthor")]
        public int Insert(Author author)
        {
            return _author.InsertAuthor(author);
        }
    }
}
