namespace LMS.BackendApi.Models.ViewModels
{
    public class BorrowedBookVM
    {

        public int MemberId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int TotalBooksBorrowed { get; set; }
        public int BooksReturned { get; set; }
        public int BooksBorrowed { get; set; }

    }
}
