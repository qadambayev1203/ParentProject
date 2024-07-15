using Contracts.AllRepository.DepartamentsRepository;
using Entities.Model.DepartamentsModel;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Entities.Model.StatusModel;
using Newtonsoft.Json;
using static System.Net.Mime.MediaTypeNames;

namespace Repository.AllSqlRepository.DepartamentsSqlRepository
{
    public class DepartamentSqlRepository : IDepartamentRepository
    {

        private readonly RepositoryContext _context;
        private readonly ILogger<DepartamentSqlRepository> _logger;
        public DepartamentSqlRepository(RepositoryContext repositoryContext, ILogger<DepartamentSqlRepository> logger)
        {
            _context = repositoryContext;
            _logger = logger;
        }




        //Departament CRUD
        public IEnumerable<Departament> AllDepartament(int queryNum, int pageNum)
        {
            try
            {
                var departaments = new List<Departament>();
                if (queryNum == 0 && pageNum != 0)
                {
                    departaments = _context.departament_2024parent.Include(x => x.img_).Include(x => x.status_).Skip(10 * (pageNum - 1)).Take(10).ToList();

                }
                if (queryNum != 0 && pageNum != 0)
                {
                    if (queryNum > 200) { queryNum = 200; }
                    departaments = _context.departament_2024parent.Include(x => x.img_).Include(x => x.status_).Skip(queryNum * (pageNum - 1)).Take(queryNum).ToList();

                }
                else
                {
                    departaments = _context.departament_2024parent.Include(x => x.img_).Include(x => x.status_).ToList();

                }
                return departaments;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error" + ex.ToString());
                return null;
            }
        }

        public IEnumerable<Departament> AllDepartamentSite(int queryNum, int pageNum)
        {
            try
            {
                var departaments = new List<Departament>();
                if (queryNum == 0 && pageNum != 0)
                {
                    departaments = _context.departament_2024parent.Include(x => x.img_).Include(x => x.status_).Where(x => x.status_.status != "Deleted").Skip(10 * (pageNum - 1)).Take(10).ToList();

                }
                if (queryNum != 0 && pageNum != 0)
                {
                    if (queryNum > 200) { queryNum = 200; }
                    departaments = _context.departament_2024parent.Include(x => x.img_).Include(x => x.status_).Where(x => x.status_.status != "Deleted").Skip(queryNum * (pageNum - 1)).Take(queryNum).ToList();

                }
                else
                {
                    departaments = _context.departament_2024parent.Include(x => x.img_).Include(x => x.status_).Where(x => x.status_.status != "Deleted").ToList();

                }
                return departaments;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error" + ex.ToString());
                return null;
            }
        }

        public int CreateDepartament(Departament departament)
        {
            try
            {
                if (departament == null)
                {
                    return 0;
                }


                _context.departament_2024parent.Add(departament);
                _context.SaveChanges();
                _logger.LogInformation($"Created " + JsonConvert.SerializeObject(departament));

                return departament.id;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error" + ex.ToString());
                return 0;
            }
        }

        public bool DeleteDepartament(int id)
        {
            try
            {
                var departament = GetDepartamentById(id);
                if (departament == null)
                {
                    return false;
                }
                departament.status_id = (_context.statuses_2024parent.FirstOrDefault(x => x.status == "Deleted")).id;
                _context.departament_2024parent.Update(departament);
                _logger.LogInformation($"Deleted " + JsonConvert.SerializeObject(departament));

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error" + ex.ToString());
                return false;
            }
        }

        public Departament GetDepartamentById(int id)
        {
            try
            {
                var departament = _context.departament_2024parent.Include(x => x.img_).Include(x => x.status_).FirstOrDefault(x => x.id.Equals(id));

                return departament;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error" + ex.ToString());
                return null;
            }
        }

        public Departament GetDepartamentByIdSite(int id)
        {
            try
            {
                var departament = _context.departament_2024parent.Include(x => x.img_).Include(x => x.status_).Where(x => x.status_.status != "Deleted").FirstOrDefault(x => x.id.Equals(id));

                return departament;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error" + ex.ToString());
                return null;
            }
        }

        public bool UpdateDepartament(int id, Departament departament)
        {

            try
            {
                var dbcheck = GetDepartamentById(id);
                if (dbcheck is null)
                {
                    return false;
                }

                dbcheck.title = departament.title;
                dbcheck.first_name = departament.first_name;
                dbcheck.last_name = departament.last_name;
                dbcheck.father_name = departament.father_name;

                dbcheck.text = departament.text;
                dbcheck.parent_id = departament.parent_id;
                dbcheck.status_id = departament.status_id;
                dbcheck.updated_at = departament.updated_at;
                if (departament.img_ != null)
                {
                    dbcheck.img_ = departament.img_;
                }


                _logger.LogInformation($"Updated " + JsonConvert.SerializeObject(dbcheck));
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error" + ex.ToString());
                return false;
            }
        }

        public IEnumerable<Departament> AllDepartamentChild(int parent_id)
        {
            try
            {
                var departaments = new List<Departament>();
                departaments = _context.departament_2024parent
                   .Include(x => x.img_)
                   .Include(x => x.status_).Where(x => x.status_.status != "Deleted")
                   .Where(x => x.parent_id == parent_id)
                   .ToList();

                return departaments;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error" + ex.ToString());
                return null;
            }
        }



        public bool SaveChanges()
        {
            try { _context.SaveChanges(); return true; }
            catch (Exception ex)
            {
                _logger.LogError($"Error" + ex.ToString()); return false;
            }
        }




    }
}
