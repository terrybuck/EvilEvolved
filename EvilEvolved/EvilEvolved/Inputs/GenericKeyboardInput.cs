using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvilutionClass
{
    /// <summary>
    /// Takes in the  w,a,s,d keys as inputs
    /// </summary>
    public class GenericKeyboardInput : GenericInput
    {
        //types of key presses.... I put this in but I dont think I actually used it...
        public enum GenericKeyboardInputType { unknown, KeyPressed, KeyReleased };

        /// <summary>
        /// initialization of all inputs to false
        /// </summary>
        public GenericKeyboardInput() : base("keyboard_input")
        {
            this.IsWKeyPress = false;
            this.IsAKeyPress = false;
            this.IsSKeyPress = false;
            this.IsDKeyPress = false;
            this.keyboardInputType = GenericKeyboardInputType.unknown;

        }

        #region -----[Prooerties]

        public bool IsWKeyPress { get; set; }
        public bool IsAKeyPress { get; set; }
        public bool IsSKeyPress { get; set; }
        public bool IsDKeyPress { get; set; }

        #endregion

        public GenericKeyboardInputType keyboardInputType { get; set; }
    }
}
