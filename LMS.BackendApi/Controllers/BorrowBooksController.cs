using LMS.BackendApi.Models;
using LMS.BackendApi.Repository.Interfaces;
using LMS.BackendApi.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS.BackendApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BorrowBooksController : ControllerBase
    {
        private readonly IBorrowBookService _borrowed;
        public BorrowBooksController(IBorrowBookService borrowed)
        {
            this._borrowed = borrowed;
        }


        [HttpPost]
        [Route("InsertBorrow")]
        public int Insert(BorrowedBook bb)
        {
            return _borrowed.Insert(bb);
        }

        [HttpPost]
        [Route("BorrowReturn")]
        public int Return(BorrowedBook bb)
        {
            return _borrowed.Update(bb);
        }
    }
}
