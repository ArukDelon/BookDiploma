using BookProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace BookProject.Pages
{
    public class CartModel : PageModel
    {
        public Payment Payment { get; set; } = default!;

        [StringLength(2)]
        public string ExpirationMonth { get; set; }

        [StringLength(2)]
        public string ExpirationYear { get; set; }

        private readonly Services.ApplicationDbContext _context;

        public CartModel(Services.ApplicationDbContext context)
        {
            _context = context;
        }

        public ShoppingCart ShoppingCart { get; set; } = new ShoppingCart();

        public async Task<IActionResult> OnGetAsync()
        {
            // Assuming you store cart items in session or some persistent storage.
            var cartItems = HttpContext.Session.GetObjectFromJson<ShoppingCart>("Cart") ?? new ShoppingCart();

            foreach (var item in cartItems.Items)
            {
                var book = await _context.books
                .Include(b => b.Authors)
                .Include(b => b.Category)
                .FirstOrDefaultAsync(m => m.BookId == item.BookId);

                if (book != null)
                {
                    ShoppingCart.AddToCart(book, item.Quantity);
                }
            }
            ViewData["ShoppingCartJson"] = JsonConvert.SerializeObject(cartItems);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Payment Payment, string ExpirationMonth, string ExpirationYear, string ShoppingCartJson)
        {
            if (ModelState.IsValid)
            {
                ShoppingCart shoppingCart = JsonConvert.DeserializeObject<ShoppingCart>(ShoppingCartJson);

                var payment = new Payment
                {
                    CardHolder = Payment.CardHolder,
                    CardNumber = Payment.CardNumber,
                    ExpirationDate = $"{ExpirationMonth}/{ExpirationYear}",
                    CVC = Payment.CVC,
                    Amount = shoppingCart.GetTotal(),
                    AppUserId = User.FindFirstValue(ClaimTypes.NameIdentifier)
                };

                _context.payments.Add(payment);
                await _context.SaveChangesAsync();

                foreach (var item in shoppingCart.Items)
                {
                    var paymentItem = new PaymentItem
                    {
                        PaymentId = payment.Id,
                        BookId = item.BookId,
                        Quantity = item.Quantity,
                        Price = item.Quantity * item.Book.Price
                    };
                    _context.paymentItems.Add(paymentItem);
                }
                await _context.SaveChangesAsync();

                return RedirectToPage("Index");
            }

            return Page();
        }

        public IActionResult OnPostRemove(int bookId)
        {
            var cartItems = HttpContext.Session.GetObjectFromJson<ShoppingCart>("Cart") ?? new ShoppingCart();
            var item = cartItems.Items.FirstOrDefault(i => i.BookId == bookId);

            if (item != null)
            {
                cartItems.Items.Remove(item);
                HttpContext.Session.SetObjectAsJson("Cart", cartItems);
            }

            return RedirectToPage();
        }

        public IActionResult OnPostUpdateQuantity(int bookId, int quantity)
        {
            var cart = HttpContext.Session.GetObjectFromJson<ShoppingCart>("Cart") ?? new ShoppingCart();
            var item = cart.Items.FirstOrDefault(i => i.Book.BookId == bookId);

            if (item != null)
            {
                item.Quantity = quantity;
                HttpContext.Session.SetObjectAsJson("Cart", cart);
            }

            return RedirectToPage();
        }

    }
    public static class SessionExtensions
    {
        public static void SetObjectAsJson(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T GetObjectFromJson<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }
    }
}

