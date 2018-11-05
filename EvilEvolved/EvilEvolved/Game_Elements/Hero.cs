using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvilutionClass
{
    public class Hero : GenericItem
    {
        /// <summary>
        /// Create a playable Hero character.
        /// </summary>
        /// <param name="name">The name of the scene if any.</param>
        public Hero(string name)
            : base(name)
        {
            Velocity = (float)(2.0/60.0);
        }
        public override void Update(TimeSpan dt, GenericInput input)
        { 
            //           this.Location = new Vector2(this.Location.X + velocity, this.Location.Y + velocity);
        }

        public float Velocity { get; set; }
       // public 

    }
}
