using MauiApp1.Models;
using MauiApp1.Services;
using Microsoft.Maui.Graphics.Text;
using System.Linq;

namespace MauiApp1.Pages;

public partial class UrunEkle : ContentPage
{
    IServices services;
    Guid kategoriId;
    Guid markaId;
    Guid modelId;

    public UrunEkle()
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
        //MarkaPicker.SelectedItem = null;
        var seciliItem = (String)KategoriPicker.SelectedItem;
        var gelenListe = await services.GetKategoriler();
        var gelenKategori = gelenListe.Where(x => x.Name == seciliItem).ToList();

        if (gelenKategori.Count > 0)
        {
            kategoriId = gelenKategori[0].Id;
        }
        
        PickerMarkaDoldur();
    }

    private async void PickerMarkaDoldur()
    {
        var liste = await services.GetMarka();
        var filtrele = liste.Where(x => x.KategoriId == kategoriId).ToList();
        List<String> yeniListe = new List<String>();
        foreach (var item in filtrele)
        {
            yeniListe.Add(item.Name); //item.Name
        }
        MarkaPicker.ItemsSource = yeniListe;
    }

    private async void MarkaPicker_SelectedIndexChanged(object sender, EventArgs e)
    {
        var seciliItem = (string)MarkaPicker.SelectedItem;
        var gelenListe = await services.GetMarka();
        var gelenMarka = gelenListe.Where(x => x.Name == seciliItem).ToList();

        if (gelenMarka.Count > 0)
        {
            markaId = gelenMarka.FirstOrDefault().Id;
        }

        PickerModelDoldur();
    }


    private async void PickerModelDoldur()
    {
        var liste = await services.GetModel();
        var filtrele = liste.Where(x => x.MarkaId == markaId).ToList();
        List<String> yeniListe = new List<String>();
        foreach (var item in filtrele)
        {
            yeniListe.Add(item.Name); //item.Name
        }
        ModelPicker.ItemsSource = yeniListe;
    }


    private async void ModelPicker_SelectedIndexChanged(object sender, EventArgs e)
    {
        var seciliItem = (string)ModelPicker.SelectedItem;
        var gelenListe = await services.GetModel();
        var gelenModel = gelenListe.Where(x => x.Name == seciliItem).ToList();

        if (gelenModel.Count > 0)
        {
            modelId = gelenModel[0].Id;
        }
        else
        {
            
        }
    }


    private async void KaydetButton_Clicked(object sender, EventArgs e)
    {
        var gelenKategori = (String)KategoriPicker.SelectedItem;
        var gelenListeKategori = await services.GetKategoriler();
        var kategori = gelenListeKategori.Where(x => x.Name == gelenKategori).ToList();
        var gelenMarka = (String)MarkaPicker.SelectedItem;
        var gelenListeMarka = await services.GetMarka();
        var marka = gelenListeMarka.Where(x => x.Name == gelenMarka).ToList();
        var gelenModel = (String)ModelPicker.SelectedItem;
        var gelenListeModel = await services.GetModel();
        var model = gelenListeModel.Where(x => x.Name == gelenModel).ToList();
        DateTime createdDate = CreatedDateDatePicker.Date;
        double price = double.Parse(PriceEntry.Text);

        if (kategori[0] != null && marka[0] != null && model[0] != null && price != null)
        {
            var urun = new Urun()
            {
                Id = Guid.NewGuid(),
                //Kategori = kategori[0],
                //Marka = marka[0],
                //Model = model[0],
                CreatedDate = createdDate,
                Price = price,
                KategoriId = kategori[0].Id,
                MarkaId = marka[0].Id,
                ModelId = model[0].Id,
            };
            await services.Ekle(urun);
            await DisplayAlert("Ýþlem Baþarýlý", "Ürün Baþarýyla Ekledindi", "Tamam");
            KategoriPicker.SelectedItem = null;
            MarkaPicker.SelectedItem = null;
            ModelPicker.SelectedItem = null;
            PriceEntry.Text = null;
        }

        else
        {
            await DisplayAlert("HATA", "Lütfen boþ alan býrakmayýnýz!", "Tamam");
        }

    }


}