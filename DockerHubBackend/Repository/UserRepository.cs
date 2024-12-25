using Contexts;
using DockerHubBackend.IRepository;
using DockerHubBackend.Model;
using Microsoft.EntityFrameworkCore;

namespace DockerHubBackend.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public DataContext Context
        {
            get { return _dbContext as DataContext; }
        }
        public UserRepository(DataContext context) : base(context)
        {

        }
        public async Task<User> GetByEmailAsync(string email)
        {
            return await _dbContext.Set<User>().FirstOrDefaultAsync(x => x.Email == email);
        }
    }
}
