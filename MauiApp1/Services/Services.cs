using MauiApp1.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MauiApp1.Services
{
    public interface IServices 
    {
        Task Ekle(Kategori kategori);

        Task Ekle(Marka marka);
        Task<List<Kategori>> GetKategoriler();

        Task<String> GetKategoriName(Guid id);
        Task Ekle(Model model);
        Task<List<Marka>> GetMarka();
        Task<String> GetMarkaName(Guid id);
        Task Ekle(Kullanici kullanici);

        Task<List<Kullanici>> GetKullanici();
        Task Ekle(Urun urun);

        Task Sil(Urun urun);

        Task Guncelle(Urun urun);

        Task<List<Urun>> GetUrun();

        Task<List<Model>> GetModel();
        Task<String> GetModelName(Guid id);
        Task<List<Musteri>> GetMusteri();

        Task Ekle(Musteri musteri);

        Task<List<Urun>> SearchBar(String filterText);
    }

    public class UrlHelper
    {
        public static string BaseAddress =
            DeviceInfo.Platform == DevicePlatform.Android ? "https://10.0.2.2:7044" : "https://localhost:7044";
        
        public static string DuyuruUrl = $"{BaseAddress}/duyurular";
        public static string KategoriUrl = $"{BaseAddress}/kategori";
        public static string MarkaUrl = $"{BaseAddress}/marka";
        public static string ModelUrl = $"{BaseAddress}/model";
        public static string KullaniciUrl = $"{BaseAddress}/kullanici";
        public static string UrunUrl = $"{BaseAddress}/urun";
        public static string UrunGuncelleUrl = $"{UrunUrl}/guncelleme";
        public static string UrunSilUrl = $"{UrunUrl}/sil";
        public static string MusteriUrl = $"{BaseAddress}/musteri";
        //public static string UrunAramaUrl = $"{UrunUrl}/{filterText}";

    }
    public class Service : IServices
    {

        HttpClient httpClient;
        JsonSerializerOptions jsonSerializerOptions;

        public Service()
        {
            httpClient = new HttpClient();

            jsonSerializerOptions = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };
        }
        public async Task Ekle(Kategori kategori)
        {
            var json = JsonSerializer.Serialize(kategori);

            JsonContent jsonContent = JsonContent.Create(kategori);
            var responce = await httpClient.PostAsync("https://localhost:7044/kategori", jsonContent);
            if (responce.IsSuccessStatusCode)
            {

            }
        }

        public async Task Ekle(Marka marka)
        {
            var json = JsonSerializer.Serialize(marka);

            JsonContent jsonContent = JsonContent.Create(marka);
            var responce = await httpClient.PostAsync(UrlHelper.MarkaUrl, jsonContent);
            if (responce.IsSuccessStatusCode)
            {

            }
        }

        public async Task Ekle(Model model)
        {
            var json = JsonSerializer.Serialize(model);

            JsonContent jsonContent = JsonContent.Create(model);
            var responce = await httpClient.PostAsync(UrlHelper.ModelUrl, jsonContent);
            if (responce.IsSuccessStatusCode)
            {

            }
        }

        public async Task Ekle(Kullanici kullanici)
        {
            var json = JsonSerializer.Serialize(kullanici);

            JsonContent jsonContent = JsonContent.Create(kullanici);
            var responce = await httpClient.PostAsync(UrlHelper.KullaniciUrl, jsonContent);
            if (responce.IsSuccessStatusCode)
            {

            }
        }

        public async Task Ekle(Urun urun)
        {
            var json = JsonSerializer.Serialize(urun);

            JsonContent jsonContent = JsonContent.Create(urun);
            var responce = await httpClient.PostAsync(UrlHelper.UrunUrl, jsonContent);
            if (responce.IsSuccessStatusCode)
            {

            }
        }

        public async Task Ekle(Musteri musteri)
        {
            var json = JsonSerializer.Serialize(musteri);

            JsonContent jsonContent = JsonContent.Create(musteri);
            var responce = await httpClient.PostAsync(UrlHelper.MusteriUrl, jsonContent);
            if (responce.IsSuccessStatusCode)
            {

            }
        }

        public async Task<List<Kategori>> GetKategoriler()
        {
            var sonuc = await httpClient.GetFromJsonAsync<List<Kategori>>(UrlHelper.KategoriUrl);
            return sonuc;

        }

        public async Task<string> GetKategoriName(Guid id)
        {
            var sonuc = await httpClient.GetFromJsonAsync<Kategori>($"{UrlHelper.KategoriUrl}/{id}");
            return sonuc.Name;
        }

        public async Task<List<Kullanici>> GetKullanici()
        {
            var sonuc = await httpClient.GetFromJsonAsync<List<Kullanici>>(UrlHelper.KullaniciUrl);
            return sonuc;
        }

        public async Task<List<Marka>> GetMarka()
        {
            var sonuc = await httpClient.GetFromJsonAsync<List<Marka>>(UrlHelper.MarkaUrl);
            return sonuc;
        }

        public async Task<string> GetMarkaName(Guid id)
        {
            var sonuc = await httpClient.GetFromJsonAsync<Marka>($"{UrlHelper.MarkaUrl}/{id}");
            return sonuc.Name;
        }

        public async Task<List<Model>> GetModel()
        {
            var sonuc = await httpClient.GetFromJsonAsync<List<Model>>(UrlHelper.ModelUrl);
            return sonuc;
        }

        public async Task<string> GetModelName(Guid id)
        {
            var sonuc = await httpClient.GetFromJsonAsync<Model>($"{UrlHelper.ModelUrl}/{id}");
            return sonuc.Name;
        }

        public async Task<List<Musteri>> GetMusteri()
        {
            var sonuc = await httpClient.GetFromJsonAsync<List<Musteri>>(UrlHelper.MusteriUrl);
            return sonuc;
        }

        public async Task<List<Urun>> GetUrun()
        {
            var sonuc = await httpClient.GetFromJsonAsync<List<Urun>>(UrlHelper.UrunUrl);
            return sonuc;
        }

        public async Task Guncelle(Urun urun)
        {
            var json = JsonSerializer.Serialize(urun);

            JsonContent jsonContent = JsonContent.Create(urun);
            var responce = await httpClient.PostAsync(UrlHelper.UrunGuncelleUrl, jsonContent);
            if (responce.IsSuccessStatusCode)
            {

            }
        }

        public async Task<List<Urun>> SearchBar(string filterText)
        {
            var result = await httpClient.GetFromJsonAsync<List<Urun>>($"https://localhost:7044/urun/{filterText}");
            return result;  
        }

        public async Task Sil(Urun urun)
        {
            var json = JsonSerializer.Serialize(urun);

            JsonContent jsonContent = JsonContent.Create(urun);
            var responce = await httpClient.PostAsync(UrlHelper.UrunSilUrl, jsonContent);
            if (responce.IsSuccessStatusCode)
            {

            }
        }
    }
}
