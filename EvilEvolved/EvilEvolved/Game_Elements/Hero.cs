using Microsoft.Graphics.Canvas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Graphics.Imaging;
using Windows.UI;

namespace EvilutionClass
{
    public class Hero : Sprite
    {

        /// <summary>
        /// Create a playable Hero character.
        /// </summary>
        /// <param name="name">The name of the hero character</param>
        public Hero(string name)
            : base(name)
        {
            //hero's speed and initial direction
            Velocity = (float)(15.0 / 60.0);
            DirectionY = 0;
            DirectionX = 0;
            ArrowRange = 100;
            ArrowDamage = 100.0f;
            MaxHealth = 500.0f;
            CurrentHealth = MaxHealth;
            HealthBar = new Windows.Foundation.Rect(Location.X - 10, Location.Y -10, 50, 5);


        }

        /// <summary>
        /// Draw the bitmap and healthbar of the hero
        /// </summary>
        /// <param name="cds"></param>
        public override void Draw(CanvasDrawingSession cds)
        {
            base.Draw(cds);

            //Draw Healthbar
            cds.DrawRectangle(HealthBar, Colors.Red);
            cds.FillRectangle((float)HealthBar.Left, (float)HealthBar.Top, (float)HealthBar.Width*CurrentHealth/MaxHealth, (float)HealthBar.Height, Colors.Red);

        }

        /// <summary>
        /// Update the Attack item
        /// </summary>
        /// <param name="dt"> A delta time since the last update was called</param>
        /// <param name="input">A GenericInput to process.</param>
        public override void Update(TimeSpan dt, GenericInput input)
        {
            //on keyboard input determine the Hero's direction
            if (input is GenericKeyboardInput)
            {
                GenericKeyboardInput gki = (GenericKeyboardInput)input;
                DirectionIn = new Vector2(DirectionX, DirectionY);
                DetermineDirection(gki);
                DirectionOut = new Vector2(DirectionX, DirectionY);
                AnimateHero();
            }

            //on mouse click launch an attack
            if (input is MouseGenericInput)
            {
                MouseGenericInput mgi = (MouseGenericInput)input;

                if (mgi.MouseInputType == MouseGenericInput.MouseGenericInputType.MouseClick && (DirectionX * DirectionX + DirectionY * DirectionY) != 0)
                {


                    // create the scene switch message to switch the current scene to the top score scene
                    Message_Attack heroAttack = new Message_Attack("Arrow", this.DirectionX, this.DirectionY, Location, Message_Attack.AttackType.Hero_Arrow, ArrowRange, ArrowDamage);
                    MessageManager.AddMessageItem(heroAttack);
                }
            }
            //update hero location 
            SetLocation(dt);
            HealthBar = new Windows.Foundation.Rect(Location.X - 10, Location.Y - 10, 50, 5);
        }

        /// <summary>
        /// Simple method to determine the direction of the hero based on the keyboard inputs
        /// </summary>
        /// <param name="gki"></param>
        public void DetermineDirection(GenericKeyboardInput gki)
        {
            DirectionX = 0;
            DirectionY = 0;

            if (gki.IsWKeyPress)
            {
                this.DirectionY -= 1;
            }
            if (gki.IsSKeyPress)
            {
                this.DirectionY += 1;
            }
            if (gki.IsAKeyPress)
            {
                this.DirectionX -= 1;
            }
            if (gki.IsDKeyPress)
            {
                this.DirectionX += 1;
            }

            //normally id take sqrt(x^2/x^2+y^2) but knowing that x is 1 or -1 and y is -1 or 1 we can cheat :)
            //this way the comp does less math, and we dont need to extract the negative to get the direction.. woohoo
            if (DirectionX != 0 && DirectionY != 0)
            {
                DirectionX = DirectionX * 0.707107f;
                DirectionY = DirectionY * 0.707107f;
            }
        }

        /// <summary>
        /// currently draws the hero in the direction he should be facing
        /// (Terry) TODO: create a generic sprite animation class and play an animation in the direction of travel
        /// </summary>
        public void AnimateHero()
        {
            if (DirectionIn != DirectionOut)
            {
                if (DirectionY < 0)
                {
                    this.SetBitmapFromImageDictionary("Hero_Up_1");
                }
                else if (DirectionY > 0)
                {
                    this.SetBitmapFromImageDictionary("Hero");
                }
                else if (DirectionX > 0)
                {
                    this.SetBitmapFromImageDictionary("Hero_Right_1");
                }
                else
                {
                    this.SetBitmapFromImageDictionary("Hero_Left_1");
                }
            }
        }

        ///// <summary>
        ///// Determine if the hero has changed direction....kind of unnecessary...
        ///// </summary>
        ///// <returns></returns>
        //private bool DirectionChanged()
        //{
        //    return (DirectionIn != DirectionOut);
        //}

        #region -----[Properties]

        public List<Attack> attacks = new List<Attack>();

        private Vector2 DirectionIn;
        private Vector2 DirectionOut;

        //DateTime LastAnimation { get; set; }
        //TimeSpan TimeSinceAnimation { get; set; }

        private readonly int ArrowRange;
        private readonly float ArrowDamage;

        Windows.Foundation.Rect HealthBar;

        #endregion


    }
}
