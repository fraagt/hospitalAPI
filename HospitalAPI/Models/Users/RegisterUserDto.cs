﻿namespace HospitalAPI.Models.Users
{
    public class RegisterUserDto
    {
        public string Login { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;
    }
}