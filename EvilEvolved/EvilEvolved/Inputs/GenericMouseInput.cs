using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvilutionClass
{
    public class MouseGenericInput : GenericInput
    {
        public enum MouseGenericInputType { unknown, MouseMove, MousePressed, MouseReleased, MouseClick };
        public enum MouseGenericButtonType { None, Left, Middle, Right };


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


        //Properties
        public float X { get; set; }
        public float Y { get; set; }

        public bool IsLeftButtonPress { get; set; }
        public bool IsRightButtonPress { get; set; }
        public bool IsMiddleButtonPress { get; set; }

        public bool MouseDown { get; set; }

        public MouseGenericInputType MouseInputType { get; set; }
        public MouseGenericButtonType MouseButtonType { get; set; }


    }
}
