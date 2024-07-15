using AutoMapper;
using Contracts.AllRepository.DepartamentsRepository;
using Entities.DTO.DepartamentDTOS;
using Entities.Model.AnyClasses;
using Entities.Model.DepartamentsModel;
using Entities.Model.FileModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using TSTUWebAPI.Controllers.FileControllers;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TSTUWebAPI.Controllers.DepartamentsController
{
    [Route("api/departament")]
    [ApiController]
    public class DepartamentController : ControllerBase
    {
        private readonly IDepartamentRepository _repository;
        private readonly IMapper _mapper;
        private string[] formats = { "dd.MM.yyyy", "dd-MM-yyyy" };
        public DepartamentController(IDepartamentRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        // Departament CRUD

        [Authorize(Roles = "Admin")]
        [HttpPost("createdepartament")]
        public IActionResult CreateDepartament(DepartamentCreatedDTO departament1)
        {
            // Mapping DTO to entity
            var departament = _mapper.Map<Departament>(departament1);

            // Set default values
            departament.status_id = 1;
            departament.crated_at = DateTime.UtcNow;

            // Parse birthday if provided
            if (!string.IsNullOrEmpty(departament1.birthday_))
            {
                if (!DateOnly.TryParse(departament1.birthday_, out var birthday))
                {
                    return BadRequest("Invalid birthday format");
                }
                departament.birthday = birthday;
            }

            // Parse died day if provided
            if (!string.IsNullOrEmpty(departament1.died_day_))
            {
                if (!DateOnly.TryParse(departament1.died_day_, out var diedDay))
                {
                    return BadRequest("Invalid died day format");
                }
                departament.died_day = diedDay;
            }

            // Handle file upload
            FileUploadRepository fileUpload = new FileUploadRepository();
            var uploadResult = fileUpload.SaveFileAsync(departament1.img_up);

            if (uploadResult != "File not found or empty!" &&
                uploadResult != "Invalid file extension!" && uploadResult != "Error!")
            {
                departament.img_ = new Files
                {
                    title = Guid.NewGuid().ToString(),
                    url = uploadResult
                };
            }
            else if (uploadResult == null)
            {
                uploadResult = null;
            }
            else
            {
                return BadRequest("File upload error: " + uploadResult);
            }

            // Create department in repository
            int createdId = _repository.CreateDepartament(departament);

            if (createdId == 0)
            {
                // Delete uploaded file on failure
                fileUpload.DeleteFileAsync(uploadResult);
                return StatusCode(400);
            }

            // Return success response
            var createdItemId = new CreatedItemId
            {
                id = createdId,
                StatusCode = 200
            };

            return Ok(createdItemId);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("getalldepartament")]
        public IActionResult GetAllDepartament(int queryNum, int pageNum)
        {
            queryNum = Math.Abs(queryNum);
            pageNum = Math.Abs(pageNum);
            IEnumerable<Departament> departaments1 = _repository.AllDepartament(queryNum, pageNum);
            var departaments = _mapper.Map<IEnumerable<DepartamentReadedDTO>>(departaments1);
            if (departaments == null) { }
            return Ok(departaments);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("getbyiddepartament/{id}")]
        public IActionResult GetByIdDepartament(int id)
        {

            Departament departament1 = _repository.GetDepartamentById(id);
            if (departament1 == null)
            {

            }
            var departament = _mapper.Map<DepartamentReadedDTO>(departament1);
            if (departament == null) { }

            return Ok(departament);
        }

        [HttpGet("sitegetalldepartament")]
        public IActionResult GetAllDepartamentsite(int queryNum, int pageNum)
        {
            queryNum = Math.Abs(queryNum);
            pageNum = Math.Abs(pageNum);
            IEnumerable<Departament> departaments1 = _repository.AllDepartamentSite(queryNum, pageNum);
            var departaments = _mapper.Map<IEnumerable<DepartamentReadedSiteDTO>>(departaments1);
            if (departaments == null) { }
            return Ok(departaments);
        }

        [HttpGet("sitegetalldepartamentchild/{parent_id}")]
        public IActionResult GetAllDepartamentChild(int parent_id)
        {

            IEnumerable<Departament> departaments1 = _repository.AllDepartamentChild(parent_id);
            var departaments = _mapper.Map<IEnumerable<DepartamentChildReadedSiteDTO>>(departaments1);
            return Ok(departaments);
        }


        [HttpGet("sitegetbyiddepartament/{id}")]
        public IActionResult GetByIdDepartamentsite(int id)
        {

            Departament departament1 = _repository.GetDepartamentByIdSite(id);
            if (departament1 == null)
            {

            }
            var departament = _mapper.Map<DepartamentReadedSiteDTO>(departament1);
            if (departament == null) { }

            return Ok(departament);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("deletedepartament/{id}")]
        public IActionResult DeleteDepartament(int id)
        {
            bool check = _repository.DeleteDepartament(id);
            if (!check)
            {
                return StatusCode(400);
            }
            bool check1 = _repository.SaveChanges();
            if (!check1)
            {
                return StatusCode(400);
            }
            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("updatedepartament/{id}")]
        public IActionResult UpdateDepartament(DepartamentUpdatedDTO departament1, int id)
        {
            try
            {
                if (departament1 == null)
                {
                    return BadRequest();
                }

                // Mapping DTO to entity
                var dbupdated = _mapper.Map<Departament>(departament1);

                // Set updated timestamp
                dbupdated.updated_at = DateTime.UtcNow;

                // Parse birthday if provided
                if (!string.IsNullOrEmpty(departament1.birthday_))
                {
                    if (!DateOnly.TryParse(departament1.birthday_, out var birthday))
                    {
                        return BadRequest("Invalid birthday format");
                    }
                    dbupdated.birthday = birthday;
                }

                // Parse died day if provided
                if (!string.IsNullOrEmpty(departament1.died_day_))
                {
                    if (!DateOnly.TryParse(departament1.died_day_, out var diedDay))
                    {
                        return BadRequest("Invalid died day format");
                    }
                    dbupdated.died_day = diedDay;
                }

                // Handle file upload
                FileUploadRepository fileUpload = new FileUploadRepository();
                var uploadResult = fileUpload.SaveFileAsync(departament1.img_up);

                if (uploadResult != null && uploadResult != "File not found or empty!" &&
                    uploadResult != "Invalid file extension!" && uploadResult != "Error!")
                {
                    dbupdated.img_ = new Files
                    {
                        title = Guid.NewGuid().ToString(),
                        url = uploadResult
                    };
                }
                else if (uploadResult == null)
                {
                    uploadResult = null;
                }
                else
                {
                    return BadRequest("File upload error: " + uploadResult);
                }

                // Update department in repository
                bool updatedCheck = _repository.UpdateDepartament(id, dbupdated);

                if (!updatedCheck)
                {
                    return BadRequest("Update failed");
                }

                // Save changes in repository
                bool saveCheck = _repository.SaveChanges();

                if (!saveCheck)
                {
                    fileUpload.DeleteFileAsync(uploadResult);
                    return BadRequest("Save changes failed");
                }

                return Ok("Updated");
            }
            catch (Exception ex)
            {
                return BadRequest("Exception occurred: " + ex.Message);
            }
        }



    }
}

