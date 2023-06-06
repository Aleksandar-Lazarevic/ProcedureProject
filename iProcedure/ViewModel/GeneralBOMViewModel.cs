using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using iProcedure.Model;
using iProcedure.Popup;
using iProcedure.View;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace iProcedure.ViewModel
{
    partial class GeneralBOMViewModel : ObservableObject
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

        private StepBOMItem _selectedItem = null;
        public StepBOMItem selectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
            }
        }

        private bool _isChecked = false;

        public bool isChecked
        {
            get => _isChecked;
            set
            {
                SetProperty(ref _isChecked, value);
            }
        }

        public ICommand CheckedChanged { get; private set; }
        
        public ICommand AddFunc { get; private set; }
        public ICommand EditFunc { get; private set; }
        public ICommand DeleteFunc { get; private set; }

        public GeneralBOMViewModel()
        {
            CheckedChanged = new Command(OnCheckedChanged);

            AddFunc = new Command(OnAddFunc);
            EditFunc = new Command(OnEditFunc);
            DeleteFunc = new Command(OnDeleteFunc);

            RefreshStepBOMItems();
        }

        private void OnCheckedChanged()
        {
            if (isChecked)
            {
                foreach (var item in stepBOMItems)
                {
                    item.isSelected = true;
                }
            }
            else
            {
                foreach (var item in stepBOMItems)
                {
                    item.isSelected = false;
                }
            }
        }

        async private void OnAddFunc()
        {
            var popup = new BOMEditPopup(PopupType.Add);
            await PopupExtensions.ShowPopupAsync<BOMEditPopup>(GeneralBOMPage.Instance, popup);

            RefreshStepBOMItems();
        }

        async private void OnEditFunc()
        {
            ObservableCollection<string> selBOMIndexes = new ObservableCollection<string>();
            ObservableCollection<StepBOMItem> selBOMItems = new ObservableCollection<StepBOMItem>();
            for (int i = 0; i < stepBOMItems.Count; i++)
            {
                if (stepBOMItems[i].isSelected)
                {
                    selBOMIndexes.Add((i + 1).ToString());
                    selBOMItems.Add(stepBOMItems[i]);
                }
            }

            if (selBOMIndexes.Count == 0)
                return;

            StepBOMItem item = stepBOMItems[Convert.ToInt32(selBOMIndexes[0]) - 1];
            var popup = new BOMEditPopup(PopupType.Edit, item, selBOMIndexes, selBOMItems);
            await PopupExtensions.ShowPopupAsync<BOMEditPopup>(GeneralBOMPage.Instance, popup);

            RefreshStepBOMItems(true);
        }

        private void OnDeleteFunc()
        {
            for (int i = stepBOMItems.Count - 1; i >= 0; i--)
            {
                if (stepBOMItems[i].isSelected)
                {
                    App.Database.RemoveStepBOMItemDataAsync(stepBOMItems[i]);
                    stepBOMItems.RemoveAt(i);
                }
            }

            //stepBOMItems = new ObservableCollection<StepBOMItem>(stepBOMItems.ToList());
            RefreshStepBOMItems();
        }

        private void RefreshStepBOMItems(bool p_bEditMode = false)
        {
            ObservableCollection<string> selBOMCodes = new ObservableCollection<string>();
            if (p_bEditMode)
            {
                for (int i = 0; i < stepBOMItems.Count; i++)
                {
                    if (stepBOMItems[i].isSelected)
                        selBOMCodes.Add(stepBOMItems[i].strCode);
                }
            }

            var list = App.Database.GetAllStepBOMItemDataAsync().Result;
            stepBOMItems = new ObservableCollection<StepBOMItem>(list);

            if(p_bEditMode)
            {
                for(int i = 0; i < selBOMCodes.Count(); i++)
                {
                    for(int j = 0; j < stepBOMItems.Count; j++)
                    {
                        if (selBOMCodes[i].Equals(stepBOMItems[j].strCode))
                        {
                            stepBOMItems[j].isSelected = true;
                            break;
                        }
                    }
                }
            }

            stepBOMItems = new ObservableCollection<StepBOMItem>(from i in stepBOMItems orderby i.strSortString select i);

            for (int i = 0; i < stepBOMItems.Count; i++)
                stepBOMItems[i].strNumber = String.Format("{0}", i + 1);
        }
    }
}
