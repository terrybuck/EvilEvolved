using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace EvilutionClass
{
    public class Arrow : GenericItem
    {
        /// <summary>
        /// creates an "attack" item that will cause damage when it collides with characters.
        /// </summary>
        /// <param name="name">The name of the scene if any.</param>
        public Arrow(string name, int directionX, int directionY, Vector2 location)
            : base(name)
        {
            Velocity = (float)(20.0 / 60.0);
            DirectionY = directionY;
            DirectionX = directionX;
            this.Location = location;
        }

        public override void Update(TimeSpan dt, GenericInput input)
        {

        }

        //properties
        public float Velocity { get; set; }
        int DirectionX { get; set; }
        int DirectionY { get; set; }

    }
}
