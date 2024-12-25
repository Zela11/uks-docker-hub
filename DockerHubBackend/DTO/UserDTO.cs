﻿using DockerHubBackend.Shared;

namespace DockerHubBackend.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public int Role { get; set; }
    }
}
