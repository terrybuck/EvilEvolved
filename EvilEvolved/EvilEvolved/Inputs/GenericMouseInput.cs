using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvilutionClass
{
    /// <summary>
    /// A class that takes in mouse inputs
    /// </summary>
    public class MouseGenericInput : GenericInput
    {
        //types  of mouse interactions
        public enum MouseGenericInputType { unknown, MouseMove, MousePressed, MouseReleased, MouseClick };
        public enum MouseGenericButtonType { None, Left, Middle, Right };

        /// <summary>
        /// initialise mouse inputs
        /// </summary>
        /// <param name="x"> x location of mouse on the canvas</param>
        /// <param name="y"> y location of mouse on the canvas</param>
        public MouseGenericInput(float x, float y) : base("mouse_input")
        {
            this.X = x;
            this.Y = y;
            this.MouseDown = false;
            this.IsLeftButtonPress = false;
            this.IsRightButtonPress = false;
            this.IsMiddleButtonPress = false;

            this.MouseInputType = MouseGenericInputType.unknown;
            this.MouseButtonType = MouseGenericButtonType.None;

        }


        #region -----[Properties]
        public float X { get; set; }
        public float Y { get; set; }

        public bool IsLeftButtonPress { get; set; }
        public bool IsRightButtonPress { get; set; }
        public bool IsMiddleButtonPress { get; set; }

        public bool MouseDown { get; set; }

        public MouseGenericInputType MouseInputType { get; set; }
        public MouseGenericButtonType MouseButtonType { get; set; }
        #endregion

    }
}
