using Contracts;
using Entities.Model;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class UserRepository : RepositoryBase<User>, IUserRepositoryy
    {
        public UserRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        { }

        public async Task<User> LoginAsync(string login, string password, bool tracking, CancellationToken cancellationToken = default)
        => await FindByCondition(x => x.login.Equals(login) && x.password.Equals(password), tracking).Include(u => u.user_type_).SingleOrDefaultAsync(cancellationToken);



    }
}
