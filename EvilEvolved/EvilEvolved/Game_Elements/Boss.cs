using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvilutionClass
{
    class Boss : GenericItem
    {
        /// <summary>
        /// Create a Monster/Boss for the hero to battle against.
        /// </summary>
        /// <param name="name">The name of the scene if any.</param>
        public Boss(string name)
            : base(name)
        {
            //Velocity = (float)(10.0 / 60.0);
            //DirectionY = 0;
            //DirectionX = 0;
        }

        //properties

    }
}
