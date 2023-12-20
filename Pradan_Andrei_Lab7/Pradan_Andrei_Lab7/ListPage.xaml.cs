using Pradan_Andrei_Lab7.Models;

namespace Pradan_Andrei_Lab7;

public partial class ListPage : ContentPage
{
	public ListPage()
	{
		InitializeComponent();
	}
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        var shopl = (ShopList)BindingContext;
        listView.ItemsSource = await App.Database.GetListProductsAsync(shopl.ID);
    }
    async void OnChooseButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ProductPage((ShopList)this.BindingContext)
        {
            BindingContext = new Product()
        });
    }
    async void OnDeleteItemButtonClicked(object sender, EventArgs e)
    {
        Product product;
        if (listView.SelectedItem != null)
        {
            product = listView.SelectedItem as Product;
            await App.Database.DeleteProductAsync(product);
           listView.ItemsSource = await App.Database.GetProductsAsync();
        }
    }
    async void OnSaveButtonClicked(object sender, EventArgs e)
    {
        var slist = (ShopList)BindingContext;
        slist.Date = DateTime.UtcNow;
        await App.Database.SaveShopListAsync(slist);
        await Navigation.PopAsync();
    }
    async void OnDeleteButtonClicked(object sender, EventArgs e)
    {
        var slist = (ShopList)BindingContext;
        await App.Database.DeleteShopListAsync(slist);
        await Navigation.PopAsync();
    }
}
