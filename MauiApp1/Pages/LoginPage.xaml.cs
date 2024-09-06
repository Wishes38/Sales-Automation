using MauiApp1.Models;
using MauiApp1.Services;

namespace MauiApp1.Pages;

public partial class LoginPage : ContentPage
{

	IServices services;
    Kullanici userName;
    Kullanici passWord;
	public LoginPage()
	{
		InitializeComponent();
        services = new Service();
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        String kullaniciAdi = "null";
        String sifre = "null";

        if (UserNameEntry.Text != null && UserNameEntry.Text != "")
        {
            kullaniciAdi = UserNameEntry.Text;
        }

        if (PasswordEntry.Text != null && PasswordEntry.Text != "")
        {
            sifre = PasswordEntry.Text;
        }


        var Kullanicilar = await services.GetKullanici();

        if (Kullanicilar != null && Kullanicilar.Count > 0)
        {
            for (int i=0; i < Kullanicilar.Count;i++)
            {
                if (kullaniciAdi == Kullanicilar[i].UserName.ToString() && sifre == Kullanicilar[i].Password.ToString())
                {
                    await Navigation.PushAsync(new HomePage());
                }
                else
                {
                    await DisplayAlert("HATA", "Kullanýcý adý veya þifre hatalý.", "Tamam");
                }
            }
            
        }
        
        else
        {
            await DisplayAlert("HATA", "Kullanýcý Yok", "Tamam");

        }
        

    }

}