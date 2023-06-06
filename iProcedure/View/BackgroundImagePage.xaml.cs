using CommunityToolkit.Maui.Views;
using iProcedure.Popup;

namespace iProcedure.View;

public partial class BackgroundImagePage : ContentPage
{
    public BackgroundImagePage()
	{
		InitializeComponent(); 
		NavigationPage.SetHasNavigationBar(this, false);
    }

    async private void OnBack(object sender, EventArgs e)
	{
        App.Current.MainPage.Navigation.PopAsync();
    }
}