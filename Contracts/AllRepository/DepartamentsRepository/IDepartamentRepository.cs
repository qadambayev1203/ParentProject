using Entities.Model.DepartamentsModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.AllRepository.DepartamentsRepository
{
    public interface IDepartamentRepository
    {
        //Departament CRUD
        public IEnumerable<Departament> AllDepartament(int queryNum, int pageNum);
        public IEnumerable<Departament> AllDepartamentSite(int queryNum, int pageNum);
        public IEnumerable<Departament> AllDepartamentChild(int parent_id);
        public Departament GetDepartamentById(int id);
        public Departament GetDepartamentByIdSite(int id);
        public int CreateDepartament(Departament departament);
        public bool UpdateDepartament(int id, Departament departament);
        public bool DeleteDepartament(int id);



      
        public bool SaveChanges();

    }
}
