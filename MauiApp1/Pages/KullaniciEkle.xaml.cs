using MauiApp1.Models;
using MauiApp1.Services;

namespace MauiApp1.Pages;

public partial class KullaniciEkle : ContentPage
{
    IServices service;
    public KullaniciEkle()
	{
		InitializeComponent();

        service = new Service();
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        var kullaniciListe = await service.GetKullanici();

        var kullaniciAdi = KullaniciAdi.Text;
        var kullaniciSifre = KullaniciSifre.Text;
        var kullaniciIsmi = KullaniciIsmi.Text;
        var kullaniciSoyadi = KullaniciSoyadi.Text;
        var kullaniciUnvani = KullaniciUnvani.Text;

        int sayi = kullaniciListe.Where(x => x.UserName == kullaniciAdi).Count();


        if (kullaniciSifre != null && kullaniciAdi != null && kullaniciSoyadi != null  && kullaniciUnvani != null)
        {
            /*
            foreach (var item in kullaniciListe)
            {
                if (item.UserName == kullaniciAdi)
                {
                    gelen = "HATA";
                    await DisplayAlert("HATA", "Bu kullanýcý adý kullanýlýyor, Lütfen farklý bir kullanýcý adý giriniz.", "Tamam");
                }
            }*/

            var kullanici = new Kullanici()
            {
                Id = Guid.NewGuid(),
                UserName = kullaniciAdi,
                Password = kullaniciSifre,
                Name = kullaniciAdi,
                SurName = kullaniciSoyadi,
                Title = kullaniciUnvani,
            };
            /*
            switch (gelen)
            {
                case "HATA":
                    break;
                case null:
                    await service.Ekle(kullanici);
                    await DisplayAlert("Ýþlem Baþarýlý", "Kullanýcý Baþarýyla Eklendi", "Tamam");
                    break;
                default:
                    break;
            }*/

            switch (sayi)
            {
                case 1:
                    break;
                case 0:
                    await service.Ekle(kullanici);
                    await DisplayAlert("Ýþlem Baþarýlý", "Kullanýcý Baþarýyla Eklendi", "Tamam");
                    break;
                default:
                    break;
            }
            

            KullaniciAdi.Text = String.Empty;
            KullaniciSifre.Text = String.Empty;
            KullaniciIsmi.Text = String.Empty;
            KullaniciSoyadi.Text = String.Empty;
            KullaniciUnvani.Text = String.Empty;
        }

        else
        {
            await DisplayAlert("HATA", "Lütfen boþ alanlarý doldurunuz", "Tamam");
        }

        


    }
}