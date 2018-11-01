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
        }

        public override void Update(TimeSpan dt, GenericInput input)
        {
           // base.Update(dt, input);
        }

        public override void Draw(CanvasDrawingSession cds)
        {
            Windows.Foundation.Rect rect = new Windows.Foundation.Rect(Location.X, Location.Y, Size.Width, Size.Height);

            cds.DrawRectangle(rect, BorderColor);

            Microsoft.Graphics.Canvas.Text.CanvasTextFormat ctf = new Microsoft.Graphics.Canvas.Text.CanvasTextFormat();
            ctf.VerticalAlignment = Microsoft.Graphics.Canvas.Text.CanvasVerticalAlignment.Center;
            ctf.HorizontalAlignment = Microsoft.Graphics.Canvas.Text.CanvasHorizontalAlignment.Center;

            cds.DrawText(Text, rect, Colors.Red, ctf);
        }

        public Color BorderColor { get; set; }

        public string Text { get; set; }
    }
}
