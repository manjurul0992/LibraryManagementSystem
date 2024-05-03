namespace LMS.WebFrontend.Models.ViewModels
{
    public class BookDetailVM
    {
        //public int BookId { get; set; }
        //public string Title { get; set; }
        ////ck
        //public string AuthorName { get; set; }
        //public DateTime PublishedDate { get; set; }
        //public string ISBN { get; set; }

        ////Integer
        //public string AvailableCopies { get; set; }
        ////public string TotalCopies { get; set; }
        ///
        public int bookId { get; set; }
        public string title { get; set; }
        public string authorName { get; set; }
        public DateTime publishedDate { get; set; }
        public string isbn { get; set; }
        public string availableCopies { get; set; }
    }
}
