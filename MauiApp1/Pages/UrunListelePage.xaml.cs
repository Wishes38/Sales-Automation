using MauiApp1.Services;
using System.Collections.Generic;

namespace MauiApp1.Pages;

public partial class UrunListelePage : ContentPage
{
	IServices service;
	public UrunListelePage()
	{
		InitializeComponent();
		service = new Service();
        UrunGetir();
	}
	
	private async void UrunGetir()
	{
        List<Guid> markaIdList = new List<Guid>();
        List<String> markaNameList = new List<String>();
        List<Guid> kategoriIdList = new List<Guid>();
        List<String> kategoriNameList = new List<String>();
        List<Guid> modelIdList = new List<Guid>();
        List<String> modelNameList = new List<String>();
        var gelenUrunList = await service.GetUrun();
        String gelenKategoriName;
        String gelenModelName;
        String gelenMarkaName;

        foreach (var item in gelenUrunList)
		{
			markaIdList.Add(item.MarkaId);			
		}
        for (int i = 0; i < markaIdList.Count; i++)
        {
            gelenMarkaName = await service.GetMarkaName(markaIdList[i]);
            markaNameList.Add(gelenMarkaName);
        }




        foreach (var item in gelenUrunList)
        {
            modelIdList.Add(item.ModelId);
        }
        for (int i = 0; i < modelIdList.Count; i++)
        {
            gelenModelName = await service.GetModelName(modelIdList[i]);
            modelNameList.Add(gelenModelName);
        }
        foreach (var item in gelenUrunList)
        {
            kategoriIdList.Add(item.KategoriId);
        }
        for (int i = 0; i < kategoriIdList.Count; i++)
        {
            gelenKategoriName = await service.GetKategoriName(kategoriIdList[i]);
            kategoriNameList.Add(gelenKategoriName);
        }
        CVArama.ItemsSource = kategoriNameList;
        CVArama.ItemsSource = markaNameList;
        CVArama.ItemsSource = modelNameList;
    }
}