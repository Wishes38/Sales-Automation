namespace MauiApp1.Pages;

public partial class HomePage : ContentPage
{
	public HomePage()
	{
		InitializeComponent();
	}

    private void MenuFlyoutItem_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new UrunEkle());
    }

    private void MenuFlyoutItem_Clicked_1(object sender, EventArgs e)
    {
        Navigation.PushAsync(new UrunGuncelle());
    }


    private void MenuFlyoutItem_Clicked_3(object sender, EventArgs e)
    {
        Navigation.PushAsync(new KategoriEkle());
    }

    private void MenuFlyoutItem_Clicked_4(object sender, EventArgs e)
    {
        Navigation.PushAsync(new MarkaEkle());
    }

    private void MenuFlyoutItem_Clicked_5(object sender, EventArgs e)
    {
        Navigation.PushAsync(new ModelEkle());
    }

    private void MenuFlyoutItem_Clicked_6(object sender, EventArgs e)
    {
        Navigation.PushAsync(new KullaniciEkle());
    }

    private void MenuFlyoutItem_Clicked_7(object sender, EventArgs e)
    {
        Navigation.PushAsync(new MusteriEkle());
    }

    private void MenuFlyoutItem_Clicked_8(object sender, EventArgs e)
    {
        Navigation.PushAsync(new SatisYap());
    }

    private void MenuFlyoutItem_Clicked_9(object sender, EventArgs e)
    {
        Navigation.PushAsync(new Yardim());
    }

    private void MenuFlyoutItem_Clicked_10(object sender, EventArgs e)
    {
        Navigation.PushAsync(new LoginPage());
    }

    private void MenuFlyoutItem_Clicked_2(object sender, EventArgs e)
    {
        Navigation.PushAsync(new SearchPage());
    }

    private void MenuFlyoutItem_Clicked_11(object sender, EventArgs e)
    {
        Navigation.PushAsync(new UrunListelePage());
    }
    //private List<Button> createdButtons = new List<Button>();

    //private void Button_Clicked(object sender, EventArgs e)
    //{
    //    Button newButton = new Button
    //    {
    //        Text = "Yeni Dosya",
    //        BackgroundColor = Colors.LightGray,
    //        TextColor = Colors.Black
    //    };

    //    // Yeni düðmeyi içerik alanýna ekleyin
    //    ContentLayout.Children.Add(newButton);
    //    createdButtons.Add(newButton);

    //    newButton.Clicked += (s, args) =>
    //    {
    //        // Týklandýðýnda butonu kaldýr
    //        ContentLayout.Children.Remove(newButton);
    //        createdButtons.Remove(newButton);
    //    };
    //}
}
