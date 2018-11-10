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
    public class Message_Collision : GenericMessage
    {
        /// <summary>
        /// Message Constructor.
        /// </summary>
        /// <param name="Attack">The name of the scene we want to switch to.</param>
        /// <param name="Hitbox"> The bounding rectangle on the arrow</param>
        public Message_Collision(string name, GenericItem gi, Attack.AttackType type, float damage)
            : base("Collision")
        {
            this.Name = name;
            this.CollisionItem = gi;
            this.Type = type;
            this.Damage = damage;
        }

        public GenericItem CollisionItem;
        public Attack.AttackType Type;
        public float Damage;

    }
}
