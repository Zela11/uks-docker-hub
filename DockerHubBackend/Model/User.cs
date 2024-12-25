using DockerHubBackend.Shared;

namespace DockerHubBackend.Model
{
    public class User : Entity
    {
        public string Password { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Birthday { get; set; }

        public User() { }
        public User (string pass, string email, string name, string surname, string birthday) {
            Password = pass;
            Email = email;
            Name = name;
            Surname = surname;
            Birthday = birthday;
        }
    }
}
