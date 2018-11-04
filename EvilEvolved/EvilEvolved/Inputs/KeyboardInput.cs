using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvilutionClass
{
    public class KeyboardInput : GenericInput
    {
        public enum KeyboardInputType { unknown, KeyPressed, KeyReleased };

        public KeyboardInput() : base("keyboard_input")
        {


            this.keyboardInputType = KeyboardInputType.unknown;

        }

        public KeyboardInputType keyboardInputType { get; set; }
    }
}
