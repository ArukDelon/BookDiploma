using System.ComponentModel.DataAnnotations;

namespace BookProject.Models
{
    public class BookGrade
    {
        [Key]
        public int Id { get; set; }
        public int BookId {  get; set; }
        public string AppUserId { get; set; }
        public virtual decimal Grade { get; set; }

        public virtual Book Book { get; set; }
        public virtual AppUser AppUser { get; set; }

    }
}
