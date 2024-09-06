using MauiApp1.Pages;

namespace MauiApp1;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		//MainPage = new AppShell(); //AppShell()

		MainPage = new NavigationPage(new LoginPage());

	}
    
}
