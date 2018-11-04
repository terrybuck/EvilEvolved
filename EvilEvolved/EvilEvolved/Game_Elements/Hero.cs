using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvilutionClass
{
    public class Hero : GenericItem
    {
        /// <summary>
        /// Create a Hero character.
        /// </summary>
        /// <param name="name">The name of the scene if any.</param>
        public Hero(string name)
            : base(name)
        {
        }

    }
}
