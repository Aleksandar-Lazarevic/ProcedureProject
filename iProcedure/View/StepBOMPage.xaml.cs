namespace iProcedure.View;

public partial class StepBOMPage : ContentPage
{
	public StepBOMPage()
	{
		InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
    }

    private void OnBack(object sender, EventArgs e)
    {
        App.Current.MainPage.Navigation.PopAsync();
    }
}