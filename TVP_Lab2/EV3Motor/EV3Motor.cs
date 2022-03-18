using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace TVP_Lab2
{
    internal class EV3Motor
    {
        public static List<EV3Motor> AllEV3Motors = new List<EV3Motor> { };
        public Size size = new Size(1, 1);
        public Point pos;
        public double sizeCoef;
        public Utils.MotorStatus status;
        public Image image;
        // StatusButton
        public StatusButton statusButton;
        public LowerButton directionButton;
        public LowerButton timerButton;
        public LowerButton thirdButton;
        public Graphics g;

        public EV3Motor(Graphics graphics, Point pos, double sizeCoef = 1, Utils.MotorStatus status = Utils.MotorStatus.Off, int directionButtonValue = 0, int timerButtonValue = 0)
        {
            this.sizeCoef = sizeCoef;
            this.pos = pos;
            this.status = status;
            this.SetImage();
            // Buttons
            this.statusButton = new StatusButton(this);
            this.directionButton = new LowerButton(this, directionButtonValue, Utils.directionButtonOffset);
            this.timerButton = new LowerButton(this, timerButtonValue, Utils.timerButtonOffset);
            this.g = graphics;
            AllEV3Motors.Add(this);
        }

         ~EV3Motor() { 
            foreach (var motor in AllEV3Motors)
            {
                if (this == motor)
                {
                    AllEV3Motors.Remove(motor);
                }
            }
        }

        public static void DrawEvery()
        {
            bool first = true;
            foreach (var motor in AllEV3Motors)
            {
                if (first) motor.g.Clear(Color.White);
                first = false;
                motor.SetImage();
                motor.g.DrawImage(motor.image, motor.pos);
                motor.DrawButtons();
                motor.DrawMenus();
            }
        }

        public static void ClickEvery(Point cursorPos)
        {
            foreach (var motor in AllEV3Motors.AsEnumerable().Reverse())
            {
                motor.Click(cursorPos);
            }
        }

        public void Draw()
        {
            DrawEvery();
        }

        public void Click(Point cursorPos)
        {
            if (this.statusButton.menu.isOpen && this.statusButton.menu.ContainsCursor(cursorPos))
            {
                this.CloseAllMenus();
                this.ChangeStatusOnClick(cursorPos);
            }
            else if (this.statusButton.ContainsCursor(cursorPos))
            {
                this.CloseAllMenus();
                this.statusButton.menu.SetImage();
                this.statusButton.menu.isOpen = true;
            }
            else if (this.status != Utils.MotorStatus.Off)
            {
                if (this.directionButton.sliderMenu.isOpen && this.directionButton.sliderMenu.ContainsCursor(cursorPos))
                {
                    this.CloseAllMenus();
                    this.directionButton.sliderMenu.isOpen = true;
                    this.directionButton.value = this.directionButton.sliderMenu.Click(cursorPos);
                }
                else if (this.directionButton.ContainsCursor(cursorPos))
                {
                    this.CloseAllMenus();
                    this.directionButton.sliderMenu.isOpen = true;
                }
                else if (this.timerButton.sliderMenu.isOpen && this.timerButton.sliderMenu.ContainsCursor(cursorPos))
                {
                    this.CloseAllMenus();
                    this.timerButton.sliderMenu.isOpen = true;
                    this.timerButton.value = this.timerButton.sliderMenu.Click(cursorPos);
                }
                else if (this.timerButton.ContainsCursor(cursorPos))
                {
                    this.CloseAllMenus();
                    this.timerButton.sliderMenu.isOpen = true;
                }
                else
                {
                    this.CloseAllMenus();
                }
            }
            else
            {
                this.CloseAllMenus();
            }
            this.Draw();
        }

        public void ChangeStatusOnClick(Point cursorPos)
        {
            var imgChunk = this.statusButton.menu.image.Height / 5;
            var statusButtonMenuPos = this.statusButton.menu.GetPosition();
            if (cursorPos.Y > statusButtonMenuPos.Y + imgChunk * 0 && cursorPos.Y < statusButtonMenuPos.Y + imgChunk * 1)
            {
                this.status = Utils.MotorStatus.Off;
            }
            else if (cursorPos.Y > statusButtonMenuPos.Y + imgChunk * 1 && cursorPos.Y < statusButtonMenuPos.Y + imgChunk * 2)
            {
                this.status = Utils.MotorStatus.On;
            }
            else if (cursorPos.Y > statusButtonMenuPos.Y + imgChunk * 2 && cursorPos.Y < statusButtonMenuPos.Y + imgChunk * 3)
            {
                this.status = Utils.MotorStatus.OnForSeconds;
            }
            else if (cursorPos.Y > statusButtonMenuPos.Y + imgChunk * 3 && cursorPos.Y < statusButtonMenuPos.Y + imgChunk * 4)
            {
                this.status = Utils.MotorStatus.OnForDegrees;
            }
            else if (cursorPos.Y > statusButtonMenuPos.Y + imgChunk * 4 && cursorPos.Y < statusButtonMenuPos.Y + imgChunk * 5)
            {
                this.status = Utils.MotorStatus.OnForRotations;
            }
        }
        public void SetImage()
        {
            Image image;
            switch (this.status)
            {
                case Utils.MotorStatus.On:
                    image = Image.FromFile("../../../EV3Motor/assets/EV3MotorOn.png");
                    break;
                case Utils.MotorStatus.Off:
                    image = Image.FromFile("../../../EV3Motor/assets/EV3MotorOff.png");
                    break;
                case Utils.MotorStatus.OnForSeconds:
                    image = Image.FromFile("../../../EV3Motor/assets/EV3MotorOnForSeconds.png");
                    break;
                case Utils.MotorStatus.OnForDegrees:
                    image = Image.FromFile("../../../EV3Motor/assets/EV3MotorOnForDegrees.png");
                    break;
                case Utils.MotorStatus.OnForRotations:
                    image = Image.FromFile("../../../EV3Motor/assets/EV3MotorOnForRotations.png");
                    break;
                default:
                    throw new Exception("Status must be initialized");
            }
            var newSize = new Size((int)(image.Size.Width * this.sizeCoef), (int)(image.Size.Height * this.sizeCoef));
            var resizedImage = Utils.ResizeImage(image, newSize);
            this.image = resizedImage;
            this.Resize(newSize);
        }
        
        public void DrawButtons()
        {
            if (this.status != Utils.MotorStatus.Off)
            {
                this.directionButton.Draw();
                this.timerButton.Draw();
            }
        }

        public void DrawMenus()
        {
            if (this.statusButton.menu.isOpen)
            {
                this.statusButton.menu.Draw();
            }
            if (this.directionButton.sliderMenu.isOpen)
            {
                this.directionButton.sliderMenu.Draw();
            }
            if (this.timerButton.sliderMenu.isOpen)
            {
                this.timerButton.sliderMenu.Draw();
            }
        }

        public void CloseAllMenus()
        {
            this.statusButton.menu.isOpen = false;
            this.directionButton.sliderMenu.isOpen = false;
            this.timerButton.sliderMenu.isOpen = false;
        }

        public void Resize(Size newSize)
        {
            this.size = newSize;
        }
    }
}
