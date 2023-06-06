using CommunityToolkit.Mvvm.ComponentModel;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iProcedure.Model
{
    public class StepBOMItem: ObservableObject
    {
        private bool _isSelected = false;
        public bool isSelected
        {
            get => _isSelected;
            set
            {
                SetProperty(ref _isSelected, value);
            }
        }

        private string _strNumber = "";
        public string strNumber 
        {
            get => _strNumber;
            set
            {
                SetProperty(ref _strNumber, value);
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
        [PrimaryKey]
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

        private string _strQuantityAvailable = "";
        public string strQuantityAvailable
        {
            get => _strQuantityAvailable;
            set
            {
                SetProperty(ref _strQuantityAvailable, value);
            }
        }

        private string _strstrBulkMaterial = "";
        public string strBulkMaterial
        {
            get => _strstrBulkMaterial;
            set
            {
                SetProperty(ref _strstrBulkMaterial, value);
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
    }
}
