using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace TVP_Lab2
{
    internal class SliderMenu : EV3MotorElement
    {
        public bool isOpen;
        public Rectangle slider;
        public int min, max;
        public SliderMenu(EV3Motor parent, Size offset, int value, int min = 0, int max = 360)
        {
            Size offsetWithoutResizing = offset + new Size(0, Utils.lowerButtonSize.Height);
            this.parent = parent;
            this.size = new Size((int)(Utils.sliderMenuSize.Width * this.parent.sizeCoef), (int)(Utils.sliderMenuSize.Height * this.parent.sizeCoef));
            this.offset = new Size((int)(offsetWithoutResizing.Width * this.parent.sizeCoef), (int)(offsetWithoutResizing.Height * this.parent.sizeCoef));
            this.rect = new Rectangle(this.parent.pos + this.offset, this.size);
            this.min = min;
            this.max = max;
            this.SetSlider(value);
            this.isOpen = false;
        }

        public int Click(Point cursorPos)
        {
            
            int value = -(int)((double)(this.max - this.min) * ((double)(cursorPos.Y - this.rect.Top) / (double)(this.rect.Bottom - this.rect.Top))) + this.max;
            this.SetSlider(value);
            return value;
        }

        public void SetSlider(int value)
        {
            double height = this.offset.Height + this.size.Height - this.size.Height * (value - this.min) / (double)(this.max - this.min);
            Debug.WriteLine(height);
            this.slider = new Rectangle(this.parent.pos + new Size((int)(this.offset.Width), (int)(height)),
                                        new Size((int)(Utils.sliderSize.Width * this.parent.sizeCoef), (int)(Utils.sliderSize.Height * this.parent.sizeCoef)));

        }

        public void Draw()
        {
            this.parent.g.FillRectangle(Brushes.WhiteSmoke, this.rect);
            this.parent.g.DrawRectangle(Pens.Black, this.rect);
            this.parent.g.DrawLine(Pens.Gray, this.rect.Location + new Size(this.size.Width / 2, 0), this.rect.Location + new Size(this.size.Width / 2, this.size.Height));
            this.parent.g.FillRectangle(Brushes.Black, this.slider);
        }
    }
}
