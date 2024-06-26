﻿using System.ComponentModel.DataAnnotations;

namespace BookProject.Models
{
    public class BookTag
    {
        [Key]
        public int Id { get; set; }
        public int BookId { get; set; }
        public int TagId { get; set; }

        public virtual Book Book { get; set; }
        public virtual Tag Tag { get; set; }    
    }
}
