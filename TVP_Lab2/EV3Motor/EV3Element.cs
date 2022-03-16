using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TVP_Lab2
{
    internal abstract class EV3MotorElement
    {
        public EV3Motor parent;
        public Rectangle rect;
        public Size size;
        public Size offset;

        public bool ContainsCursor(Point cursorPos)
        {
            return this.rect.Contains(cursorPos);
        }

        public Point GetPosition()
        {
            return this.rect.Location;
        }
    }
}
