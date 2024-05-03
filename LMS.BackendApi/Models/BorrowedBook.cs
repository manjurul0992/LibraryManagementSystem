using static System.Reflection.Metadata.BlobBuilder;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LMS.BackendApi.Models
{
    public class BorrowedBook
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonIgnore]
        public int BorrowID { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime ReturnDate { get; set; }
        [ForeignKey("Member")]
        public int MemberId { get; set; }
        [ForeignKey("Book")]
        public int BookId { get; set; }
        public string? Status { get; set; }
        [JsonIgnore]
        public Member? Member { get; set; }
        [JsonIgnore]
        public Book? Book { get; set; }
    }
}
