using CommunityToolkit.Maui.Views;
using iProcedure.Popup;
using Microsoft.Maui.Controls.Shapes;
using System.Reflection;

namespace iProcedure.View;

public partial class AnnotationsPage : ContentPage
{
    public static AnnotationsPage Instance;
    public AnnotationsPage()
	{
		InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);

        Init();
        Instance = this;
    }

    async private void Init()
    {
        var content = await Utils.GetAnnotations("Annotations/Ellipse.txt");

        Frame frame = new Frame();
        frame.WidthRequest = 70; frame.HeightRequest = 70;
        frame.BorderColor = Colors.Transparent; frame.BackgroundColor = Colors.Transparent;
        frame.Padding = new Thickness(0, 0, 0, 0);
        frame.CornerRadius = 0;

        Grid grid = new Grid();
        grid.RowDefinitions.Add(new RowDefinition(50));
        grid.RowDefinitions.Add(new RowDefinition(20));
        
        FlexLayout flexLayout = new FlexLayout();
        flexLayout.Direction = Microsoft.Maui.Layouts.FlexDirection.Column;
        
        Ellipse ellipse = MakeEllipse(Colors.Transparent, Colors.Red, 2, 50, 50);
        flexLayout.Add(ellipse);
        grid.Add(ellipse, 0 , 0);

        Label label = MakeLabel("ROUND", LayoutOptions.Center, LayoutOptions.End);
        grid.Add(label, 0, 1);

        frame.Content = grid;

        ShapesGroup.Add(frame);
    }

    private Label MakeLabel(string text, LayoutOptions horizontalOptions, LayoutOptions verticalOptions)
    {
        Label label = new Label();
        label.Text = text;
        label.HorizontalOptions = horizontalOptions; 
        label.VerticalOptions = verticalOptions;

        return label;
    }

    private Ellipse MakeEllipse(Color fillColor, Color strokeColor, double strokeThickness, double width, double height)
    {
        Ellipse ellipse = new Ellipse();
        ellipse.Fill = Colors.Transparent;
        ellipse.Stroke = Colors.Red;
        ellipse.StrokeThickness = 2;
        ellipse.WidthRequest = 50;
        ellipse.HeightRequest = 50;

        return ellipse;
    }

    async private void OnBack(object sender, EventArgs e)
    {
        await App.Current.MainPage.Navigation.PopAsync();
    }

    async private void OnMenu(object sender, EventArgs e)
    {
        var actionSheet = await DisplayActionSheet("Please Select", "Cancel", null, "DELETE", "RENAME", "IMPORT");
    }

    async private void OnAdd(object sender, EventArgs e)
    {
    }
}