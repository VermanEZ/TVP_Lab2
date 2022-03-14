using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TVP_Lab2
{
    internal class LowerButton : EV3MotorElement
    {
        public SliderMenu sliderMenu;
        public int value;
        
        public LowerButton(EV3Motor parent, int value, Size offset)
        {
            this.parent = parent;
            this.value = value;
            this.size = new Size((int)(Utils.lowerButtonSize.Width * this.parent.sizeCoef), (int)(Utils.lowerButtonSize.Height * this.parent.sizeCoef));
            this.offset = new Size((int)(offset.Width * this.parent.sizeCoef), (int)(offset.Height * this.parent.sizeCoef));
            this.rect = new Rectangle(this.parent.pos + this.offset, this.size);
            this.sliderMenu = new SliderMenu(this.parent, offset, this.value);
        }

        public void Draw()
        {
            this.parent.g.DrawString($"{this.value}", new Font("arial", (int)(14 * this.parent.sizeCoef)), Brushes.Black, this.GetPosition() + this.size / 4);
        }
    }
}
