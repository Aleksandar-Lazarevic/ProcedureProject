using CommunityToolkit.Mvvm.ComponentModel;
using iProcedure.Model;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace iProcedure.ViewModel.Popup
{
    partial class BOMEditPopupViewModel : ObservableObject
    {
        private StepBOMItem _m_objStepBOMItem = new StepBOMItem();
        public StepBOMItem m_objStepBOMItem
        {
            get => _m_objStepBOMItem;
            set
            {
                strSortString = value.strSortString;
                strCode = value.strCode;
                strDescription = value.strDescription;

                for(int i = 0; i < Units.Count; i++)
                {
                    if (Units[i].unit_name.Equals(value.strQuantityAvailable))
                    {
                        selectedIndex = i;
                        break;
                    }
                }

                strQuantity = value.strQuantity;
                if (value.strBulkMaterial.Equals(""))
                    bToggled = false;
                else
                    bToggled = true;
            }
        }

        private string _arrow = "\uF0D7";//F0D8
        public string arrow
        {
            get => _arrow;
            set
            {
                SetProperty(ref _arrow, value);
            }
        }

        private int _selectedIndex = -1;
        public int selectedIndex
        {
            get => _selectedIndex;
            set
            {
                SetProperty(ref _selectedIndex, value);
            }
        }

        private bool _isVisiblePicker = true;
        public bool isVisiblePicker
        {
            get => _isVisiblePicker;
            set
            {
                SetProperty(ref _isVisiblePicker, value);
            }
        }

        private bool _isVisibleEntry = false;
        public bool isVisibleEntry
        {
            get => _isVisibleEntry;
            set
            {
                SetProperty(ref _isVisibleEntry, value);
            }
        }

        private string _strNewUnit = "";
        public string strNewUnit
        {
            get => _strNewUnit;
            set
            {
                SetProperty(ref _strNewUnit, value);
            }
        }



        private string _strSortString = "";
        public string strSortString
        {
            get => _strSortString;
            set
            {
                SetProperty(ref _strSortString, value);
            }
        }

        private string _strCode = "";
        public string strCode
        {
            get => _strCode;
            set
            {
                SetProperty(ref _strCode, value);
            }
        }

        private string _strDescription = "";
        public string strDescription
        {
            get => _strDescription;
            set
            {
                SetProperty(ref _strDescription, value);
            }
        }

        private string _strQuantity = "";
        public string strQuantity
        {
            get => _strQuantity;
            set
            {
                SetProperty(ref _strQuantity, value);
            }
        }

        private bool _bToggled = false;
        public bool bToggled
        {
            get => _bToggled;
            set
            {
                SetProperty(ref _bToggled, value);
            }
        }


        private bool _chkDescription = false;
        public bool chkDescription
        {
            get => _chkDescription;
            set
            {
                SetProperty(ref _chkDescription, value);
            }
        }

        private bool _chkQuantity = false;
        public bool chkQuantity
        {
            get => _chkQuantity;
            set
            {
                SetProperty(ref _chkQuantity, value);
            }
        }

        private bool _chkUnit = false;
        public bool chkUnit
        {
            get => _chkUnit;
            set
            {
                SetProperty(ref _chkUnit, value);
            }
        }

        private bool _chkBulkMaterial = false;
        public bool chkBulkMaterial
        {
            get => _chkBulkMaterial;
            set
            {
                SetProperty(ref _chkBulkMaterial, value);
            }
        }



        


        public ObservableCollection<Unit> Units { get; set; }


        private ObservableCollection<string> _selBOMIndexes = new ObservableCollection<string>();
        public ObservableCollection<string> selBOMIndexes
        {
            get => _selBOMIndexes;
            set
            {
                SetProperty(ref _selBOMIndexes, value);
            }
        }

        public ObservableCollection<StepBOMItem> selBOMItems;


        public ICommand ShowUpDownArrow { get; private set; }
        public ICommand SelectedIndexChanged { get; private set; }

        public ICommand DoFunc { get; private set; }
        public ICommand CancelFunc { get; private set; }

        public delegate void ClosePopupViewAction();
        public event ClosePopupViewAction ClosePopupView;

        public BOMEditPopupViewModel()
        {
            var list = App.Database.GetAllUnitDataAsync().Result;
            Units = new ObservableCollection<Unit>(list);

            ShowUpDownArrow = new Command(OnShowUpDownArrow);
            SelectedIndexChanged = new Command(OnSelectedIndexChanged);
            DoFunc = new Command(OnDoFunc);
            CancelFunc = new Command(OnCancelFunc);
        }

        private void OnShowUpDownArrow()
        {
            /*if (arrow.Equals("\uF0D8"))
                arrow = "\uF0D7";
            else
                arrow = "\uF0D8";*/
        }

        private void OnSelectedIndexChanged()
        {
            if(selectedIndex == 0)
            {
                isVisibleEntry = true;
                isVisiblePicker = false;
            }
        }

        private void OnDoFunc()
        {
            if(isVisibleEntry == true)
            {
                isVisibleEntry = false;
                isVisiblePicker = true;
                selectedIndex = -1;

                if(!strNewUnit.Equals(""))
                {
                    Unit unit = new Unit();
                    unit.unit_name = strNewUnit;
                    App.Database.SaveUnitDataAsync(unit);

                    Units.Add(unit);
                    selectedIndex = Units.Count - 1;
                }
                else
                    selectedIndex = -1;

                strNewUnit = "";
            }
            else
            {
                if (selectedIndex != -1 && !strSortString.Equals("") && !strCode.Equals("") && 
                    !strDescription.Equals("") && !strQuantity.Equals(""))
                {
                    int w_nItemsCount = App.Database.GetAllStepBOMItemDataAsync().Result.Count();
                    StepBOMItem stepBOMItem = new StepBOMItem();
                    stepBOMItem.isSelected = false;
                    stepBOMItem.strNumber = "0";
                    /*stepBOMItem.strSortString = strSortString;
                    stepBOMItem.strCode = strCode;
                    stepBOMItem.strDescription = strDescription;
                    stepBOMItem.strQuantityAvailable = Units[selectedIndex].unit_name;
                    stepBOMItem.strBulkMaterial = bToggled == true ? "Bulk Material" : "";
                    stepBOMItem.strQuantity = strQuantity;*/

                    if (selBOMIndexes.Count > 0)
                    {
                        if (selBOMIndexes.Count == 1)
                        {
                            stepBOMItem.strSortString = strSortString;
                            stepBOMItem.strCode = strCode;
                            stepBOMItem.strDescription = strDescription;
                            stepBOMItem.strQuantityAvailable = Units[selectedIndex].unit_name;
                            stepBOMItem.strBulkMaterial = bToggled == true ? "Bulk Material" : "";
                            stepBOMItem.strQuantity = strQuantity;

                            App.Database.UpdateStepBOMItemDataAsync(stepBOMItem).Wait();
                        }
                        else
                        {
                            for (int i = 0; i < selBOMItems.Count; i++)
                            {
                                stepBOMItem.strCode = selBOMItems[i].strCode;
                                stepBOMItem.strSortString = selBOMItems[i].strSortString;

                                if (chkDescription)
                                    stepBOMItem.strDescription = strDescription;
                                else
                                    stepBOMItem.strDescription = selBOMItems[i].strDescription;

                                if (chkQuantity)
                                    stepBOMItem.strQuantity = strQuantity;
                                else
                                    stepBOMItem.strQuantity = selBOMItems[i].strQuantity;

                                if (chkUnit)
                                    stepBOMItem.strQuantityAvailable = Units[selectedIndex].unit_name;
                                else
                                    stepBOMItem.strQuantityAvailable = selBOMItems[i].strQuantityAvailable;

                                if (chkBulkMaterial)
                                    stepBOMItem.strBulkMaterial = bToggled == true ? "Bulk Material" : "";
                                else
                                    stepBOMItem.strBulkMaterial = selBOMItems[i].strBulkMaterial;

                                App.Database.UpdateStepBOMItemDataAsync(stepBOMItem).Wait();
                            }
                        }
                    }
                    else
                        App.Database.SaveStepBOMItemDataAsync(stepBOMItem).Wait();
                }

                ClosePopupView?.Invoke();
            }
        }

        private void OnCancelFunc()
        {
            if (isVisibleEntry == true)
            {
                isVisibleEntry = false;
                isVisiblePicker = true;
                selectedIndex = -1;
            }
            else
            {
                ClosePopupView?.Invoke();
            }
        }

    }
}
