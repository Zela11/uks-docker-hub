using DockerHubBackend.IRepository;

namespace DockerHubBackend
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> Save();
        public IUserRepository GetUserRepository();
        public ITokenGeneratorRepository GetTokenRepository();
    }
}
