using Contexts;
using DockerHubBackend.IRepository;
using DockerHubBackend.Repository;
using Marketing_system.DA.Contracts.Shared;

namespace DockerHubBackend.Shared
{
    public class UnitOfWork : IUnitOfWork
    {
        private DataContext _context;

        public UnitOfWork(DataContext context)
        {
            _context = context;
        }
        public async void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
                _context.Dispose();
            _context = null;
        }

        public Task<int> Save()
        {
            try
            {
                return _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving changes to the database: {ex.Message}");
                throw;
            }
        }

        private IUserRepository _userRepository { get; set; }
        private ITokenGeneratorRepository _tokenGeneratorRepository {  get; set; }

        public IUserRepository GetUserRepository()
        {
            return _userRepository ?? (_userRepository = new UserRepository(_context));
        }
        public ITokenGeneratorRepository GetTokenRepository()
        {
            return _tokenGeneratorRepository ?? (_tokenGeneratorRepository = new JwtGenerator());
        }
    }
}
