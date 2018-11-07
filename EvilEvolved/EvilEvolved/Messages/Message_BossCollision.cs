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
    /// A Message telling the GameScene where the arrow's bounding rectangle is located within the scene
    /// </summary>
    public class Message_BossCollision : EvilutionClass.GenericInput
    {
        /// <summary>
        /// Message Constructor.
        /// </summary>
        /// <param name="Attack">The name of the scene we want to switch to.</param>
        /// <param name="Hitbox"> The bounding rectangle on the arrow</param>
        public Message_BossCollision(string name, GenericItem gi)
            : base("Boss_Collision")
        {
            this.Name = name;
            this.CollisionItem = gi;
        }

        public GenericItem CollisionItem;
    }
}
