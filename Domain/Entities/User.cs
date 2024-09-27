using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Device { get; set; }
        public string IpAddress { get; set; }
        public DateTime CreatedAt { get; set; }
        public decimal Balance { get; set; } = 0;
        public bool IsFirstSignIn { get; set; } = true;
        public string LastBrowser {  get; set; } = string.Empty;
    }
}
