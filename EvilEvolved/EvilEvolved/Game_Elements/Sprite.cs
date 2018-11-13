using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace EvilutionClass
{
    public class Sprite : GenericItem
    {
        /// <summary>
        /// Create a playable Hero character.
        /// </summary>
        /// <param name="name">The name of the hero character</param>
        public Sprite(string name)
            : base(name)
        {
            //hero's speed and initial direction
            Velocity = (float)(0.0);
            DirectionY = 0;
            DirectionX = 0;
            LastCollision = DateTime.Now;
            iFrames = 50;
        }

        /// <summary>
        /// Update the Attack item
        /// </summary>
        /// <param name="dt"> A delta time since the last update was called</param>
        /// <param name="input">A GenericInput to process.</param>
        public override void Update(TimeSpan dt, GenericInput input)
        {
            SetLocation(dt);
        }

        public void SetLocation(TimeSpan dt)
        {
            this.Location = new Vector2(this.Location.X + (float)(DirectionX * Velocity * dt.Milliseconds), this.Location.Y + (float)(DirectionY * Velocity * dt.Milliseconds));
        }

        #region -----[Properties]

        public float MaxHealth { get; set; }
        public float CurrentHealth { get; set; }

        public TimeSpan TimeSinceCollision { get; set; }
        public DateTime LastCollision { get; set; }
        public int iFrames { get; set; }


        public float Velocity { get; set; }
        public float DirectionX { get; set; }
        public float DirectionY { get; set; }

        public int Hitpoints { get; set; }
        #endregion


    }
}