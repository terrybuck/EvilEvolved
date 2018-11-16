using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Graphics.Canvas;
using Windows.Media.Playback;

namespace EvilutionClass
{
    public class GenericScene : GenericItem
    {
        /// <summary>
        /// Create a GenericScene.
        /// </summary>
        /// <param name="name">The name of the scene if any.</param>
        public GenericScene(string name)
            : base(name)
        {
        }

        /// <summary>
        /// Update the scene.
        /// </summary>
        /// <param name="dt">A delta time since the last update.</param>
        /// <param name="gi">A GenericInput to process.</param>
        public override void Update(TimeSpan dt, GenericInput input)
        {
            foreach (GenericItem gi in objects)
            {
                gi.Update(dt, input);
            }
        }

        public override void Update(TimeSpan dt, GenericMessage message)
        {
            foreach (GenericItem gi in objects)
            {
                gi.Update(dt, message);
            }
        }

        /// <summary>
        /// Draw the scene onto a surface.
        /// </summary>
        /// <param name="cds">A surface to draw the scene on.</param>
        public override void Draw(CanvasDrawingSession cds)
        {

            foreach (GenericItem gi in objects)
            {
                gi.Draw(cds);
            }

        }

        /// <summary>
        /// Add a GenericItem into the object list.
        /// </summary>
        /// <param name="gi">A GenericItem to add.</param>
        /// <returns>Returns true if the object was successfully inserted.  Else false.</returns>
        public virtual bool AddObject(GenericItem gi)
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

        /// <summary>
        /// Remove a GenericItem from the object list.
        /// </summary>
        /// <param name="gi">A GenericItem to remove.</param>
        /// <returns>Returns true if the object was successfully removed.  Else false.</returns>
        public bool RemoveObject(GenericItem gi)
        {
            return objects.Remove(gi);
        }

        /// <summary>
        /// Remove a GenericItem from the object list.
        /// </summary>
        /// <param name="index">The index of the GenericItem to remove.</param>
        /// <returns>Returns true if the object was successfully removed.  Else false.</returns>
        public bool RemoveObject(int index)
        {
            int size_before_remove = objects.Count;
            objects.RemoveAt(index);
            int size_after_remove = objects.Count;
            if (size_after_remove < size_before_remove)
                return true;
            return false;

        }


        #region [Helpers Functions -------------------------------------------]

        /// <summary>
        /// Centers the GenericItem on the screen.
        /// </summary>
        /// <param name="gi">The GenericItem to center.</param>
        /// <param name="center_on_width">If true, center horizontally.</param>
        /// <param name="center_on_height">If true, center vertically.</param>
        protected void CenterObject(GenericItem gi, bool center_on_width = true, bool center_on_height = true)
        {
            if (gi == null)
                return;

            Vector2 correction = new Vector2();
            if (center_on_width)
            {
                correction.X = (int)(this._width / 2 - gi.Size.Width / 2);
            }
            else
            {
                correction.X = gi.Location.X;
            }

            if (center_on_height)
            {
                correction.Y = (int)(this._height / 2 - gi.Size.Height / 2);
            }
            else
            {
                correction.Y = gi.Location.Y;
            }

            gi.Location = correction;
        }

        #endregion

        public virtual void SetupScene()
        {
        }

        public override void Reset()
        {
            objects.Clear();
            SetupScene();
        }


        #region [Properties --------------------------------------------------]

        public float Score { get; set; }

        protected List<GenericItem> objects = new List<GenericItem>();

        // Size of scene
        protected int _width;
        protected int _height;

        //each scene has it's own background music
        public MediaPlayer mp;

        #endregion

    }
}
