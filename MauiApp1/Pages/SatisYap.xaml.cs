using MauiApp1.Models;
using MauiApp1.Services;
using System.Collections.ObjectModel;

namespace MauiApp1.Pages;

public partial class SatisYap : ContentPage
{

    IServices services;
    Guid kategoriId;
    Guid markaId;
    Guid modelId;
    Guid urunId;
    Guid musteriId;
    List<String> kategoriListe = new List<String>();
    List<String> markaListe = new List<String>();
    List<String> modelListe = new List<String>();
    List<Urun> filtrele = new List<Urun>();
    List<Musteri> filtreleMusteri= new List<Musteri>();
    List<Urun> silinecekUrun = new List<Urun>();
    ObservableCollection<Urun> urunListele;
    String seciliItemKategori;
    String seciliItemMarka;
    String seciliItemModel;

    public SatisYap()
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

    private async void Button_Clicked(object sender, EventArgs e)
    {
        var gelenUrunListe = await services.GetUrun();
        silinecekUrun = gelenUrunListe.Where(x => x.ModelId == modelId).ToList();
        var silinecekUrunModelId = silinecekUrun[0].ModelId;
        var listeModel = await services.GetModel();
        var urunName = listeModel.Where(x => x.Id == modelId).Select(x=>x.Name).ToList();

        if (silinecekUrun.Count > 0 && filtreleMusteri.Count>0)
        {
            DisplayAlert("Uyarý", $"{urunName[0]} Modeli {filtreleMusteri[0].Name} {filtreleMusteri[0].SurName} Kiþisine Satýldý!", "Tamam");
            await services.Sil(silinecekUrun[0]);
            Navigation.PushAsync(new SatisYap());
        }
        else
        {
            DisplayAlert("Uyarý", "Silinecek Ürün Bulunamadý!", "Tamam");
        }
    }

    private async void KategoriPicker_SelectedIndexChanged(object sender, EventArgs e)
    {
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
        OtomatikDoldur();
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

    private async void OtomatikDoldur()
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
            CreatedDateDatePicker.Date = filtreUrun.CreatedDate;
            PriceEntry.Text = filtreUrun.Price.ToString();







        }
        else
        {
            DisplayAlert("Uyarý", "Böyle bir ürün stokta yok", "Tamam");
        }

    }

    private async void MusteriTel_TextChanged(object sender, TextChangedEventArgs e)
    {
        var gelenMusteriTel = (String)MusteriTel.Text;
        var gelenMusteriTelListe = await services.GetMusteri();
        filtreleMusteri = gelenMusteriTelListe.Where(x => x.Tel == gelenMusteriTel).ToList();

        if (filtreleMusteri.Count > 0)
        {
            musteriId = filtreleMusteri[0].Id;
            var musteriNesnesi = filtreleMusteri[0];
            MusteriAdi.Text = musteriNesnesi.Name;
            MusteriSoyadi.Text = musteriNesnesi.SurName;
            MusteriAdres.Text = musteriNesnesi.Adress;
            MusteriEmail.Text = musteriNesnesi.Email;
            MusteriDogumTarihi.Date = musteriNesnesi.DateOfBirth;
            //SLUrun.IsVisible = true;
            //GuncelleButon.IsVisible = true;
            //SilButon.IsVisible = true;
            //SLKategori.ItemsSource = kategoriListe;
            //SLKategori.SelectedItem = seciliItemKategori;
            //SLMarka.ItemsSource = markaListe;
            //SLMarka.SelectedItem = seciliItemMarka;
            //SLModel.ItemsSource = modelListe;
            //SLModel.SelectedItem = seciliItemModel;
            //SLCreatedDate.Text = filtreUrun.CreatedDate.ToString();
            //SLPrice.Text = filtreUrun.Price.ToString();
        }
        else
        {

        }
    }
}