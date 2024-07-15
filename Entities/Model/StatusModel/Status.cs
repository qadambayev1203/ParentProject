using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Model.StatusModel
{
    public class Status
    {
        public int id { get; set; }
        [MaxLength(250)] public string? status { get; set; }
    }
}
