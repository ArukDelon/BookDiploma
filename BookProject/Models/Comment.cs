using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BookProject.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        public string Body { get; set; }
        public string AppUserId { get; set; }
        public int BookId { get; set; }

        public DateTime CreatedDate { get; set; }

        public virtual Book Book { get; set; }
        public virtual AppUser AppUser { get; set; }
    }
}
