namespace Api.Models
{
    public class Urun
    {
        public Guid Id { get; set; }
        public Guid KategoriId { get; set; }
        public Kategori Kategori { get; set; }
        public Guid MarkaId { get; set; }
        public Marka Marka { get; set; }
        public Guid ModelId { get; set; }
        public Model Model { get; set; }
        public DateTime CreatedDate{ get; set; }
        public double Price{ get; set; }

        public string ModelName
        {
            get { return Model?.Name; }
        }


    }
}
