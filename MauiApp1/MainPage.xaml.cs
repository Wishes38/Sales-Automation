using MauiApp1.Models;
using MauiApp1.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MauiApp1;

public partial class MainPage : ContentPage
{

    public ICommand DuyuruSilCommand { get; set; }
    public ICommand DuyuruAyrintiCommand { get; set; }

    public ICommand DuyuruRefreshCommand { get; set; }

    ObservableCollection<Duyuru> duyurular;


	IDuyuruService duyuruService;
	public MainPage()
	{

        InitializeComponent();

		duyuruService = new DuyuruService();
        duyurular = new ObservableCollection<Duyuru>();
        lstDuyuru.ItemsSource = duyurular;

        DuyuruSilCommand = new Command<Duyuru>(async (Duyuru seciliDuyuru) => 
        {
            
            bool cevap =await DisplayAlert("Mesaj","Bu duyuruyu silmek istediğinize emin misiniz?","Evet","Hayır");
            if (cevap) 
            { 
                await duyuruService.Sil(seciliDuyuru.Id);
                await GetDuyurular();
            }
        });

        DuyuruAyrintiCommand = new Command<Duyuru>(async (Duyuru seciliDuyuru) =>
        {
            await Navigation.PushModalAsync(new Pages.DuyuruAyrintiPage(seciliDuyuru));
        });

        DuyuruRefreshCommand = new Command(async () =>
        {
            await duyuruService.GetDuyurular();
        });

        this.BindingContext = this;

    }


    private async void btnLogin_Clicked(object sender, EventArgs e)
    {
        await GetDuyurular();
    }

    private async Task GetDuyurular()
    {
        var sonuc = await duyuruService.GetDuyurular();
        duyurular.Clear();
        foreach (var item in sonuc)
        {
            duyurular.Add(item);
        }
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        var duyuru = new Duyuru()
        {
            Baslik = txtBaslik.Text,
            DuyuruTarih = DateTime.Now,
            FotografUrl = "",
            Icerik="Deneme İçeriği",
            Id= Guid.NewGuid(),
            UserId = 3
        };
        await duyuruService.Ekle(duyuru);
        await GetDuyurular();
    }

    private async void lstDuyuru_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var seciliDuyuru = (Duyuru)lstDuyuru.SelectedItem;
        if (seciliDuyuru!=null)
        {
            await Navigation.PushModalAsync(new Pages.DuyuruAyrintiPage(seciliDuyuru));
        }
    }

    private void SwipeItem_Clicked(object sender, EventArgs e)
    {
        //var seciliDuyuru = (Duyuru)lstDuyuru.SelectedItem;
    }
}

