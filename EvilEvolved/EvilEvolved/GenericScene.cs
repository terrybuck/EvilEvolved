using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Graphics.Canvas;

namespace Evilution
{
    class GenericScene : GenericItem
    {
        public GenericScene(string name)
            :base(name)
        {

        }

        public override void Update(TimeSpan dt)
        {
            foreach (GenericItem gi in objects)
            {
                gi.Update(dt);
            }
        }

        public override void Draw(CanvasDrawingSession cds)
        {
            foreach (GenericItem gi in objects)
            {
                gi.Draw(cds);
            }
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

    }
}
