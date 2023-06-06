using iProcedure.ViewModel;
using System.Windows.Input;

namespace iProcedure;

public partial class MainPage : ContentPage
{
    public MainPage()
	{
        InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
    }
}

