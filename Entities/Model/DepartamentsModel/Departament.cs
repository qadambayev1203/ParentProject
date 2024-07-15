using Entities.Model.FileModel;
using Entities.Model.StatusModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Model.DepartamentsModel
{
    public class Departament
    {
        public int id { get; set; }
        [MaxLength(250)] public string? title { get; set; }
        [MaxLength(250)] public string? first_name { get; set; }
        [MaxLength(250)] public string? last_name { get; set; }
        [MaxLength(250)] public string? father_name { get; set; }
        public DateOnly? birthday { get; set; }
        public DateOnly? died_day { get; set; }
        public bool? is_died { get; set; }
        public bool? is_spouse { get; set; }
        public string? text { get; set; }
        public int? parent_id { get; set; }
        [ForeignKey("Status")] public int? status_id { get; set; }
        public Status? status_ { get; set; }
        public DateTime? crated_at { get; set; } = DateTime.UtcNow;
        public DateTime? updated_at { get; set; }
        [ForeignKey("Files")] public int? img_id { get; set; }
        public Files? img_ { get; set; }
    }
}
