using MauiApp1.Models;
using MauiApp1.Services;

namespace MauiApp1.Pages;

public partial class MarkaEkle : ContentPage
{
	IServices services;
	Guid kategoriId;
	public MarkaEkle()
	{
		InitializeComponent();
		services = new Service();
		PickerDoldur();

    }

	private async void PickerDoldur()
	{
		var liste = await services.GetKategoriler();
		List<String> yeniListe = new List<String>();
		foreach (var item in liste)
		{
			yeniListe.Add(item.Name);
		}
		KategoriPicker.ItemsSource = yeniListe;
	}

    private async void KategoriPicker_SelectedIndexChanged(object sender, EventArgs e)
    {
		var seciliItem = (String)KategoriPicker.SelectedItem;
		var gelenListe = await services.GetKategoriler();
		var gelenKategori = gelenListe.Where(x=>x.Name == seciliItem).ToList();
		kategoriId = gelenKategori[0].Id;
			
	}

    private async void Button_Clicked(object sender, EventArgs e)
    {
		if(MarkaAdi.Text!=null && kategoriId != null)
		{
			var marka = new Marka()
			{
				Id = Guid.NewGuid(),
				Name = MarkaAdi.Text,
				KategoriId = kategoriId
			};
			await services.Ekle(marka);
            await DisplayAlert("Ýþlem Baþarýlý", "Marka Baþarýyla Eklendi", "Tamam");
            MarkaAdi.Text = String.Empty;
        }
        else
        {
            await DisplayAlert("HATA", "Marka Adý veya Kategori Alaný Boþ Býrakýlamaz", "Tamam");
        }
    }
}