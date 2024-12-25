using DockerHubBackend.Shared;

namespace DockerHubBackend.Model
{
    public class User : Entity
    {
        public string Password { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public Role Role { get; set; }

        public User() { }
        public User (string pass, string email, string username, Role role) {
            Password = pass;
            Email = email;
            Username = username;
            Role = role;
        }
    }
}
