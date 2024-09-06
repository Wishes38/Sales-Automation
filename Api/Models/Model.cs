namespace Api.Models
{
    public class Model
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid MarkaId { get; set; }
        public Marka Marka { get; set; }
    }
}
