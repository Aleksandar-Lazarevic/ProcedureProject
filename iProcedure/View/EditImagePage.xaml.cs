using iProcedure.Crop;
using iProcedure.ViewModel;
using SkiaSharp;
using SkiaSharp.Views.Maui;
using SkiaSharp.Views.Maui.Controls;
using System.Windows.Input;

namespace iProcedure.View;

public partial class EditImagePage : ContentPage
{
    private bool m_bFirst = true;

    public EditImagePage()
    {
        InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
        
        var vm = (EditImageViewModel)BindingContext;

        vm.skiaView = skiaView;
        vm.Init();
        //vm.CallTogglePopupLayoutCommandEvent += OnTogglePopupLayoutCommand;
    }

    internal void OnTogglePopupLayoutCommand(Grid popupLayout)
    {
        if (!popupLayout.IsVisible)
        {
            popupLayout.IsVisible = true;
            popupLayout.MaximumHeightRequest = Height > 0 ? Height : 10000;
            var bounds = popupLayout.Measure(Width, Height);

            popupLayout.Opacity = 0;
            popupLayout.MaximumHeightRequest = 0;

            var animation = new Animation();
            animation.Add(0, 1, new Animation(v => popupLayout.Opacity = v, 0, 1));
            animation.Add(0, 1, new Animation(v => popupLayout.MaximumHeightRequest = v, 0, bounds.Request.Height));

            animation.Commit(popupLayout, nameof(popupLayout), length: 500, easing: Easing.CubicOut);
        }
        else
        {
            var animation = new Animation();
            animation.Add(0, 1, new Animation(v => popupLayout.Opacity = v, 1, 0));
            animation.Add(0, 1, new Animation(v => popupLayout.MaximumHeightRequest = v, popupLayout.Height, 0));

            animation.Commit(popupLayout, nameof(popupLayout), length: 500, easing: Easing.CubicOut,
                finished: (v, f) => popupLayout.IsVisible = false);
        }
    }

    private void OnTouch(object sender, SKTouchEventArgs e)
    {
        var vm = (EditImageViewModel)BindingContext;
        vm.SKCanvasViewWidth = this.Width;
        vm.SKCanvasViewHeight = this.Height - 140;
        vm.OnTouch(sender, e);
    }

    private void OnPaintSurface(object sender, SKPaintSurfaceEventArgs e)
    {
        var vm = (EditImageViewModel)BindingContext;
        vm.OnPaintSurface(sender, e);

        if (m_bFirst)
        {
            m_bFirst = false;
            OnTogglePopupLayoutCommand(PopupResizeLayout);
            OnTogglePopupLayoutCommand(PopupRotateLayout);
            OnTogglePopupLayoutCommand(PopupFlipLayout);
            OnTogglePopupLayoutCommand(PopupBrigthnessLayout);
            OnTogglePopupLayoutCommand(PopupContrastLayout);
            OnTogglePopupLayoutCommand(PopupSharpenLayout);
        }
    }

    private void ContentPage_Loaded(object sender, EventArgs e)
    {
        
    }

    private void OnCrop(object sender, EventArgs e)
    {
        if (!PopupCropLayout.IsVisible)
            OnTogglePopupLayoutCommand(PopupCropLayout);

        if (PopupResizeLayout.IsVisible)
            OnTogglePopupLayoutCommand(PopupResizeLayout);

        if (PopupRotateLayout.IsVisible)
            OnTogglePopupLayoutCommand(PopupRotateLayout);

        if(PopupFlipLayout.IsVisible)
            OnTogglePopupLayoutCommand(PopupFlipLayout);

        if (PopupBrigthnessLayout.IsVisible)
            OnTogglePopupLayoutCommand(PopupBrigthnessLayout);

        if (PopupContrastLayout.IsVisible)
            OnTogglePopupLayoutCommand(PopupContrastLayout);

        if (PopupSharpenLayout.IsVisible)
            OnTogglePopupLayoutCommand(PopupSharpenLayout);
    }

    private void OnResize(object sender, EventArgs e)
    {
        if (PopupCropLayout.IsVisible)
            OnTogglePopupLayoutCommand(PopupCropLayout);

        if (!PopupResizeLayout.IsVisible)
            OnTogglePopupLayoutCommand(PopupResizeLayout);

        if (PopupRotateLayout.IsVisible)
            OnTogglePopupLayoutCommand(PopupRotateLayout);

        if (PopupFlipLayout.IsVisible)
            OnTogglePopupLayoutCommand(PopupFlipLayout);

        if (PopupBrigthnessLayout.IsVisible)
            OnTogglePopupLayoutCommand(PopupBrigthnessLayout);

        if (PopupContrastLayout.IsVisible)
            OnTogglePopupLayoutCommand(PopupContrastLayout);

        if (PopupSharpenLayout.IsVisible)
            OnTogglePopupLayoutCommand(PopupSharpenLayout);
    }

    private void OnRotate(object sender, EventArgs e)
    {
        if (PopupCropLayout.IsVisible)
            OnTogglePopupLayoutCommand(PopupCropLayout);

        if (PopupResizeLayout.IsVisible)
            OnTogglePopupLayoutCommand(PopupResizeLayout);

        if (!PopupRotateLayout.IsVisible)
            OnTogglePopupLayoutCommand(PopupRotateLayout);

        if (PopupFlipLayout.IsVisible)
            OnTogglePopupLayoutCommand(PopupFlipLayout);

        if (PopupBrigthnessLayout.IsVisible)
            OnTogglePopupLayoutCommand(PopupBrigthnessLayout);

        if (PopupContrastLayout.IsVisible)
            OnTogglePopupLayoutCommand(PopupContrastLayout);

        if (PopupSharpenLayout.IsVisible)
            OnTogglePopupLayoutCommand(PopupSharpenLayout);
    }

    private void OnFlip(object sender, EventArgs e)
    {
        if (PopupCropLayout.IsVisible)
            OnTogglePopupLayoutCommand(PopupCropLayout);

        if (PopupResizeLayout.IsVisible)
            OnTogglePopupLayoutCommand(PopupResizeLayout);

        if (PopupRotateLayout.IsVisible)
            OnTogglePopupLayoutCommand(PopupRotateLayout);

        if (!PopupFlipLayout.IsVisible)
            OnTogglePopupLayoutCommand(PopupFlipLayout);

        if (PopupBrigthnessLayout.IsVisible)
            OnTogglePopupLayoutCommand(PopupBrigthnessLayout);

        if (PopupContrastLayout.IsVisible)
            OnTogglePopupLayoutCommand(PopupContrastLayout);

        if (PopupSharpenLayout.IsVisible)
            OnTogglePopupLayoutCommand(PopupSharpenLayout);
    }

    private void OnBrightness(object sender, EventArgs e)
    {
        if (PopupCropLayout.IsVisible)
            OnTogglePopupLayoutCommand(PopupCropLayout);

        if (PopupResizeLayout.IsVisible)
            OnTogglePopupLayoutCommand(PopupResizeLayout);

        if (PopupRotateLayout.IsVisible)
            OnTogglePopupLayoutCommand(PopupRotateLayout);

        if (PopupFlipLayout.IsVisible)
            OnTogglePopupLayoutCommand(PopupFlipLayout);

        if (!PopupBrigthnessLayout.IsVisible)
            OnTogglePopupLayoutCommand(PopupBrigthnessLayout);

        if (PopupContrastLayout.IsVisible)
            OnTogglePopupLayoutCommand(PopupContrastLayout);

        if (PopupSharpenLayout.IsVisible)
            OnTogglePopupLayoutCommand(PopupSharpenLayout);
    }
    private void OnContrast(object sender, EventArgs e)
    {
        if (PopupCropLayout.IsVisible)
            OnTogglePopupLayoutCommand(PopupCropLayout);

        if (PopupResizeLayout.IsVisible)
            OnTogglePopupLayoutCommand(PopupResizeLayout);

        if (PopupRotateLayout.IsVisible)
            OnTogglePopupLayoutCommand(PopupRotateLayout);

        if (PopupFlipLayout.IsVisible)
            OnTogglePopupLayoutCommand(PopupFlipLayout);

        if (PopupBrigthnessLayout.IsVisible)
            OnTogglePopupLayoutCommand(PopupBrigthnessLayout);

        if (!PopupContrastLayout.IsVisible)
            OnTogglePopupLayoutCommand(PopupContrastLayout);

        if (PopupSharpenLayout.IsVisible)
            OnTogglePopupLayoutCommand(PopupSharpenLayout);
    }

    private void OnSharpen(object sender, EventArgs e)
    {
        if (PopupCropLayout.IsVisible)
            OnTogglePopupLayoutCommand(PopupCropLayout);

        if (PopupResizeLayout.IsVisible)
            OnTogglePopupLayoutCommand(PopupResizeLayout);

        if (PopupRotateLayout.IsVisible)
            OnTogglePopupLayoutCommand(PopupRotateLayout);

        if (PopupFlipLayout.IsVisible)
            OnTogglePopupLayoutCommand(PopupFlipLayout);

        if (PopupBrigthnessLayout.IsVisible)
            OnTogglePopupLayoutCommand(PopupBrigthnessLayout);

        if (PopupContrastLayout.IsVisible)
            OnTogglePopupLayoutCommand(PopupContrastLayout);

        if (!PopupSharpenLayout.IsVisible)
            OnTogglePopupLayoutCommand(PopupSharpenLayout);
    }

    private void OnUndo(object sender, EventArgs e)
    {
    }

    private void OnRedo(object sender, EventArgs e)
    {
    }
}