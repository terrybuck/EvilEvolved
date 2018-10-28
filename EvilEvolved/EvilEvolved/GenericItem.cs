using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Windows.Graphics.Imaging;
using Microsoft.Graphics.Canvas;
using System.Numerics;
using Windows.Foundation;

namespace Evilution
{
    public class GenericItem
    {
        //Constructor, default name is empty string if name is not provided
        public GenericItem(string name = "")
        {
            this.Name = name;

            this.DrawBoundingRectangle = false;
        }

        ///<summary>
        ///Update the GenericItem
        ///</summary>
        ///<param name= "dt"> A delta time since the last update was called.</param>
       public void Update(TimeSpan dt)
        {
        }

        ///<summary>
        ///Draw the GenericItem
        ///</summary>
        ///<param name= "cds"> A surface to draw on.</param>
        public void Draw(CanvasDrawingSession cds)
        {
            if (null == Bitmap)
                return;

            cds.DrawImage(Bitmap, Location);

            if (DrawBoundingRectangle)
            {
                cds.DrawRectangle(BoundingRectangle, Windows.UI.Colors.Purple);
            }
        }


        public bool SetBitmapFromImageDictionary(string key)
        {
            CanvasBitmap cb = null;
            if(Manage_Imported_Images.ImageDictionary.TryGetValue(key, out cb))
            {
                this.Bitmap = cb;
                this.Size = this.Bitmap.SizeInPixels;
                return true;
            }
            return false;
        }

        //Properties
        public string Name { get; set; }
        public Vector2 Location { get; set; }
        public CanvasBitmap Bitmap { get; set; }
        public BitmapSize Size { get; set; }
        public Rect BoundingRectangle { get { return new Rect(Location.X, Location.Y, Size.Width-1, Size.Height-1); } }
        public bool DrawBoundingRectangle { get; set; }



    }
}
