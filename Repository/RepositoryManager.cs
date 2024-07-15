using Contracts;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryContext repositoryContext;
        private IUserRepositoryy userRepository;

        public RepositoryManager(RepositoryContext repositoryContext)
        {
            this.repositoryContext = repositoryContext;
        }
        public IUserRepositoryy User
        {

            get
            {
                if (userRepository == null)
                {
                    userRepository = new UserRepository(repositoryContext);
                }
                return userRepository;
            }
        }

        public Task SaveAsync() => repositoryContext.SaveChangesAsync();
    }
}
