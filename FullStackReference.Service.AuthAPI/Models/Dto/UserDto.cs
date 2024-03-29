﻿namespace FullStackReference.Service.AuthAPI.Models.Dto
{
    public class UserDto
    {
        public string ID { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        //public string Specialization { get; set; }
        public string PhoneNumber { get; set; }
        public string Role { get; set; }
    }
}
