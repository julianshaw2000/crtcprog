﻿namespace crtcprog.api.DTO
{
    public class UserDto
    {
        public int Id { get; set; }

        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string PostCode { get; set; }
        public bool EmailGood { get; set; } = false;
        public bool PhoneGood { get; set; } = false;
    }
}