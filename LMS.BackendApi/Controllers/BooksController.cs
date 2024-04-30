using LMS.BackendApi.Models;
using LMS.BackendApi.Models.ViewModels;
using LMS.BackendApi.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS.BackendApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBook _book;
        public BooksController(IBook _book)
        {
            this._book = _book;
        }

        [HttpGet]
        [Route("GetBooks")]
        public Task<List<BookDetailVM>> Get()
        {
            return _book.GetBookDetails();
        }

        [HttpPost]
        [Route("InsertBook")]
        public int Insert(Book book)
        {
            return _book.InsertBook(book);
        }
    }
}
