using Entities.Model.DepartamentsModel;
using Entities.Model.StatusModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Model
{
    public class User
    {
        public int id { get; set; }
        [MaxLength(250)] public required string login { get; set; }
        [MaxLength(500)] public required string password { get; set; }
        public string? token { get; set; }
        [ForeignKey("UserType")] public required int? user_type_id { get; set; }
        public UserType? user_type_ { get; set; }       
        public User()
        {

        }
    }

}
