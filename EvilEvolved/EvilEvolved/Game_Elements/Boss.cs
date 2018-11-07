using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace EvilutionClass
{
    class Boss : GenericItem
    {
        /// <summary>
        /// Create a Monster/Boss for the hero to battle against.
        /// </summary>
        /// <param name="name">The name of the scene if any.</param>
        public Boss(string name)
            : base(name)
        {
            TimeBetweenAttacks = 1;
            LastAttack = DateTime.Now;
            Velocity = (float)(10.0 / 60.0);
            DirectionY = 0;
            DirectionX = 0;
        }

        public override void Update(TimeSpan dt, GenericInput input)
        {
            Random r = new Random();
            DirectionX = (float)(r.Next(-100, 100)) / 100.0f;
            DirectionY = (float)(r.Next(-100, 100)) / 100.0f;

            TimeSinceAttack = DateTime.Now - LastAttack;

                if (TimeSinceAttack.Seconds > TimeBetweenAttacks)
                {
                    Message_Attack BossAttack = new Message_Attack("Boss_Attack", -1, 1, this.Location, Message_Attack.AttackType.Boss_Arrow);
                    InputManager.AddInputItem(BossAttack);
                    LastAttack = DateTime.Now;
                }

            this.Location = new Vector2(this.Location.X + (float)(DirectionX * Velocity * dt.Milliseconds), this.Location.Y + (float)(DirectionY * Velocity * dt.Milliseconds));


        }
        //properties
        public int TimeBetweenAttacks { get; set; }
        DateTime LastAttack { get; set; }
        TimeSpan TimeSinceAttack { get; set; }
        float Velocity { get; set; }
        public float DirectionX { get; set; }
        public float DirectionY { get; set; }


    }
}
