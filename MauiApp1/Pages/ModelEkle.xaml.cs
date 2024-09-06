using MauiApp1.Models;
using MauiApp1.Services;

namespace MauiApp1.Pages;

public partial class ModelEkle : ContentPage
{
    IServices services;
    Guid kategoriId;
    Guid markaId;
    
    public ModelEkle()
	{
		InitializeComponent();
        services = new Service();
        PickerKategoriDoldur();
    }

    private async void PickerKategoriDoldur()
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
        MarkaPicker.SelectedItem = null;
        var seciliItem = (String)KategoriPicker.SelectedItem;
        var gelenListe = await services.GetKategoriler();
        var gelenKategori = gelenListe.Where(x => x.Name == seciliItem).ToList();
        kategoriId = gelenKategori[0].Id;
        PickerMarkaDoldur();
    }

    private async void PickerMarkaDoldur()
    {
        var liste = await services.GetMarka();
        var filtrele = liste.Where(x => x.KategoriId == kategoriId).ToList();
        List<String> yeniListe = new List<String>();
        foreach (var item in filtrele)
        {
            yeniListe.Add(item.Name);
        }
        MarkaPicker.ItemsSource = yeniListe;
    }


    private async void MarkaPicker_SelectedIndexChanged(object sender, EventArgs e)
    {
        var seciliItem = (string)MarkaPicker.SelectedItem;
        var gelenListe = await services.GetMarka();

        if (gelenListe != null && gelenListe.Count > 0)
        {
            var gelenMarka = gelenListe.Where(x => x.Name == seciliItem).ToList();

            if (gelenMarka != null && gelenMarka.Count > 0)
            {
                markaId = gelenMarka.FirstOrDefault().Id;
            }
        }
    }


    private async void Button_Clicked(object sender, EventArgs e)
    {
        if (ModelAdi.Text != null && markaId != null && kategoriId != null)
        {
            var model = new Model()
            {
                Id = Guid.NewGuid(),
                Name = ModelAdi.Text,
                MarkaId = markaId
            };
            await services.Ekle(model);
            await DisplayAlert("Ýþlem Baþarýlý", "Model Baþarýyla Eklendi", "Tamam");
            ModelAdi.Text = String.Empty;
        }
        else
        {
            await DisplayAlert("HATA", "Model Adý ve Kategori Alaný Boþ Býrakýlamaz", "Tamam");
        }
    }


}