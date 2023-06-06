using iProcedure.Crop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkiaSharp;
using System.Collections.ObjectModel;
using SkiaSharp.Views.Maui.Controls;
using SkiaSharp.Views.Maui;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;

namespace iProcedure.ViewModel
{
    partial class EditImageViewModel : ObservableObject
    {
        public SKCanvasView skiaView = null;
        public double SKCanvasViewWidth = 0;
        public double SKCanvasViewHeight = 0;

        private SKBitmap bitmap;
        private float? aspectRatio;
        private /*readonly*/ CroppingRectangle croppingRect;
        private SKMatrix inverseBitmapMatrix;
        private Dictionary<long, TouchPoint> touchPoints = new Dictionary<long, TouchPoint>();
        private Dictionary<long, SKPoint> touchPointsInside = new Dictionary<long, SKPoint>();
        private SKPoint bitmapLocationfirst = new SKPoint();
        private SKPoint bitmapLocationlast = new SKPoint();
        private const int corner = 30;

        public ICommand CropFunc { get; private set; }
        public ICommand TogglePopupLayoutCommand { get; private set; }
        public Action CallTogglePopupLayoutCommandEvent;


        private GridLength _bottomLayerHeight = 50;
        public GridLength bottomLayerHeight
        {
            get => _bottomLayerHeight;
            set
            {
                SetProperty(ref _bottomLayerHeight, value);
            }
        }

        public EditImageViewModel()
        {
            CropFunc = new Command(OnCropFunc);
            TogglePopupLayoutCommand = new Command(OnTogglePopupLayoutCommand);
        }

        public void Init()
        {
            string mainDir = "/storage/emulated/0/Pictures/02 start game.png";
            using (var fs = new FileStream(mainDir, FileMode.Open))
            {
                this.bitmap = SKBitmap.Decode(fs);
                croppingRect = new CroppingRectangle(new SKRect(0, 0, bitmap.Width, bitmap.Height), aspectRatio);
                SetAspectRatio(aspectRatio);
            }
        }

        internal struct TouchPoint
        {
            public int CornerIndex { set; get; }
            public SKPoint Offset { set; get; }
        }

        internal SKBitmap CroppedBitmap
        {
            get
            {
                SKRect cropRect = croppingRect.Rect;
                SKBitmap croppedBitmap = new SKBitmap((int)cropRect.Width, (int)cropRect.Height);
                SKRect dest = new SKRect(0, 0, cropRect.Width, cropRect.Height);
                SKRect source = new SKRect(cropRect.Left, cropRect.Top, cropRect.Right, cropRect.Bottom);
                using (SKCanvas canvas = new SKCanvas(croppedBitmap))
                {
                    canvas.DrawBitmap(bitmap, source, dest);
                }
                return croppedBitmap;
            }
        }


        private void Rotate()
        {
            var rotatedBitmap = new SKBitmap(bitmap.Height, bitmap.Width);

            using (var canvas = new SKCanvas(rotatedBitmap))
            {
                canvas.Translate(rotatedBitmap.Width, 0);
                canvas.RotateDegrees(90);
                canvas.DrawBitmap(bitmap, 0, 0);
            }

            bitmap.Dispose();
            bitmap = rotatedBitmap;

            SetAspectRatio(aspectRatio);
        }

        public void SetAspectRatio(float? aspectRatio = null, bool isFullRect = false)
        {
            this.aspectRatio = aspectRatio;
            croppingRect.SetRect(new SKRect(0, 0, bitmap.Width, bitmap.Height), aspectRatio, isFullRect);
            skiaView.InvalidateSurface();
        }

        internal void SetAspectRatio(CropItem crop)
        {
            switch (crop?.Action)
            {
                case CropRotateType.CropRotate:
                    Rotate();
                    break;
                case CropRotateType.CropFree:
                    SetAspectRatio(null, false);
                    break;
                case CropRotateType.CropFull:
                    SetAspectRatio(null, true);
                    break;
                case CropRotateType.CropSquare:
                    SetAspectRatio(1f);
                    break;
                case CropRotateType.Crop2_3:
                    SetAspectRatio(2f / 3f);
                    break;
                case CropRotateType.Crop3_2:
                    SetAspectRatio(3f / 2f);
                    break;
                case CropRotateType.Crop3_4:
                    SetAspectRatio(3f / 4f);
                    break;
                case CropRotateType.Crop4_3:
                    SetAspectRatio(4f / 3f);
                    break;
                case CropRotateType.Crop16_9:
                    SetAspectRatio(16f / 9f);
                    break;
                case CropRotateType.Crop9_16:
                    SetAspectRatio(9f / 16f);
                    break;
            }
        }

        private void OnCropFunc()
        {
            bitmap = CroppedBitmap;
            croppingRect = new CroppingRectangle(new SKRect(0, 0, bitmap.Width, bitmap.Height), aspectRatio);
            SetAspectRatio(aspectRatio);
            skiaView.InvalidateSurface();
        }

        internal void OnTogglePopupLayoutCommand()
        {
            CallTogglePopupLayoutCommandEvent.Invoke();
        }

        public void OnTouch(object sender, SKTouchEventArgs e)
{
            SKPoint pixelLocation = new SKPoint((float)(skiaView.CanvasSize.Width * e.Location.X / SKCanvasViewWidth), 
                (float)(skiaView.CanvasSize.Height * e.Location.Y / SKCanvasViewHeight));
            SKPoint bitmapLocation = inverseBitmapMatrix.MapPoint(pixelLocation);

            switch (e.ActionType)
            {
                case SKTouchAction.Pressed:
                    // Convert radius to bitmap/cropping scale
                    float radius = inverseBitmapMatrix.ScaleX * 50;

                    // Find corner that the finger is touching
                    int cornerIndex = croppingRect.HitTest(bitmapLocation, radius);

                    if (cornerIndex != -1 && !touchPoints.ContainsKey(e.Id))
                    {
                        TouchPoint touchPoint = new TouchPoint
                        {
                            CornerIndex = cornerIndex,
                            Offset = bitmapLocation - croppingRect.Corners[cornerIndex]
                        };

                        touchPoints.Add(e.Id, touchPoint);
                    }
                    else
                    {
                        if (croppingRect.TestPointInsideSquare(bitmapLocation) && !touchPointsInside.ContainsKey(e.Id))
                        {
                            touchPointsInside.Add(e.Id, bitmapLocation);
                            bitmapLocationfirst = bitmapLocation;
                        }
                    }
                    break;

                case SKTouchAction.Moved:
                    if (touchPoints.ContainsKey(e.Id))
                    {
                        TouchPoint touchPoint = touchPoints[e.Id];
                        croppingRect.MoveCorner(touchPoint.CornerIndex, bitmapLocation - touchPoint.Offset);
                        skiaView.InvalidateSurface();
                    }
                    if (touchPointsInside.ContainsKey(e.Id))
                    {
                        bitmapLocationlast = bitmapLocation;
                        SKPoint point = new SKPoint(bitmapLocationlast.X - bitmapLocationfirst.X, bitmapLocationlast.Y - bitmapLocationfirst.Y);
                        croppingRect.MoveAllCorner(point);
                        bitmapLocationfirst = bitmapLocationlast;
                        skiaView.InvalidateSurface();
                    }
                    break;

                case SKTouchAction.Released:
                case SKTouchAction.Cancelled:
                    if (touchPoints.ContainsKey(e.Id))
                        touchPoints.Remove(e.Id);

                    else if (touchPointsInside.ContainsKey(e.Id))
                        touchPointsInside.Remove(e.Id);
                    break;
            }

            e.Handled = true;
        }

        public void OnPaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            SKImageInfo info = e.Info;
            SKSurface surface = e.Surface;
            SKCanvas canvas = surface.Canvas;

            canvas.Clear(SkiaHelper.backgroundColor);
            
            // Calculate rectangle for displaying bitmap 
            var rect = SkiaHelper.CalculateRectangle(new SKRect(0, 0, info.Width, info.Height), bitmap);
            canvas.DrawBitmap(bitmap, rect.rect);

            // Calculate a matrix transform for displaying the cropping rectangle
            SKMatrix bitmapScaleMatrix = SKMatrix.MakeIdentity();
            bitmapScaleMatrix.SetScaleTranslate(rect.scaleX, rect.scaleY, rect.rect.Left, rect.rect.Top);

            // Display rectangle
            SKRect scaledCropRect = bitmapScaleMatrix.MapRect(croppingRect.Rect);


            using (SKPaint edgeStroke = new SKPaint())
            {
                edgeStroke.Style = SKPaintStyle.Stroke;
                edgeStroke.Color = SKColors.White;
                edgeStroke.StrokeWidth = 3;
                edgeStroke.IsAntialias = true;
                canvas.DrawRect(scaledCropRect, edgeStroke);
            }

            canvas.DrawSurrounding(rect.rect, scaledCropRect, SKColors.Gray.WithAlpha(190));

            // Display heavier corners
            using (SKPaint cornerStroke = new SKPaint())
            using (SKPath path = new SKPath())
            {
                cornerStroke.Style = SKPaintStyle.Stroke;
                cornerStroke.Color = SKColors.White;
                cornerStroke.StrokeWidth = 7;

                path.MoveTo(scaledCropRect.Left, scaledCropRect.Top + corner);
                path.LineTo(scaledCropRect.Left, scaledCropRect.Top);
                path.LineTo(scaledCropRect.Left + corner, scaledCropRect.Top);

                path.MoveTo(scaledCropRect.Right - corner, scaledCropRect.Top);
                path.LineTo(scaledCropRect.Right, scaledCropRect.Top);
                path.LineTo(scaledCropRect.Right, scaledCropRect.Top + corner);

                path.MoveTo(scaledCropRect.Right, scaledCropRect.Bottom - corner);
                path.LineTo(scaledCropRect.Right, scaledCropRect.Bottom);
                path.LineTo(scaledCropRect.Right - corner, scaledCropRect.Bottom);

                path.MoveTo(scaledCropRect.Left + corner, scaledCropRect.Bottom);
                path.LineTo(scaledCropRect.Left, scaledCropRect.Bottom);
                path.LineTo(scaledCropRect.Left, scaledCropRect.Bottom - corner);

                canvas.DrawPath(path, cornerStroke);
            }

            // Invert the transform for touch tracking
            bitmapScaleMatrix.TryInvert(out inverseBitmapMatrix);
        }
    }
}
