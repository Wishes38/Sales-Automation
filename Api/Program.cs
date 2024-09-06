using Api.EF;
using Api.Models;

namespace Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var app = builder.Build();

            app.MapGet("/", () => { return "Boþ sayfa"; }
                );

            app.MapGet("/kategori", () =>
            {
                Context context = new Context();
                var liste = context.Kategori.ToList();
                return liste;

            });
            app.MapGet("/kategori/{id}", (Guid id) =>
            {
                Context context = new Context();
                var liste = context.Kategori.Where(x=>x.Id == id).ToList();
                return liste[0];

            });

            app.MapPost("/kategori", (Kategori kategori) =>
            {
                Context context = new Context();
                context.Kategori.Add(kategori);
                context.SaveChanges();

            });

            app.MapGet("/marka", () =>
            {
                Context context = new Context();
                var liste = context.Marka.ToList();
                return liste;

            });

            app.MapGet("/marka/{id}", (Guid id) =>
            {
                Context context = new Context();
                var liste = context.Marka.Where(x => x.Id == id).ToList();
                return liste[0];

            });

            app.MapPost("/marka", (Marka marka) =>
            {
                Context context = new Context();
                context.Marka.Add(marka);
                context.SaveChanges();

            });

            app.MapPost("/model", (Model model) =>
            {
                Context context = new Context();
                context.Model.Add(model);
                context.SaveChanges();

            });

            app.MapGet("/model", () =>
            {
                Context context = new Context();
                return context.Model.ToList();
            });
            app.MapGet("/model/{id}", (Guid id) =>
            {
                Context context = new Context();
                var liste = context.Model.Where(x => x.Id == id).ToList();
                return liste[0];

            });
            app.MapPost("/kullanici", (Kullanici kullanici) =>
            {
                Context context = new Context();
                context.Kullanici.Add(kullanici);
                context.SaveChanges();

            });

            app.MapGet("/kullanici", () =>
            {
                Context context = new Context();
                return context.Kullanici.ToList();
            });

            app.MapPost("/urun", (Urun urun) =>
            {
                Context context = new Context();
                context.Urun.Add(urun);
                context.SaveChanges();
            });

            app.MapPost("/urun/guncelleme", (Urun urun) =>
            {
                Context context = new Context();
                context.Urun.Update(urun);
                context.SaveChanges();
            });

            app.MapPost("/urun/sil", (Urun urun) =>
            {
                Context context = new Context();
                context.Urun.Remove(urun);
                context.SaveChanges();
            });

            app.MapGet("/urun", () =>
            {
                Context context = new Context();
                return context.Urun.ToList();
            });

            app.MapGet("/musteri", () =>
            {
                Context context = new Context();
                var liste = context.Musteri.ToList();
                return liste;

            });

            app.MapPost("/musteri", (Musteri musteri) =>
            {
                Context context = new Context();
                context.Musteri.Add(musteri);
                context.SaveChanges();
            });

            #region duyurular


            app.MapGet("/duyurular", () =>
            {
                Context context = new Context();
                return context.Duyurular.ToList();
            });

            app.MapGet("/duyurular/yeni", () =>
            {
                Context context = new Context();
                return context.Duyurular.Where(x => x.DuyuruTarih >= DateTime.Today).ToList();
            });

            app.MapGet("/duyurular/{id}", (Guid id) =>
            {
                Context context = new Context();
                return context.Duyurular.Where(x => x.Id == id).FirstOrDefault();
            });

            app.MapPost("/duyurular", (Duyuru duyuru) =>
            {
                Context context = new Context();
                context.Duyurular.Add(duyuru);
                context.SaveChanges();
            });

            app.MapDelete("/duyurular/{id}", (Guid id) =>
            {
                Context context = new Context();
                var silinecek = context.Duyurular.Find(id);
                if (silinecek != null)
                {
                    context.Duyurular.Remove(silinecek);
                    context.SaveChanges();
                }
            });

            app.MapGet("/urun/{filterText}", (String filterText) =>
            {
                Context context = new Context();
                var gelenUrun = context.Urun.Where(x => x.Model.Name.StartsWith(filterText)).ToList();
                var gelenUrunAdlari = new List<String>();
                for (int i= 0; i < gelenUrun.Count; i++)
                {
                    gelenUrunAdlari.Add(gelenUrun[i].ModelName);
                }
                return gelenUrun;
            });

            #endregion

            //app.Run();


            //    DuyuruContext context = new DuyuruContext();

            //    //----------------DATABASE VERÝ EKLEME----------------
            //    //context.Duyurular.Add(new Models.Duyuru
            //    //{
            //    //    Id=Guid.NewGuid(),
            //    //    Baslik="AA",
            //    //    DuyuruTarih=DateTime.Now,
            //    //    FotografUrl="https://isparta.edu.tr/logo.png",
            //    //    Icerik="BB",
            //    //    UserId=2,

            //    //});
            //    //context.SaveChanges();

            //    //----------------DATABASE VERÝ GÜNCELLEME----------------

            //    //var seciliDuyuru = context.Duyurular
            //    //.Where(x=> x.Id == Guid.Parse("B7EBF84A - 16E7 - 46FD - 90D1 - 267FD8AB4B86"))
            //    //.FirstOrDefault();

            //    //if (seciliDuyuru is not null)
            //    //{
            //    //    seciliDuyuru.Baslik = "Güncellendi";
            //    //    context.SaveChanges();
            //    //}


            //    //----------------DATABASE VERÝ SÝLME----------------

            //    var seciliDuyuru = context.Duyurular
            //    .Where(x => x.Id == Guid.Parse("B7EBF84A-16E7-46FD-90D1-267FD8AB4B86"))
            //    .FirstOrDefault();

            //    if (seciliDuyuru is not null)
            //    {
            //        context.Duyurular.Remove(seciliDuyuru);
            //        context.SaveChanges();
            //    }

            //    //---------------------------------------------------
            //    var duyurular = context.Duyurular.ToList();
            //    return duyurular;
            //}

            //);

            app.Run();

        }
    }
}
