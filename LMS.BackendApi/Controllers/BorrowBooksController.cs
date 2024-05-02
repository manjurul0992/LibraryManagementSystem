using LMS.BackendApi.Models;
using LMS.BackendApi.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS.BackendApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BorrowBooksController : ControllerBase
    {
        private readonly IBorrowedBooks borrowedBooks;
        public BorrowBooksController(IBorrowedBooks borrowedBooks)
        {
            this.borrowedBooks = borrowedBooks;
        }


        [HttpPost]
        [Route("InsertBorrow")]
        public int Insert(BorrowedBook bb)
        {
            return borrowedBooks.Insert(bb);
        }

        [HttpPost]
        [Route("BorrowReturn")]
        public int Return(BorrowedBook bb)
        {
            return borrowedBooks.Update(bb);
        }
    }
}
