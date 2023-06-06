using CommunityToolkit.Mvvm.ComponentModel;
using iProcedure.Model;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace iProcedure.ViewModel;

public partial class StepBOMViewModel:ObservableObject
{
    private ObservableCollection<StepBOMItem> _stepBOMItems = new ObservableCollection<StepBOMItem>();
    public ObservableCollection<StepBOMItem> stepBOMItems
    {
        get => _stepBOMItems;
        set
        {
            SetProperty(ref _stepBOMItems, value);
        }
    }


    public ICommand OnEntryUnfocused { get; private set; }

    private string _entryText;

    [ObservableProperty]
    private bool _deleteButtonVisible = false;

    [ObservableProperty]
    private string _glyph = "\uf078";//x078

    [ObservableProperty]
    private Color _color = Colors.White;

    public Color color
    {
        get => _color;
        set
        {
            SetProperty(ref _color, value);
        }
    }

    public string glyph
    {
        get => _glyph;
        set
        {
            SetProperty(ref _glyph, value);
        }
    }

    public string entryText
    {
        get => _entryText;
        set
        {
            SetProperty(ref _entryText, value); //_entryText = value;

            if (value.ToString().Equals(""))
                color = Colors.White;
            else
                color = Colors.Black;
        }
    }

    public StepBOMViewModel()
    {
        StepBOMItem stepBOMItem = new StepBOMItem();
        stepBOMItem.strNumber = "1";
        stepBOMItem.strCode = "C1000-001";
        stepBOMItem.strDescription = "Seat base";
        stepBOMItem.strQuantityAvailable = "1 Each";
        stepBOMItem.strBulkMaterial = "";
        stepBOMItem.strQuantity = "1";
        stepBOMItems.Add(stepBOMItem);

        stepBOMItem = new StepBOMItem();
        stepBOMItem.strNumber = "2";
        stepBOMItem.strCode = "C1000-001 Long code";
        stepBOMItem.strDescription = "Seat base glue long\r\ndescription super long";
        stepBOMItem.strQuantityAvailable = "1 Mil";
        stepBOMItem.strBulkMaterial = "Bulk Material";
        stepBOMItem.strQuantity = "AR";
        stepBOMItems.Add(stepBOMItem);
    }

    private void Entry_Unfocused()
    {
    }
}