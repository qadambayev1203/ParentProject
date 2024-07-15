using Entities.Model.DepartamentsModel;
using Entities.Model.StatusModel;
using Entities.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        public required string Login { get; set; }
        public string Email { get; set; }
        public string UserType { get; set; }
    }
}
