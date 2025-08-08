﻿namespace AlignTech.WebAPI.DataFirst.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty ;
        public string PasswordHash { get; set; } = string.Empty;
    }
}
