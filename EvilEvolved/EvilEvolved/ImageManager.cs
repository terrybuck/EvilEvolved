using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.UI.Xaml;

namespace EvilutionClass
{
    /// <summary>
    /// The pipeline for all Bitmap Images.  This provides a path to access any images that have been loaded.
    /// </summary>
    public static class ImageManager
    {
        static public Dictionary<string, CanvasBitmap> ImageDictionary = new Dictionary<string, CanvasBitmap>();

        /// <summary>
        /// Loads a bitmap from a file and adds it to the ImageDictionary.
        /// </summary>
        /// <param name="key">A unique id to give to bitmap.</param>
        /// <param name="file_path">Path to the file to load.</param>
        /// <returns>Returns true if the image has been loaded and added to the ImageDictionary.  Else false.</returns>
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
