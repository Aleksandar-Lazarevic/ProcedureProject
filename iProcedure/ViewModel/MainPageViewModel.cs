namespace iProcedure.ViewModel;

using CommunityToolkit.Maui.Views;
using iProcedure.Popup;
using iProcedure.View;
using System.Windows.Input;

public class MainPageViewModel : BindableObject
{
    public ICommand GoGeneralBOM { get; private set; }
    public ICommand GoAnnotation { get; private set; }
    public ICommand GoStepBOM { get; private set; }
    public ICommand SelectBackgroundImage { get; private set; }

    public MainPageViewModel()
	{
        GoGeneralBOM = new Command(OnGoGeneralBOM);
        GoAnnotation = new Command(OnGoAnnotation);
        GoStepBOM = new Command(OnGoStepBOM);
        SelectBackgroundImage = new Command(OnSelectBackgroundImage);
    }

    async private void OnGoGeneralBOM()
    {
        var generalBOMPage = new NavigationPage(new GeneralBOMPage());
        await App.Current.MainPage.Navigation.PushAsync(generalBOMPage);
    }

    async private void OnGoAnnotation()
    {
        var annotationsPage = new NavigationPage(new AnnotationsPage());
        await App.Current.MainPage.Navigation.PushAsync(annotationsPage);
    }

    async private void OnGoStepBOM()
    {
        var stepBOMPage = new NavigationPage(new StepBOMPage());
        await App.Current.MainPage.Navigation.PushAsync(stepBOMPage);
    }

    async private void OnSelectBackgroundImage()
    {
        var selectBackgroundImagePage = new NavigationPage(new BackgroundImagePage());
        await App.Current.MainPage.Navigation.PushAsync(selectBackgroundImagePage);
    }
}