using System.ComponentModel;
using System.Windows.Input;

namespace iProcedure.Controls;

public partial class SearchViewUI : ContentView
{
    public static readonly BindableProperty deleteButtonWidthProperty = BindableProperty.Create(nameof(deleteButtonWidth), typeof(int), typeof(SearchViewUI), 25);
    public static readonly BindableProperty searchButtonWidthProperty = BindableProperty.Create(nameof(searchButtonWidth), typeof(int), typeof(SearchViewUI), 25);

    public static readonly BindableProperty deleteButtonVisibleProperty = BindableProperty.Create(nameof(deleteButtonVisible), typeof(bool), typeof(SearchViewUI), false, coerceValue: CoerceAngle);

    public static readonly BindableProperty entryTextProperty = BindableProperty.Create(nameof(entryText), typeof(string), typeof(SearchViewUI), string.Empty,
        defaultBindingMode: BindingMode.TwoWay, propertyChanged: OnEntryTextPropertyChanged);


    public int deleteButtonWidth
    {
        get => (int)GetValue(deleteButtonWidthProperty);
        set => SetValue(deleteButtonWidthProperty, value);
    }

    public int searchButtonWidth
    {
        get => (int)GetValue(searchButtonWidthProperty);
        set => SetValue(searchButtonWidthProperty, value);
    }

    public bool deleteButtonVisible
    {
        get => (bool)GetValue(deleteButtonVisibleProperty);
        set => SetValue(deleteButtonVisibleProperty, value);
    }

    public string entryText
    {
        get => (string)GetValue(entryTextProperty);
        set => SetValue(entryTextProperty, value);
    }
    static bool ddd = false;
    private static void OnEntryTextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        string w_strEntryText = newValue as string;

        if (w_strEntryText.Equals(""))
        {
//            bindable.SetValue(deleteButtonVisibleProperty, false); //bindable.SetValue(deleteButtonVisibleProperty, false);
            ddd = false;
            bindable.CoerceValue(deleteButtonVisibleProperty);
        }
        else
        {
//            bindable.SetValue(deleteButtonVisibleProperty, true);
            ddd = true;
            bindable.CoerceValue(deleteButtonVisibleProperty);
        }
    }

    static object CoerceAngle(BindableObject bindable, object value)
    {
        //value = ddd;
        //bindable.SetValue(deleteButtonVisibleProperty, value);
        return true;
    }


    public SearchViewUI()
	{
		InitializeComponent();
        this.BindingContext = this;
	}
}