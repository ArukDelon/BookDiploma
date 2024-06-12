using Microsoft.AspNetCore.Identity;

namespace BookProject.Models
{
    public class LoginHistory
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public DateTime LoginDate { get; set; }
        public string IpAddress { get; set; }

        public AppUser User { get; set; }
    }
}
