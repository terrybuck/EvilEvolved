using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace EvilutionClass
{
    public class Villain : Sprite
    {
        /// <summary>
        /// Create a Monster/Boss for the hero to battle against.
        /// </summary>
        /// <param name="name">The name of the scene if any.</param>
        public Villain(string name)
            : base(name)
        {

            TimeBetweenAttacks = 3000;
            LastAttack = DateTime.Now;

            Velocity = (float)(5.0 / 60.0);
            DirectionY = 0;
            DirectionX = 0;

            LastCollision = DateTime.Now;

            MaxHealth = 100.0f;
            CurrentHealth = 100.0f;

            Damage = 50.0f;
            Range = 100;

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
                Message_Attack VillainAttack = new Message_Attack("Villain_Attack", DirectionX, DirectionY, this.Location, Message_Attack.AttackType.Minion_Arrow, 200, Damage);
                MessageManager.AddMessageItem(VillainAttack);
                LastAttack = DateTime.Now;
            }

            //Update bosses location on call to update
            SetLocation(dt);

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
        public Vector2 HeroLocation { get; set; }
        public bool HurtImage = false;
        public float Damage;
        public int Range;

        #endregion

    }
}