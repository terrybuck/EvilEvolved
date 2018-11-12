using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace EvilutionClass
{
    public class Attack : GenericItem
    {
        public enum AttackType { none, Hero_Arrow, Boss_Arrow, Minion_Arrow }

        /// <summary>
        /// creates an "attack" item that will cause damage when it collides with characters.
        /// </summary>
        /// <param name="name"> Name of the gereral item</param>
        /// <param name="directionX"> -1 to 1 value representing the left to right direction of the attack</param>
        /// <param name="directionY"> -1 to 1 value representing the left to right direction of the attack</param>
        /// <param name="location"> the origin point from which the attack was fired</param>
        /// <param name="type"> type of attack</param>
        public Attack(string name, float directionX, float directionY, Vector2 location, AttackType type, int range, float damage)
            : base(name)
        {
            Velocity = (float)(20.0 / 60.0);
            DirectionY = directionY;
            DirectionX = directionX;
            this.Location = location;
            this.Origin = location;
            Distance = 0.0f;
            Range = range;
            Type = type;
            Damage = damage;
            
        }
        /// <summary>
        /// Update the Attack item
        /// </summary>
        /// <param name="dt"> A delta time since the last update was called</param>
        /// <param name="input">A GenericInput to process.</param>
        public override void Update(TimeSpan dt, GenericInput input)
        {
            UpdateLocation(dt);
        }
        //public override void Update(TimeSpan dt, GenericMessage message)
        //{
        //    UpdateLocation(dt);
        //}

        private void UpdateLocation(TimeSpan dt)
        {

            this.Location = new Vector2(this.Location.X + (float)(DirectionX * Velocity * dt.Milliseconds), this.Location.Y + (float)(DirectionY * Velocity * dt.Milliseconds));


            //Distance = Math.Sqrt((double)((Location.X - Origin.X) * (Location.X - Origin.X) + (Location.Y - Origin.Y) * (Location.Y - Origin.Y)));

            //if (Distance > Range)
            //{

            //}
        }

        #region ------[Properties]

        public float Velocity { get; set; }
        public float DirectionX { get; set; }
        public float DirectionY { get; set; }
        public Vector2 Origin { get; set; }
        public float Range { get; set; }
        public float Distance { get; set; }
        public float Damage { get; set; }
        public AttackType Type { get; set; }

        #endregion
    }
}
