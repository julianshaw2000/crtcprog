using Microsoft.AspNetCore.Identity;

namespace crtcprog.api.Models
{
    public class ApplicationUser : IdentityUser
    {

        public string Email { get; set; }
        public string Phone { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string PostCode { get; set; }
    }
}
