using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace BookProject.Models
{
    public class Payment
    {
        [Key]
        public int Id { get; set; }

        [ValidateNever]
        [Required]
        public DateTime PaymentDate { get; set; } = DateTime.Now;

        [Required]
        [StringLength(100)]
        public string CardHolder { get; set; }

        [Required]
        [StringLength(20)]
        [CreditCard]
        public string CardNumber { get; set; }

        [ValidateNever]
        [Required]
        [StringLength(5)]
        public string ExpirationDate { get; set; } // Format MM/YY

        [Required]
        [StringLength(4)]
        public string CVC { get; set; }

        [ValidateNever]
        [Required]
        public decimal Amount { get; set; }

        [ValidateNever]
        public string AppUserId { get; set; }
        [ValidateNever]
        public virtual AppUser AppUser { get; set; }
        [ValidateNever]
        public virtual ICollection<PaymentItem> PaymentItems { get; set; }
    }
}
