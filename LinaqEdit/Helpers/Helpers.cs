using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace LinaqEdit.Helpers
{
    public class Helpers
    {
        private static Random rand = new Random();
        public static Brush RandomBrush()
        {
            return new SolidColorBrush(Color.FromRgb((byte)rand.Next(0, 256), (byte)rand.Next(0, 256), (byte)rand.Next(0, 256)));
        }
    }
}
