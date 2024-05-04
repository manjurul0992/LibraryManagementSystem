using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.WPFClient.Models.ViewModels
{
    public class BookDetailVM
    {
        public int bookId { get; set; }
        public string title { get; set; }
        public string authorName { get; set; }
        public DateTime publishedDate { get; set; }
        public string isbn { get; set; }
        public string availableCopies { get; set; }
    }
}
