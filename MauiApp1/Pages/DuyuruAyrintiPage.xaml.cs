using MauiApp1.Models;

namespace MauiApp1.Pages;

public partial class DuyuruAyrintiPage : ContentPage
{
	public DuyuruAyrintiPage(Duyuru duyuru)
    {
        InitializeComponent();

        SayfaDoldur(duyuru);
    }

    private void SayfaDoldur(Duyuru duyuru)
    {
        lblBaslik.Text = duyuru.Baslik;
        lblIcerik.Text = duyuru.Icerik;
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }
}