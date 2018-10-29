using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvilutionClass
{
    /// <summary>
    /// Storyboard class.  This class contains all the scenes inside the application.  This class also dictates what the CurrentScene is.
    /// </summary>
    public static class StoryBoard
    {
        public static Dictionary<string, GenericScene> SceneDictionary = new Dictionary<string, GenericScene>();

        public static bool AddScene(GenericScene gs)
        {
            //TODO: add a checksize function?, seem to be repeating this quite a bit
            int size_before_add = SceneDictionary.Count;
            SceneDictionary.Add(gs.Name, gs);
            int size_after_add = SceneDictionary.Count;
            if (size_after_add > size_before_add)
                return true;
            return false;
        }

        public static GenericScene CurrentScene { get; set; }
    }
}
