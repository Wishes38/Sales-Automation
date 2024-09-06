namespace Api.Models
{
    public class Musteri
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string Tel { get; set; }
        public string Adress { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
