using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace EvilutionClass
{
    class BossAttack : Arrow
    {
        public BossAttack(string name, int directionX, int directionY, Vector2 location)
            : base(name, directionX, directionY, location)
        {
            Velocity = (float)(20.0 / 60.0);
            DirectionY = directionY;
            DirectionX = directionX;
            this.Location = location;
            this.Origin = location;
            Distance = 0.0;
            Range = 100;
        }
    }
}
