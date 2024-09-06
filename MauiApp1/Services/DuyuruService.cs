using MauiApp1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace MauiApp1.Services
{
    internal interface IDuyuruService
    {
        Task<List<Duyuru>> GetDuyurular();
        Task Ekle(Duyuru duyuru);
        Task Sil(Guid duyuruId);
    }


    public class DuyuruService : IDuyuruService
    {
        HttpClient httpClient;
        JsonSerializerOptions jsonSerializerOptions;
        public DuyuruService()
        {
#if (DEBUG && ANDROID)
            HttpClientHandlerService handler = new HttpClientHandlerService();
            httpClient = new HttpClient(handler.GetPlatformMessageHandler());

#else
            httpClient = new HttpClient();
#endif




            jsonSerializerOptions = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };
        }

        public async Task Ekle(Duyuru duyuru)
        {
            var json = JsonSerializer.Serialize(duyuru);

            JsonContent jsonContent = JsonContent.Create(duyuru);
            var responce = await httpClient.PostAsync(UrlHelper.DuyuruUrl, jsonContent);
            if (responce.IsSuccessStatusCode)
            {

            }
        }

        public async Task<List<Duyuru>> GetDuyurular()
        {
            var sonuc = await httpClient.GetFromJsonAsync<List<Duyuru>>(UrlHelper.DuyuruUrl);
            return sonuc;

            //var responce = await httpClient.GetAsync(UrlHelper.DuyuruUrl);
            //if (responce.IsSuccessStatusCode)
            //{
            //    string jsonContent = await responce.Content.ReadAsStringAsync();
            //    var sonuc = JsonSerializer.Deserialize<List<Duyuru>>(jsonContent, jsonSerializerOptions);

            //    return sonuc;
            //}
            //return new List<Duyuru>();
        }

        public async Task Sil(Guid duyuruId)
        {
            var url = UrlHelper.DuyuruUrl + $"/{duyuruId}";
            await httpClient.DeleteAsync(url);
        }
    }
    public class HttpClientHandlerService
    {
        public HttpMessageHandler GetPlatformMessageHandler()
        {
#if ANDROID
            var handler = new Xamarin.Android.Net.AndroidMessageHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
            {
                if (cert != null && cert.Issuer.Equals("CN=localhost"))
                    return true;
                return errors == System.Net.Security.SslPolicyErrors.None;

            };
            return handler;
        
#elif IOS
            var handler = new NSUrlSessionHandler
            {
                TrustOverrideForUrl = IsHttpLocalhost
            };
            return handler;
#else
        throw new PlatformNotSupportedException("Only Android and iOS supported.");
#endif
        }

#if IOS
        public bool IsHttpLocalhost(NSUrlSessionHandler sender, string url, Security.SecTrust trust)
        {
            if (url.StartsWith("https://localhost"))
                return true;
            return false;
        }  
#endif
    }   
}
