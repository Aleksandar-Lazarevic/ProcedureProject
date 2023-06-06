using iProcedure.Model;
using iProcedure.ViewModel.Popup;
using System.Collections.ObjectModel;

namespace iProcedure.Popup;

public enum PopupType
{
    Add,
    Edit
}

public partial class BOMEditPopup : CommunityToolkit.Maui.Views.Popup
{
    private PopupType m_nPopupType = PopupType.Add;

    public BOMEditPopup(PopupType p_nPopupType)
    {
        InitializeComponent();
        m_nPopupType = p_nPopupType;
        InitUI();
    }

    public BOMEditPopup(PopupType p_nPopupType, StepBOMItem stepBOMItem, ObservableCollection<string> selBOMIndexes, ObservableCollection<StepBOMItem> selBOMItems)
	{
		InitializeComponent();
        m_nPopupType = p_nPopupType;

        bool w_bMulti = false;
        if (selBOMIndexes.Count > 1)
            w_bMulti = true;

        InitUI(w_bMulti);

        var vm = (BOMEditPopupViewModel)BindingContext;
        vm.m_objStepBOMItem = stepBOMItem;
        vm.selBOMIndexes = selBOMIndexes;
        vm.selBOMItems = selBOMItems;
    }

    private void InitUI(bool p_bMulti = false)
    {
        if(m_nPopupType == PopupType.Add)
        {
            TopInfomation0.IsVisible = false;
            TopInfomation1.IsVisible = false;
        }
        else
        {
            if (p_bMulti)
            {
                TopInfomation0.IsVisible = true;
            }
            else
            {
                TopInfomation0.IsVisible = false;

                CodeCheck.IsVisible = false;
                DescripttionCheck.IsVisible = false;
                QuantityCheck.IsVisible = false;
                UnitCheck.IsVisible = false;
                BulkMaterialCheck.IsVisible = false;
            }

            TopInfomation1.IsVisible = true;
            CodeEntry.IsEnabled = false;
        }

        var vm = (BOMEditPopupViewModel)BindingContext;
        vm.ClosePopupView += ClosePopupView;
    }

    private void Picker_SelectedIndexChanged(object sender, EventArgs e)
    {
        if((sender as Picker).SelectedIndex == 0)
            addNewItemEntry.Focus();
    }

    private void ClosePopupView()
    {
        this.Close();
    }
}