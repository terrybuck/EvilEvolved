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

            TimeBetweenAttacks = 1000;
            LastAttack = DateTime.Now;
            Velocity = (float)(1.0 / 60.0);
            DirectionY = 0;
            DirectionX = 0;
            //MaxHealth = 500.0f;
            //CurrentHealth = 500.0f;

        }

        /// <summary>
        /// Update the Attack item
        /// </summary>
        /// <param name="dt"> A delta time since the last update was called</param>
        /// <param name="input">A GenericInput to process.</param>
        public override void Update(TimeSpan dt, GenericInput input)
        {
            //boss moves towards hero
            HeroDirectionVector();

            //boss attacks on a timer
            TimeSinceAttack = DateTime.Now - LastAttack;

                if (TimeSinceAttack.TotalMilliseconds > TimeBetweenAttacks)
                {
                    Message_Attack BossAttack = new Message_Attack("Boss_Attack", DirectionX, DirectionY, this.Location, Message_Attack.AttackType.Boss_Arrow, 200, 100.0f);
                    MessageManager.AddMessageItem(BossAttack);
                    LastAttack = DateTime.Now;
                }
            
                //Update bosses location on call to update
            this.Location = new Vector2(this.Location.X + (float)(DirectionX * Velocity * dt.Milliseconds), this.Location.Y + (float)(DirectionY * Velocity * dt.Milliseconds));

        }

        public void HeroDirectionVector()
        {
            float x_distance = HeroLocation.X - this.Location.X;
            float y_distance = HeroLocation.Y - this.Location.Y;

            //normalize
            if (x_distance > 0)
            {
                DirectionX = (float)Math.Sqrt((double)((x_distance * x_distance) / (x_distance * x_distance + y_distance * y_distance)));

            }
            else
            {
                DirectionX = -(float)Math.Sqrt((double)((x_distance * x_distance) / (x_distance * x_distance + y_distance * y_distance)));
            }
            if (y_distance > 0)
            {
                DirectionY = (float)Math.Sqrt((double)((y_distance * y_distance) / (x_distance * x_distance + y_distance * y_distance)));

            }
            else
            {
                DirectionY = -(float)Math.Sqrt((double)((y_distance * y_distance) / (x_distance * x_distance + y_distance * y_distance)));
            }
        }


        #region -----[Properties]

        public int TimeBetweenAttacks { get; set; }
        DateTime LastAttack { get; set; }
        TimeSpan TimeSinceAttack { get; set; }
        public float Velocity { get; set; }
        public float DirectionX { get; set; }
        public float DirectionY { get; set; }
        public Vector2 HeroLocation { get; set; }
        public int Level { get; set; }

        #endregion

    }
}
