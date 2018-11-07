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
            Velocity = (float)(10.0/60.0);
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
            if (input is MouseGenericInput)
            {
                MouseGenericInput mgi = (MouseGenericInput)input;

                if (mgi.MouseInputType == MouseGenericInput.MouseGenericInputType.MouseClick && (DirectionX*DirectionX + DirectionY * DirectionY) != 0)
                {
                    // create the scene switch message to switch the current scene to the top score scene
                    Message_Attack heroAttack = new Message_Attack("Arrow", this.DirectionX, this.DirectionY, Location, Message_Attack.AttackType.Hero_Arrow);
                    InputManager.AddInputItem(heroAttack);
                }
            }
            this.Location = new Vector2(this.Location.X + (float)(DirectionX * Velocity * dt.Milliseconds), this.Location.Y + (float)(DirectionY * Velocity * dt.Milliseconds));
        }

        //simple method for determining the direction of the hero
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
            if (!gki.IsWKeyPress && !gki.IsSKeyPress || gki.IsWKeyPress && gki.IsSKeyPress)
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
            if (!gki.IsAKeyPress && !gki.IsDKeyPress || gki.IsAKeyPress && gki.IsDKeyPress)
            {
                this.DirectionX = 0;
            }
        }

        //properties
        public float Velocity { get; set; }
        public int DirectionX { get; set; }
        public int DirectionY { get; set; }
        public int Hitpoints { get; set; }


    }
}
