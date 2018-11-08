using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;

namespace EvilutionClass
{
    /// <summary>
    /// A message that is sent if a collision is detected between attacks and their target
    /// </summary>
    public class Message_Collision : EvilutionClass.GenericInput
    {
        /// <summary>
        /// Message Constructor.
        /// </summary>
        /// <param name="Attack">The name of the scene we want to switch to.</param>
        /// <param name="Hitbox"> The bounding rectangle on the arrow</param>
        public Message_Collision(string name, GenericItem gi)
            : base("Collision")
        {
            this.Name = name;
            this.CollisionItem = gi;
        }

        public GenericItem CollisionItem;

    }
}
