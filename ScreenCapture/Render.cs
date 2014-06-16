using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ScreenCapture
{
    public static class Render
    {
        static Render()
        {
            var flags = BindingFlags.NonPublic | BindingFlags.Static;
            var dpiProperty = typeof(SystemParameters).GetProperty("Dpi", flags);

            Dpi = (int)dpiProperty.GetValue(null, null);
            PixelSize = 96.0 / Dpi;
        }

        //Размер физического пикселя в виртуальных единицах
        public static double PixelSize { get; private set; }

        //Текущее разрешение
        public static int Dpi { get; private set; }
    }
}
