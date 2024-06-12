using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookProject.Models
{
    [NotMapped]
    public class CartItem
    {
        [JsonProperty]
        public int BookId { get; set; }
        public Book Book { get; set; }
        [JsonProperty]
        public int Quantity { get; set; }
    }

    [NotMapped]
    public class ShoppingCart
{
        [JsonProperty]
        public List<CartItem> Items { get; set; } = new List<CartItem>();
    
        public void AddToCart(Book book, int quantity)
        {
            var existingItem = Items.FirstOrDefault(i => i.Book.BookId == book.BookId);
            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                Items.Add(new CartItem { Book = book, Quantity = quantity });
            }
        }

        public void RemoveFromCart(int bookId)
        {
            var item = Items.FirstOrDefault(i => i.Book.BookId == bookId);
            if (item != null)
            {
                Items.Remove(item);
            }
        }

        public decimal GetTotal()
        {
            return Items.Sum(i => i.Book.Price * i.Quantity);
        }
    }
}
