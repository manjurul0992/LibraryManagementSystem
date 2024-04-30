using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LMS.BackendApi.Models
{
    public class Book
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonIgnore]
        public int BookId { get; set; }
        public string Title { get; set; }
        public DateTime PublishedDate { get; set; }
        public string ISBN { get; set; }
        [ForeignKey("Author")]
        public int AuthorID { get; set; }
        public int AvailableCopies { get; set; }
        public int TotalCopies { get; set; }
        [JsonIgnore]
        public Author? Author { get; set; }
    }
}
