using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Graphics.Canvas;

using Windows.UI;

namespace EvilutionClass
{
    class EvilutionButton : GenericItem
    {
        public EvilutionButton(string text = "")
            :base()
        {
            this.Text = text;
            this.BorderColor = Colors.White;
            this.HighlightBorderColor = Colors.Lime;
        }

        public override void Update(TimeSpan dt, GenericInput input)
        {
            if (input is MouseGenericInput)
            {
                MouseGenericInput mgi = (MouseGenericInput)input;
                switch(mgi.MouseInputType)
                {
                    case MouseGenericInputType.MouseMove:
                        if (mgi.X >this.Location.X && mgi.X < this.Location.X+this.Size.Width &&
                            mgi.Y > this.Location.Y && mgi.Y < this.Location.Y+this.Size.Height)
                        {
                            IsHover = true;
                        }
                        else
                        {
                            IsHover = false;
                        }
                        break;
                }
            }
           // base.Update(dt, input);
        }

        public override void Draw(CanvasDrawingSession cds)
        {
            //test rectangle
            Windows.Foundation.Rect rect = new Windows.Foundation.Rect(Location.X, Location.Y, Size.Width, Size.Height);

            //draw the border
            if (IsHover)
            {
                cds.DrawRectangle(rect, HighlightBorderColor);
            }
            else
            {
                cds.DrawRectangle(rect, BorderColor);
            }

            Microsoft.Graphics.Canvas.Text.CanvasTextFormat ctf = new Microsoft.Graphics.Canvas.Text.CanvasTextFormat();
            ctf.VerticalAlignment = Microsoft.Graphics.Canvas.Text.CanvasVerticalAlignment.Center;
            ctf.HorizontalAlignment = Microsoft.Graphics.Canvas.Text.CanvasHorizontalAlignment.Center;

            cds.DrawText(Text, rect, Colors.Red, ctf);
        }

        public Color BorderColor { get; set; }
        public Color HighlightBorderColor { get; set; }
        private bool IsHover { get; set; }

        public string Text { get; set; }
    }
}
