using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TVP_Lab2
{
    public class Utils
    {
        public static Size statusButtonSize = new Size(85, 53);
        public static Size statusButtonOffset = new Size(10, 120);
        public static Size statusButtonMenuSize = statusButtonSize * 2;
        public static Size statusButtonMenuOffset = new Size(statusButtonOffset.Width, statusButtonOffset.Height + statusButtonSize.Height / 2);

        public static Size lowerButtonSize = new Size(62, 50);
        public static Size directionButtonOffset = new Size(103, 115);
        public static Size timerButtonOffset = directionButtonOffset + new Size(lowerButtonSize.Width, 0);
        public static Size sliderMenuSize = new Size(45, 140);
        public static Size sliderSize = new Size(sliderMenuSize.Width, sliderMenuSize.Height / 20);
        public enum MotorStatus
        {
            Off,
            On,
            OnForSeconds,
            OnForDegrees,
            OnForRotations
        };
        public static Image ResizeImage(Image img, Size newSize)
        {
            Image newImage = new Bitmap(newSize.Width, newSize.Height);
            using (Graphics g2 = Graphics.FromImage(newImage))
                g2.DrawImage(img, 0, 0, newSize.Width, newSize.Height);
            return newImage;
        }
    }
}
