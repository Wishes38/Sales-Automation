using MauiApp1.Models;
using MauiApp1.Services;
using System.Collections.ObjectModel;

namespace MauiApp1.Pages;

public partial class SearchPage : ContentPage
{
	IServices services;
    ObservableCollection<Urun> urun;
    List<Urun>? gelecekUrun = new List<Urun>();
    List<Model> gelecekModel = new List<Model>();
    Guid modelId;
    string arananKelime;
    public SearchPage()
	{
		InitializeComponent();
        urun = new ObservableCollection<Urun>();
        CVArama.ItemsSource = urun;
        services = new Service();
        
    }

    private async void modelAdGetir()
    {
        var gelenUrunListe = await services.GetUrun();
        //gelecekUrun = gelenUrunListe.Where(x => x.ModelId == modelId).ToList();
        gelecekUrun = gelenUrunListe.ToList();
        var listeModel = await services.GetModel();
        if (arananKelime != String.Empty)
        {
            gelecekModel = listeModel.Where(x => x.Name.StartsWith(arananKelime)).ToList();
        
        

        if (arananKelime != null && gelecekUrun.Count > 0)
        {
            var silinecekUrunModelId = gelecekUrun[0].ModelId;
            
            var modelName = listeModel.Where(x => x.Name.StartsWith(arananKelime)).Select(x => x.Name).ToList();
            if (modelName.Count>0)
            {
                ModelNameLabel.IsVisible = true;
                    for (int i = 0; i < modelName.Count; i++)
                    {
                        ModelNameLabel.Text += $"{i+1}. Ürün Modeli: - {modelName[i]}\n";
                    }
                
            }
            }
        }

    }

    private async void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
    {
        ModelNameLabel.IsVisible = false;
        arananKelime = SearchBar.Text;

        if (String.IsNullOrEmpty(e.NewTextValue))
        {
            CVArama.ItemsSource = null;
            
        }
        else
        {
            var gelenUrun = await services.SearchBar(e.NewTextValue);
            CVArama.ItemsSource = gelenUrun;
        }
        modelAdGetir();
    }

    

}