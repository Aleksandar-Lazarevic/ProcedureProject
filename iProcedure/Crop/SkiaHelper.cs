using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkiaSharp;

namespace iProcedure.Crop
{

    internal static class SkiaHelper
    {
        internal const uint backgroundColor = 0xC9020204;
        internal const double trashSize = 50;
        internal const double trashBigSize = 75;
        internal static Thickness trashMargin = new Thickness(0, 0, 0, 30);
        internal static Color backgroundColorHex = Color.FromUint(backgroundColor);

        internal static (SKRect rect, float scaleX, float scaleY) CalculateRectangle(SKRect info, SKBitmap bitmap, BBAspect aspect = BBAspect.AspectFit) => CalculateRectangle(info, bitmap.Width, bitmap.Height, aspect);

        internal static (SKRect rect, float scaleX, float scaleY) CalculateRectangle(SKRect info, float width, float height, BBAspect aspect = BBAspect.AspectFit)
        {
            BBAspect _aspect;
            if (aspect == BBAspect.Auto)
            {
                float aspectInfo = Math.Abs(info.Width / info.Height);
                float aspectBitmap = Math.Abs(width / height);
                var res = Math.Abs(aspectInfo - aspectBitmap);
                if (res < 0.27)
                    _aspect = BBAspect.AspectFill;
                else
                    _aspect = BBAspect.AspectFit;
            }
            else
                _aspect = aspect;

            float scaleX = info.Width / width;
            float scaleY = info.Height / height;

            if (_aspect != BBAspect.Fill)
            {
                scaleX = scaleY = _aspect == BBAspect.AspectFit ? Math.Min(scaleX, scaleY) : Math.Max(scaleX, scaleY);
                float left = ((info.Width - scaleX * width) / 2) + info.Left;
                float top = ((info.Height - scaleX * height) / 2) + info.Top;
                float right = left + scaleX * width;
                float bottom = top + scaleX * height;
                return (new SKRect(left, top, right, bottom), scaleX, scaleY);

            }
            else
                return (info, scaleX, scaleY);
        }


        internal static byte[] SKBitmapToBytes(SKBitmap bitmap)
        {
            byte[] imageData;

            using (SKData data = SKImage.FromBitmap(bitmap).Encode())
            using (Stream stream = data.AsStream())
            {
                imageData = new byte[stream.Length];
                stream.Read(imageData, 0, System.Convert.ToInt32(stream.Length));
            }
            GC.Collect();

            return imageData;
        }


        internal static ObservableCollection<Color> GetColors()
        {
            ObservableCollection<Color> colors = new ObservableCollection<Color>
            {
                 Colors.White
                ,Colors.Gray
                ,Colors.Black
                ,Colors.Red
                ,Colors.Orange
                ,Colors.Yellow
                ,Colors.Green
                ,Colors.Cyan
                ,Colors.Blue
                ,Colors.Violet
            };

            int count = 35;
            double offset = 16777215 / (double)count;
            for (int i = 1; i < count - 1; i++)
            {
                var color = Color.FromHex(((int)(i * offset)).ToString("X"));
                if (color.Alpha != -1 && color.Red != -1 && color.Green != -1 && color.Blue != -1)
                    colors.Add(color);
            }

            return colors;
        }

    }
}
