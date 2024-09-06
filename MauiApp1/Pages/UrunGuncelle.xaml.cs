using MauiApp1.Models;
using MauiApp1.Services;
using System.Collections.ObjectModel;

namespace MauiApp1.Pages;

public partial class UrunGuncelle : ContentPage
{
    IServices services;
    Guid kategoriId;
    Guid markaId;
    Guid modelId;
    Guid kategoriIdGuncelleme;
    Guid markaIdGuncelleme;
    Guid modelIdGuncelleme;
    List<String> kategoriListe = new List<String>();
    List<String> markaListe = new List<String>();
    List<String> modelListe = new List<String>();
    List<Urun> filtrele = new List<Urun>();
    ObservableCollection<Urun> urunListele;
    String seciliItemKategori;
    String seciliItemMarka;
    String seciliItemModel;
    String seciliItemKategoriGuncelleme;
    String seciliItemMarkaGuncelleme;
    String seciliItemModelGuncelleme;

    public UrunGuncelle()
	{
		InitializeComponent();
        services = new Service();
        PickerKategoriDoldur();
        urunListele = new ObservableCollection<Urun>();
        
	}
    private async void PickerKategoriDoldur()
    {
        var liste = await services.GetKategoriler();
      
        foreach (var item in liste)
        {
            kategoriListe.Add(item.Name);
        }
        KategoriPicker.ItemsSource = kategoriListe;
    }

    //private async Task GetUrunler()
    //{
    //    var sonuc = await services.GetUrun();
    //    urunListele.Clear();
    //    foreach (var item in sonuc)
    //    {
    //        urunListele.Add(item);
    //    }

    //}

    //protected override async void OnAppearing()
    //{
    //    base.OnAppearing();
    //    GetUrunler();
    //}

    private async void Button_Clicked(object sender, EventArgs e)
    {
        var gelenModel = (String)ModelPicker.SelectedItem;
        var gelenModelListe = await services.GetModel();
        var model = gelenModelListe.Where(x => x.Name == gelenModel).ToList();

        if (model.Count > 0)
        {
            modelId = model[0].Id;
        }
        else
        {

        }

        var gelenUrun = await services.GetUrun();
         filtrele = gelenUrun.Where(x => x.ModelId == modelId).ToList();
        

        if (filtrele.Count > 0)
        {
            var filtreUrun = filtrele[0];
            SLUrun.IsVisible = true;
            GuncelleButon.IsVisible = true;
            SilButon.IsVisible = true;
            SLKategori.ItemsSource = kategoriListe;
            SLKategori.SelectedItem = seciliItemKategori;
            SLMarka.ItemsSource = markaListe;
            SLMarka.SelectedItem = seciliItemMarka;
            SLModel.ItemsSource = modelListe;
            SLModel.SelectedItem = seciliItemModel;
            SLCreatedDate.Text = filtreUrun.CreatedDate.ToString();
            SLPrice.Text = filtreUrun.Price.ToString();







        }
        else
        {
            DisplayAlert("Uyarý", "Böyle bir ürün stokta yok", "Tamam");
        }
        
    }

   

    private async void KategoriPicker_SelectedIndexChanged(object sender, EventArgs e)
    {
        //MarkaPicker.SelectedItem = null;
        seciliItemKategori = (String)KategoriPicker.SelectedItem;
        var gelenListe = await services.GetKategoriler();
        var gelenKategori = gelenListe.Where(x => x.Name == seciliItemKategori).ToList();

        if (gelenKategori.Count > 0)
        {

            kategoriId = gelenKategori[0].Id;
        }

        PickerMarkaDoldur();
    }

    private async void MarkaPicker_SelectedIndexChanged(object sender, EventArgs e)
    {
        seciliItemMarka = (string)MarkaPicker.SelectedItem;
        var gelenListe = await services.GetMarka();
        var gelenMarka = gelenListe.Where(x => x.Name == seciliItemMarka).ToList();

        if (gelenMarka.Count > 0)
        {
            markaId = gelenMarka.FirstOrDefault().Id;
        }

        PickerModelDoldur();
    }

    private async void PickerMarkaDoldur()
    {
        var liste = await services.GetMarka();
        var filtrele = liste.Where(x => x.KategoriId == kategoriId).ToList();
        markaListe.Clear();
        MarkaPicker.ItemsSource = null;
        foreach (var item in filtrele)
        {
            
            markaListe.Add(item.Name); //item.Name
        }
        MarkaPicker.ItemsSource = markaListe;
    }

    private async void PickerMarkaGuncelle()
    {
        var liste = await services.GetMarka();
        var filtrele = liste.Where(x => x.KategoriId == kategoriIdGuncelleme).ToList();
        markaListe.Clear();
        SLMarka.ItemsSource = null;
        
        foreach (var item in filtrele)
        {

            markaListe.Add(item.Name); //item.Name
        }
        SLMarka.ItemsSource = markaListe;
        SLMarka.SelectedItem = seciliItemMarka;
    }

    private async void ModelPicker_SelectedIndexChanged(object sender, EventArgs e)
    {
        seciliItemModel = (string)ModelPicker.SelectedItem;
        var gelenListe = await services.GetModel();
        var gelenModel = gelenListe.Where(x => x.Name == seciliItemModel).ToList();

        if (gelenModel.Count > 0)
        {
            modelId = gelenModel[0].Id;
        }
        else
        {

        }
    }

    private async void PickerModelDoldur()
    {
        var liste = await services.GetModel();
        var filtrele = liste.Where(x => x.MarkaId == markaId).ToList();
        modelListe.Clear();
        ModelPicker.ItemsSource = null;
        foreach (var item in filtrele)
        {
           
            modelListe.Add(item.Name); //item.Name
        }
        ModelPicker.ItemsSource = modelListe;
    }

    private async void PickerModelGuncelle()
    {
        var liste = await services.GetModel();
        var filtrele = liste.Where(x => x.MarkaId == markaIdGuncelleme).ToList();
        modelListe.Clear();
        SLModel.ItemsSource = null;
        foreach (var item in filtrele)
        {

            modelListe.Add(item.Name); //item.Name
        }
        SLModel.ItemsSource = modelListe;
        SLModel.SelectedItem = seciliItemModel;
    }



    private async void GuncelleButon_Clicked(object sender, EventArgs e)
    {
        var gelenKategori = (String)SLKategori.SelectedItem;
        var gelenListeKategori = await services.GetKategoriler();
        var kategori = gelenListeKategori.Where(x => x.Name == gelenKategori).ToList();
        var gelenMarka = (String)SLMarka.SelectedItem;
        var gelenListeMarka = await services.GetMarka();
        var marka = gelenListeMarka.Where(x => x.Name == gelenMarka).ToList();
        var gelenModel = (String)SLModel.SelectedItem;
        var gelenListeModel = await services.GetModel();
        var model = gelenListeModel.Where(x => x.Name == gelenModel).ToList();
        DateTime createdDate = filtrele[0].CreatedDate;
        double price = Convert.ToDouble(SLPrice.Text);

        if (kategori[0] != filtrele[0].Kategori || marka[0] != filtrele[0].Marka || model[0] != filtrele[0].Model || price != filtrele[0].Price)
        {
            var urun = new Urun()
            {
                Id = filtrele[0].Id,                
                CreatedDate = createdDate,
                Price = price,
                KategoriId = kategori[0].Id,
                MarkaId = marka[0].Id,
                ModelId = model[0].Id,
                
            };
            await services.Guncelle(urun);
            await DisplayAlert("Ýþlem Baþarýlý", "Ürün Baþarýyla Güncellendi", "Tamam");           
        }

        else
        {
            await DisplayAlert("HATA", "Herhangi bir alan güncellenmedi", "Tamam");
        }
    }

    private async void SLKategori_SelectedIndexChanged(object sender, EventArgs e)
    {
        seciliItemKategoriGuncelleme = (String)SLKategori.SelectedItem;
        var gelenListe = await services.GetKategoriler();
        var gelenKategori = gelenListe.Where(x => x.Name == seciliItemKategoriGuncelleme).ToList();

        if (gelenKategori.Count > 0)
        {

            kategoriIdGuncelleme = gelenKategori[0].Id;
        }

        PickerMarkaGuncelle();
    }

    private async void SLMarka_SelectedIndexChanged(object sender, EventArgs e)
    {
        seciliItemMarkaGuncelleme = (string)SLMarka.SelectedItem;
        var gelenListe = await services.GetMarka();
        var gelenMarka = gelenListe.Where(x => x.Name == seciliItemMarkaGuncelleme).ToList();

        if (gelenMarka.Count > 0)
        {
            markaIdGuncelleme = gelenMarka.FirstOrDefault().Id;
        }

        PickerModelGuncelle();
    }

    private async void SLModel_SelectedIndexChanged(object sender, EventArgs e)
    {
        seciliItemModelGuncelleme = (string)SLModel.SelectedItem;
        var gelenListe = await services.GetModel();
        var gelenModel = gelenListe.Where(x => x.Name == seciliItemModelGuncelleme).ToList();

        if (gelenModel.Count > 0)
        {
            modelIdGuncelleme = gelenModel[0].Id;
        }
        else
        {

        }
    }

    private async void SilButon_Clicked(object sender, EventArgs e)
    {

        await services.Sil(filtrele[0]);
        DisplayAlert("Uyarý", "Ürün Silindi!", "Tamam");
        Navigation.PushAsync(new UrunGuncelle());

    }
}