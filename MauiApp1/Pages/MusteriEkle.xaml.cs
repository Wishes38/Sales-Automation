using MauiApp1.Models;
using MauiApp1.Services;
using System.IO;
using System.Xml.Linq;

namespace MauiApp1.Pages;

public partial class MusteriEkle : ContentPage
{
    IServices service;
    Guid musteriId;
    public MusteriEkle()
	{
		InitializeComponent();

        service = new Service();
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        var musteriListe = await service.GetMusteri();

        var musteriIsmi = MusteriAdi.Text;
        var musteriSoyadi = MusteriSoyadi.Text;
        var musteriTel = MusteriTel.Text;
        var musteriAdres = MusteriAdres.Text;
        var musteriEmail = MusteriEmail.Text;
        var musteriDateOfBirth = MusteriDogumTarihi.Date;


        if (musteriIsmi != null && musteriSoyadi != null && musteriTel != null && musteriAdres != null && musteriEmail!=null)
        {
            var musteri = new Musteri()
        {
            Id = Guid.NewGuid(),
            Name = musteriIsmi,
            SurName = musteriSoyadi,
            Tel = musteriTel,
            Adress = musteriAdres,
            Email = musteriEmail,
            DateOfBirth = musteriDateOfBirth,
            
            };
            musteriId = Id;

        int sayi = musteriListe.Where(x => x.Id == musteriId).Count();
            switch (sayi)
        {
            case 1:
                break;
            case 0:
                await service.Ekle(musteri);
                await DisplayAlert("Ýþlem Baþarýlý", "Müþteri Baþarýyla Eklendi", "Tamam");
                break;
            default:
                break;
        }

        MusteriAdi.Text = String.Empty;
        MusteriSoyadi.Text = String.Empty;
        MusteriTel.Text = String.Empty;
        MusteriAdres.Text = String.Empty;
        MusteriEmail.Text = String.Empty;
        MusteriDogumTarihi.Date = DateTime.Now;
    }

        else
        {
            await DisplayAlert("HATA", "Lütfen boþ alanlarý doldurunuz", "Tamam");
}
    }
}