
using Microsoft.Maui;
using System.Windows.Input;

namespace iProcedure.Controls;

public partial class EllipseWithIconViewUI : ContentView
{
    public static readonly BindableProperty borderColorProperty = BindableProperty.Create(nameof(borderColor), typeof(Color), typeof(EllipseWithIconViewUI), Colors.Pink);
    public static readonly BindableProperty tmpGlyphProperty = BindableProperty.Create(nameof(tmpGlyph), typeof(string), typeof(EllipseWithIconViewUI), string.Empty);

    public static readonly BindableProperty imageWidthProperty = BindableProperty.Create(nameof(imageWidth), typeof(int), typeof(EllipseWithIconViewUI), 50);
    public static readonly BindableProperty imageHeightProperty = BindableProperty.Create(nameof(imageHeight), typeof(int), typeof(EllipseWithIconViewUI), 50);

    public static readonly BindableProperty ellipseWidthProperty = BindableProperty.Create(nameof(ellipseWidth), typeof(int), typeof(EllipseWithIconViewUI), 20);
    public static readonly BindableProperty ellipseHeightProperty = BindableProperty.Create(nameof(ellipseHeight), typeof(int), typeof(EllipseWithIconViewUI), 20);

    public static readonly BindableProperty fontFamilyProperty = BindableProperty.Create(nameof(fontFamily), typeof(string), typeof(EllipseWithIconViewUI), string.Empty);
    public static readonly BindableProperty fontSizeProperty = BindableProperty.Create(nameof(fontSize), typeof(int), typeof(EllipseWithIconViewUI), 15);

    public static readonly BindableProperty frameCornerRadiusProperty = BindableProperty.Create(nameof(frameCornerRadius), typeof(int), typeof(EllipseWithIconViewUI), 15);
    public static readonly BindableProperty imageCornerRadiusProperty = BindableProperty.Create(nameof(imageCornerRadius), typeof(int), typeof(EllipseWithIconViewUI), 15);


    public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(EllipseWithIconViewUI), null);


    public ICommand Command
    {
        get => (ICommand)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }

    public Color borderColor
    {
        get => (Color)GetValue(borderColorProperty);
        set => SetValue(borderColorProperty, value);
    }

    public string tmpGlyph
    {
        get => (string)GetValue(tmpGlyphProperty);
        set => SetValue(tmpGlyphProperty, value);
    }

    public int imageWidth
    {
        get => (int)GetValue(imageWidthProperty);
        set => SetValue(imageWidthProperty, value);
    }

    public int imageHeight
    {
        get => (int)GetValue(imageHeightProperty);
        set => SetValue(imageHeightProperty, value);
    }

    public int ellipseWidth
    {
        get => (int)GetValue(ellipseWidthProperty);
        set => SetValue(ellipseWidthProperty, value);
    }

    public int ellipseHeight
    {
        get => (int)GetValue(ellipseHeightProperty);
        set => SetValue(ellipseHeightProperty, value); 
    }


    public string fontFamily
    {
        get => (string)GetValue(fontFamilyProperty);
        set => SetValue(fontFamilyProperty, value);
    }

    public int fontSize
    {
        get => (int)GetValue(fontSizeProperty);
        set => SetValue(fontSizeProperty, value);
    }

    public int frameCornerRadius
    {
        get => (int)GetValue(frameCornerRadiusProperty);
        set => SetValue(frameCornerRadiusProperty, value);
    }

    public int imageCornerRadius
    {
        get => (int)GetValue(imageCornerRadiusProperty);
        set => SetValue(imageCornerRadiusProperty, value);
    }

    public EllipseWithIconViewUI()
	{
		InitializeComponent();
	}
}