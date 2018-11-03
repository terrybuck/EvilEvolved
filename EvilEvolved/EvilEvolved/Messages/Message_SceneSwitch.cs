using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvilutionClass
{
    /// <summary>
    /// A Message telling the GameEngine that we need to switch the current scene to a specific scene in the StoryBoard.
    /// </summary>
    public class Message_SceneSwitch : GenericInput
    {
        /// <summary>
        /// Message Constructor.
        /// </summary>
        /// <param name="target_scene">The name of the scene we want to switch to.</param>
        public Message_SceneSwitch(string target_scene = null)
            : base("Scene Switch")
        {
            this.TargetScene = target_scene;
        }

    }
}
