using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO
{
    public class UserAuthInfoDTO
    {
        public UserDTO UserDetails { get; set; }
        public string Token { get; set; }
    }
}
