using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TVP_Lab2
{
    internal class StatusButtonMenu : EV3MotorElement
    {
        public bool isOpen;
        public Image image;

        public StatusButtonMenu(EV3Motor parent)
        {
            this.parent = parent;
            this.size = new Size((int)(Utils.statusButtonMenuSize.Width * this.parent.sizeCoef), (int)(Utils.statusButtonMenuSize.Height * this.parent.sizeCoef));
            this.offset = new Size((int)(Utils.statusButtonMenuOffset.Width * this.parent.sizeCoef), (int)(Utils.statusButtonMenuOffset.Height * this.parent.sizeCoef));
            this.rect = new Rectangle(this.parent.pos + this.offset, this.size);
            this.isOpen = false;
        }

        public void SetImage()
        {
            Image image;
            switch (this.parent.status)
            {
                case Utils.MotorStatus.On:
                    image = Image.FromFile("D:/pictures/statusButtonMenuOn.png");
                    break;
                case Utils.MotorStatus.Off:
                    image = Image.FromFile("D:/pictures/statusButtonMenuOff.png");
                    break;
                case Utils.MotorStatus.OnForSeconds:
                    image = Image.FromFile("D:/pictures/statusButtonMenuOnForSeconds.png");
                    break;
                case Utils.MotorStatus.OnForDegrees:
                    image = Image.FromFile("D:/pictures/statusButtonMenuOnForDegrees.png");
                    break;
                case Utils.MotorStatus.OnForRotations:
                    image = Image.FromFile("D:/pictures/statusButtonMenuOnForRotations.png");
                    break;
                default:
                    throw new Exception("Status must be initialized");
            }
            var newSize = new Size((int)(image.Size.Width * this.parent.sizeCoef), (int)(image.Size.Height * this.parent.sizeCoef));
            var resizedImage = Utils.ResizeImage(image, newSize);
            this.image = resizedImage;
            this.Resize(newSize);
        }

        public void Draw()
        {
            this.parent.g.DrawImage(this.image, this.rect.Location);
        }

        public void Resize(Size newSize)
        {
            this.size = newSize;
            this.rect = new Rectangle(this.GetPosition(), newSize);
        }
    }
}
