namespace LMS.WebFrontend.Models.ViewModels
{
    public class BorrowedBookVM
    {
        public int memberId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public int totalBooksBorrowed { get; set; }
        public int booksReturned { get; set; }
        public int booksBorrowed { get; set; }
    }
}
