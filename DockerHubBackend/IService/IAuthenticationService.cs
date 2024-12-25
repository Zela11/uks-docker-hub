using DockerHubBackend.DTO;

namespace DockerHubBackend.IService
{
    public interface IAuthenticationService
    {
        Task<TokenDTO> Login(string email, string password);
        Task<bool> Register(UserDTO user);
    }
}
