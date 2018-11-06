using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace EvilutionClass
{
    /// <summary>
    /// A Message telling the GameEngine that we need to switch the current scene to a specific scene in the StoryBoard.
    /// </summary>
    public class Message_HeroAttack : EvilutionClass.GenericInput
    {
        /// <summary>
        /// Message Constructor.
        /// </summary>
        /// <param name="Attack">The name of the scene we want to switch to.</param>
        /// <param name="location"> The location of the hero at the time of attack</param>
        public Message_HeroAttack(string attack, int directionX, int directionY, Vector2 location)
            : base("Attack")
        {
            Location = location;
            Name = attack;
            DirectionY = directionY;
            DirectionX = directionX;
        }

        public Vector2 Location { get; set; }
        public int DirectionX { get; set; }
        public int DirectionY { get; set; }
        // public new string Name { get; set; }
    }
}
