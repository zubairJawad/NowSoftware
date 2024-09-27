using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class UserDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Token { get; set; }
    }
    public class UserBalanceDTO
    {
        public decimal Balance { get; set;}
    }
}
