using DockerHubBackend.Model;

namespace DockerHubBackend.IRepository
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetByEmailAsync(string email);
    }
}
