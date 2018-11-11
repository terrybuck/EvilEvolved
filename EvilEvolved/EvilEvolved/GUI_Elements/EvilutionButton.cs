using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Graphics.Canvas;

using Windows.UI;
using static EvilutionClass.MouseGenericInput;

namespace EvilutionClass
{
    /// <summary>
    /// A Generic Button that responds the OnClick event.
    /// </summary>
    public class EvilutionButton : EvilutionLabel
    {
        /// <summary>
        /// EvilutionButton constructor.
        /// </summary>
        /// <param name="text">The text to show in the middle of the button.</param>
        /// <param name="foreground_color">The color of the text.</param>
        /// <param name="width">The default width.</param>
        /// <param name="height">the default height.</param>
        public EvilutionButton(string text = "", Color foreground_color = default(Color), uint width = 100, uint height = 50)
            : base(text, foreground_color, width, height)
        {
            this.HighlightBorderColor = Colors.OrangeRed;
        }

        /// <summary>
        /// Update.
        /// </summary>
        /// <param name="dt">The delta time.</param>
        /// <param name="gi">The input item if any.</param>
        public override void Update(TimeSpan dt, GenericInput gi)
        {
            if (gi is MouseGenericInput)
            {
                MouseGenericInput mgi = (MouseGenericInput)gi;
                switch (mgi.MouseInputType)
                {
                    case MouseGenericInputType.MouseMove:
                        {
                            // do hit test
                            if (mgi.X > this.Location.X && mgi.X < this.Location.X + this.Size.Width &&
                                mgi.Y > this.Location.Y && mgi.Y < this.Location.Y + this.Size.Height)
                            {
                                IsHover = true;
                            }
                            else
                            {
                                IsHover = false;
                            }
                        }
                        break;

                    case MouseGenericInputType.MouseClick:
                        {
                            if (IsHover)
                            {
                                // raise the event
                                OnButtonClicked(null);
                            }
                        }
                        break;
                }
            }
        }

        /// <summary>
        /// Draw.
        /// </summary>
        /// <param name="cds">The surface to draw on.</param>
        public override void Draw(Microsoft.Graphics.Canvas.CanvasDrawingSession cds)
        {
            // the bounding rectangle
            Windows.Foundation.Rect r = new Windows.Foundation.Rect(Location.X, Location.Y, Size.Width, Size.Height);

            // create the text description
            Microsoft.Graphics.Canvas.Text.CanvasTextFormat ctf = new Microsoft.Graphics.Canvas.Text.CanvasTextFormat();
            ctf.VerticalAlignment = Microsoft.Graphics.Canvas.Text.CanvasVerticalAlignment.Center;
            ctf.HorizontalAlignment = Microsoft.Graphics.Canvas.Text.CanvasHorizontalAlignment.Center;
            ctf.FontSize = 26;
            ctf.FontFamily = "Snap ITC";

            // draw the border
            if (IsHover)
            {
         //       cds.DrawRectangle(r, HighlightBorderColor);
                // draw the text
                cds.DrawText(Text, r, HighlightBorderColor, ctf);
            }
            else
            {
         //       cds.DrawRectangle(r, BorderColor);
                // draw the text
                cds.DrawText(Text, r, ForegroundColor, ctf);
            }





        }

        /// <summary>
        /// Reset the button.
        /// </summary>
        public override void Reset()
        {
            this.IsHover = false;
        }


        #region [Properties --------------------------------------------------]

        public Color HighlightBorderColor { get; set; }

        private bool IsHover { get; set; }

        #endregion


        #region [Events ------------------------------------------------------]

        public delegate void EvilutionButton_Event_Handler(object sender, EvilutionButton_Event e);

        private event EvilutionButton_Event_Handler _button_click;
        public event EvilutionButton_Event_Handler ButtonClick
        {
            add { _button_click += value; }
            remove { _button_click -= value; }
        }
        protected virtual void OnButtonClicked(EvilutionButton_Event e)
        {

            if (_button_click != null)
            {
                _button_click.Invoke(this, e);
            }
        }

        #endregion
    }

    /// <summary>
    /// Alias for the Button Event.
    /// </summary>
    public class EvilutionButton_Event : EventArgs
    {
    }
}

