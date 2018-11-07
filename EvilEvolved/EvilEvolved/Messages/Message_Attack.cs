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
    public class Message_Attack : EvilutionClass.GenericInput
    {
        public enum AttackType { none, Hero_Arrow, Boss_Arrow}
        /// <summary>
        /// Message Constructor.
        /// </summary>
        /// <param name="Attack">The name of the scene we want to switch to.</param>
        /// <param name="location"> The location of the hero at the time of attack</param>
        public Message_Attack(string attack, int directionX, int directionY, Vector2 location, AttackType type)
            : base("Attack")
        {
            Location = location;
            Name = attack;
            DirectionY = directionY;
            DirectionX = directionX;
            Type = type;
        }

        public Vector2 Location { get; set; }
        public int DirectionX { get; set; }
        public int DirectionY { get; set; }
        public AttackType Type { get; set; }
    }
}
