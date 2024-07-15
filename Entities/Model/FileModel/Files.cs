using Entities.Model.StatusModel;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Model.FileModel
{
    public class Files
    {
        public int id { get; set; }
        [MaxLength(500)] public string? title { get; set; }
        [MaxLength(500)]public string? url { get; set; }
        [ForeignKey("Status")]        public int? status_id { get; set; }
        public Status? status_ { get; set; }
     
    }
}
