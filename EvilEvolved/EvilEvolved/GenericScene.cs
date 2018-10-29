using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Graphics.Canvas;

namespace EvilutionClass
{
    public class GenericScene : GenericItem
    {
        public GenericScene(string name)
            :base(name)
        {

        }

        public override void Update(TimeSpan dt, GenericInput input)
        {
            foreach (GenericItem gi in objects)
            {
                gi.Update(dt, input);
            }

            if (input is MouseGenericInput)
            {
                MouseGenericInput mgi = (MouseGenericInput)input;

                this.X = mgi.X;
                this.Y = mgi.Y;
                this.LeftButton = mgi.IsLeftButtonPress;
                this.MiddleButton = mgi.IsMiddleButtonPress;
                this.RightButton = mgi.IsRightButtonPress;


            }
        }

        public override void Draw(CanvasDrawingSession cds)
        {

            foreach (GenericItem gi in objects)
            {
                gi.Draw(cds);
            }

            cds.DrawText("X:" + this.X, new System.Numerics.Vector2(0, 100), Windows.UI.Colors.White);
            cds.DrawText("Y:" + this.Y, new System.Numerics.Vector2(0, 120), Windows.UI.Colors.White);
            cds.DrawText("LB:" + this.LeftButton, new System.Numerics.Vector2(10, 140), Windows.UI.Colors.White);
            cds.DrawText("MB:" + this.MiddleButton, new System.Numerics.Vector2(10, 160), Windows.UI.Colors.White);
            cds.DrawText("RB:" + this.RightButton, new System.Numerics.Vector2(10, 180), Windows.UI.Colors.White);
        }

        //I need to double check this later incase threadding has an unintended affect on the size of the list
        public bool AddObject(GenericItem gi)
        {
            if (null == objects)
                return false;

            int size_before_add = objects.Count;
            objects.Add(gi);
            int size_after_add = objects.Count;
            if (size_after_add > size_before_add)
                return true;
            return false;
        }

        public bool RemoveObject(GenericItem gi)
        {
            return objects.Remove(gi);
        }

        public bool RemoveObject(int index)
        {
            int size_before_remove = objects.Count;
            objects.RemoveAt(index);
            int size_after_remove = objects.Count;
            if (size_after_remove < size_before_remove)
                return true;
            return false;
            
        }

        protected List<GenericItem> objects = new List<GenericItem>();

        //TODO: remove on release or turn into test cases
        public float X { get; set; }
        public float Y { get; set; }
        public bool LeftButton { get; set; }
        public bool MiddleButton { get; set; }
        public bool RightButton { get; set; }


    }
}
