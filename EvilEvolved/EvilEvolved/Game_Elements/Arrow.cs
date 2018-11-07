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
            this.Origin = location;
            Distance = 0.0;
            Range = 100;
            
        }

        public override void Update(TimeSpan dt, GenericInput input)
        {
            this.Location = new Vector2(this.Location.X + (float)(DirectionX * Velocity * dt.Milliseconds), this.Location.Y + (float)(DirectionY * Velocity * dt.Milliseconds));

            Distance = Math.Sqrt((double)((Location.X - Origin.X) * (Location.X - Origin.X) + (Location.Y - Origin.Y) * (Location.Y - Origin.Y)));

            if (Distance > Range)
            {
                
            }

        }

        //properties
        public float Velocity { get; set; }
        int DirectionX { get; set; }
        int DirectionY { get; set; }
        Vector2 Origin { get; set; }
        double Range { get; set; }
        double Distance { get; set; }
        double Damage { get; set; }
    }
}
