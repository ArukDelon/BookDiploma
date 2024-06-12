using System.ComponentModel.DataAnnotations;

namespace BookProject.Models
{
    public class PaymentItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int PaymentId { get; set; }
        public virtual Payment Payment { get; set; }

        [Required]
        public int BookId { get; set; }
        public virtual Book Book { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public decimal Price { get; set; }
    }
}
