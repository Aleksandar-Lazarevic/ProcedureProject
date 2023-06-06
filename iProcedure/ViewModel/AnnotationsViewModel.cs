using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using iProcedure.Popup;
using iProcedure.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace iProcedure.ViewModel
{
    partial class AnnotationsViewModel : ObservableObject
    {
        private int selIdx = 0;

        public ICommand InitCommand { get; private set; }

        public ICommand ShapesFunc { get; private set; }
        public ICommand SignsFunc { get; private set; }
        public ICommand CalloutsFunc { get; private set; }

        private Color _underLineColor = Colors.White;

        public Color underLineColor
        {
            get => _underLineColor;
            set
            {
                SetProperty(ref _underLineColor, value);
            }
        }

        private bool _showShapesUnderLine = true;

        public bool showShapesUnderLine
        {
            get => _showShapesUnderLine;
            set
            {
                SetProperty(ref _showShapesUnderLine, value);
            }
        }

        private bool _showSignsUnderLine = true;

        public bool showSignsUnderLine
        {
            get => _showSignsUnderLine;
            set
            {
                SetProperty(ref _showSignsUnderLine, value);
            }
        }

        private bool _showCalloutsLine = true;

        public bool showCalloutsLine
        {
            get => _showCalloutsLine;
            set
            {
                SetProperty(ref _showCalloutsLine, value);
            }
        }

        public AnnotationsViewModel()
        {
            InitCommand = new Command(OnInit);

            ShapesFunc = new Command(OnShapesFunc);
            SignsFunc = new Command(OnSignsFunc);
            CalloutsFunc = new Command(OnCalloutsFunc);
        }

        private void OnShapesFunc()
        {
            showShapesUnderLine = true;
            showSignsUnderLine = false;
            showCalloutsLine = false;

            selIdx = 0;
            underLineColor = Colors.Black;
        }
        private void OnSignsFunc()
        {
            showShapesUnderLine = false;
            showSignsUnderLine = true;
            showCalloutsLine = false;

            selIdx = 1;
            underLineColor = Colors.Black;
        }
        private void OnCalloutsFunc()
        {
            showShapesUnderLine = false;
            showSignsUnderLine = false;
            showCalloutsLine = true;

            selIdx = 2;
            underLineColor = Colors.Black;
        }

        private void OnInit()
        {
        }
    }
}
