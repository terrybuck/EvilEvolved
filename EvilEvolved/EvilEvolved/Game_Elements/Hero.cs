using Microsoft.Graphics.Canvas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Graphics.Imaging;

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
            Velocity = (float)(5.0/60.0);
            DirectionY = 0;
            DirectionX = 0;
        }
        public override void Update(TimeSpan dt, GenericInput input)
        {
            if (input is GenericKeyboardInput)
            {
                GenericKeyboardInput gki = (GenericKeyboardInput)input;
                DetermineDirection(gki);
                
            }
          this.Location = new Vector2(this.Location.X + (float)(DirectionX*Velocity*dt.Milliseconds), this.Location.Y + (float)(DirectionY*Velocity)*dt.Milliseconds);
        }

        //simple method for determining the direction of the hero, badly implemeted, causes input lag
        public void DetermineDirection(GenericKeyboardInput gki)
        {
            if (gki.IsWKeyPress)
            {
                this.DirectionY = -1;
            }
            if (gki.IsSKeyPress)
            {
                this.DirectionY = 1;
            }
            if (!gki.IsWKeyPress && !gki.IsSKeyPress)
            {
                this.DirectionY = 0;
            }
            if (gki.IsAKeyPress)
            {
                this.DirectionX = -1;
            }
            if (gki.IsDKeyPress)
            {
                this.DirectionX = 1;
            }
            if (!gki.IsAKeyPress && !gki.IsDKeyPress)
            {
                this.DirectionX = 0;
            }
        }

        //properties
        public float Velocity { get; set; }
        int DirectionX { get; set; }
        int DirectionY { get; set; }


    }
}
