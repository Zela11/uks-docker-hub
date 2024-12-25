using DockerHubBackend.DTO;
using DockerHubBackend.IService;
using DockerHubBackend.Model;
using DockerHubBackend.Shared;
using Microsoft.AspNetCore.Mvc;

namespace DockerHubBackend.Service
{
    public class AuthenticationService : IAuthenticationService
    {
        public IUnitOfWork _unitOfWork { get; set; }

        public AuthenticationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<TokenDTO> Login(string email, string password)
        {
            var user = await _unitOfWork.GetUserRepository().GetByEmailAsync(email);

            if (user != null && password == user.Password)
            {
                return await _unitOfWork.GetTokenRepository().GenerateToken(user.Id, user.Email, "korisnik");
            } else
            {
                return null;
            }
        }

        public async Task<bool> Register(UserDTO user)
        {
            var result = await _unitOfWork.GetUserRepository().Add(new User(user.Password, user.Email, user.Username, (Role)user.Role));
            await _unitOfWork.Save();

            if (result != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
