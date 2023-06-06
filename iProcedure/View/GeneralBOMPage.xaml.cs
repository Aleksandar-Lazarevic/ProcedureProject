namespace iProcedure.View;

public partial class GeneralBOMPage : ContentPage
{
    public static GeneralBOMPage Instance = null;

    public GeneralBOMPage()
	{
		InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);

        Instance = this;
    }

    private void OnBack(object sender, EventArgs e)
    {
        App.Current.MainPage.Navigation.PopAsync();
    }
}