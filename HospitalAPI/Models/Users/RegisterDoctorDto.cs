﻿namespace HospitalAPI.Models.Users
{
    public class RegisterDoctorDto : RegisterUserDto
    {
        public int? IdGender { get; set; }

        public string Firstname { get; set; } = null!;

        public string Lastname { get; set; } = null!;

        public DateTime BirthDate { get; set; }

        public string Biography { get; set; } = null!;
    }
}