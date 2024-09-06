namespace MauiApp1.Models
{
    public class Duyuru
    {
        public Guid Id { get; set; }
        public string Baslik { get; set; }
        public string Icerik { get; set; }
        public DateTime DuyuruTarih{ get; set; }
        public string FotografUrl { get; set; }
        public int UserId { get; set; }
    }
}
