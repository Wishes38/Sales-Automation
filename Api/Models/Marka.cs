namespace Api.Models
{
    public class Marka
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid KategoriId { get; set; }
        public Kategori Kategori{ get; set; }

    }
}
