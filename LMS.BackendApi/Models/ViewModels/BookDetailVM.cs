namespace LMS.BackendApi.Models.ViewModels
{
    public class BookDetailVM
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        //ck
        public string AuthorName { get; set; }
        public DateTime PublishedDate { get; set; }
        public string ISBN { get; set; }

         //Integer
        public string AvailableCopies { get; set; }
        //public string TotalCopies { get; set; }
    }
}
