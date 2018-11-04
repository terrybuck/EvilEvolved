using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvilutionClass
{
    /// <summary>
    /// A Message telling the GameEngine that we need to switch the current scene to
    /// the previous scene. For example, they are done modifying a item and want to go back
    /// to the title scene.
    /// </summary>
    public class Message_GoBack : GenericInput
    {
        public Message_GoBack()
            : base("GO BACK")
        {
        }
    }
}
