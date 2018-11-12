using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace EvilutionClass
{
    public class Boss : Villain
    {
        /// <summary>
        /// Create a Monster/Boss for the hero to battle against.
        /// </summary>
        /// <param name="name">The name of the scene if any.</param>
        public Boss(string name)
            : base(name)
        {

            TimeBetweenAttacks = 1000;
            Velocity = (float)(1.0 / 60.0);
            DirectionY = 0;
            DirectionX = 0;
            MaxHealth = 500.0f;
            CurrentHealth = 500.0f;

        }

        public override void Update(TimeSpan dt, GenericInput input)
        {
            base.Update(dt, input);
        }

    }
}
