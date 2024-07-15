using Entities.Model.FileModel;
using Entities.Model.StatusModel;
using Entities.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Entities.DTO.ConfReadedDTO;

namespace Entities.DTO.DepartamentDTOS
{
    public class DepartamentChildReadedSiteDTO
    {
        public int id { get; set; }
        public string title { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string father_name { get; set; }
        public DateOnly? birthday { get; set; }
        public DateOnly? died_day { get; set; }
        public bool? is_died { get; set; }
        public bool? is_spouse { get; set; }
        public string text { get; set; }
        public int parent_id { get; set; }
        public Status status_ { get; set; }
        public DateTime crated_at { get; set; }
        public DateTime updated_at { get; set; }
        public FilesConfReadedDTO img_ { get; set; }

    }
}
