using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.UI.Xaml;

namespace Evilution
{
    //Class used to import sprites and images into game
    public static class Manage_Imported_Images
    {
        static public Dictionary<string, CanvasBitmap> ImageDictionary = new Dictionary<string, CanvasBitmap>();

        static public async Task<bool> AddImage(string key, string file_path)
        {

            if (null == ParentCanvas)
                return false;

            CanvasBitmap cb = await CanvasBitmap.LoadAsync(ParentCanvas, file_path);

            if (null == cb)
                return false;

            int size_before_add = ImageDictionary.Count;
            ImageDictionary.Add(key, cb);
            int size_after_add = ImageDictionary.Count;
            if (size_before_add<size_after_add)
            {
                return true;
            }

            return false;
        }

        static public CanvasControl ParentCanvas = null;

    }
}
