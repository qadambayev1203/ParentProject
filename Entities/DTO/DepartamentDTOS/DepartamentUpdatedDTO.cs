using Entities.Model.FileModel;
using Entities.Model.StatusModel;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO.DepartamentDTOS
{
    public class DepartamentUpdatedDTO
    {

        public string? title { get; set; }
        public string? first_name { get; set; }
        public string? last_name { get; set; }
        public string? father_name { get; set; }
        public string? birthday_ { get; set; }
        public string? died_day_ { get; set; }
        public bool? is_died { get; set; }
        public bool? is_spouse { get; set; }
        public string? text { get; set; }
        public int? parent_id { get; set; }
        public int? status_id { get; set; }
        public IFormFile? img_up { get; set; }
        
    }
}
