using MauiApp1.Models;
using MauiApp1.Services;
using Microsoft.Maui.Graphics.Text;

namespace MauiApp1.Pages;

public partial class KategoriEkle : ContentPage
{
	IServices service;

	public KategoriEkle()
	{
		InitializeComponent();

		service = new Service();
	}

    private async void Button_Clicked(object sender, EventArgs e)
    {

		var gelenAd = KategoriAdi.Text;
		if (gelenAd!=null)
		{
			var kategori = new Kategori() {
                Id = Guid.NewGuid(),
				Name = gelenAd,
			};
			await service.Ekle(kategori);
			await DisplayAlert("Ýþlem Baþarýlý", "Kategoriyi Baþarýyla Eklediniz", "Tamam");
			KategoriAdi.Text = String.Empty;
		}
		else
		{
            await DisplayAlert("HATA", "Kategori Adý Boþ Býrakýlamaz", "Tamam");
        }
    }
}