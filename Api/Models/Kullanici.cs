﻿namespace Api.Models
{
    public class Kullanici
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string Title{ get; set; }
    }
}
