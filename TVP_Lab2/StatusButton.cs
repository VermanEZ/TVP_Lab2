using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TVP_Lab2
{
    internal class StatusButton : EV3MotorElement
    {
        public StatusButtonMenu menu;

        public StatusButton(EV3Motor parent)
        {
            this.parent = parent;
            this.size = new Size((int)(Utils.statusButtonSize.Width * this.parent.sizeCoef), (int)(Utils.statusButtonSize.Height * this.parent.sizeCoef));
            this.offset = new Size((int)(Utils.statusButtonOffset.Width * this.parent.sizeCoef), (int)(Utils.statusButtonOffset.Height * this.parent.sizeCoef));
            this.rect = new Rectangle(this.parent.pos + this.offset, this.size);
            this.menu = new StatusButtonMenu(this.parent);
        }
    }
}
