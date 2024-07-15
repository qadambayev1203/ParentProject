using Entities.Model.StatusModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO.ConfReadedDTO
{
    public class FilesConfReadedDTO
    {
        public int id { get; set; }
        public string? title { get; set; }
        public string? url { get; set; }
       
    }
}
