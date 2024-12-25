using DockerHubBackend.DTO;

namespace DockerHubBackend.IRepository
{
    public interface ITokenGeneratorRepository
    {
        Task<TokenDTO> GenerateToken(int id, string email, string type);
    }
}
