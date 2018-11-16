using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.UI.Xaml;
using Windows.Media.Core;
using Windows.Media.Playback;

namespace EvilutionClass
{
    /// <summary>
    /// The pipeline for all Bitmap Images.  This provides a path to access any images that have been loaded.
    /// </summary>
    public static class AudioManager
    {
        static public Dictionary<string, MediaPlayer> AudioDictionary = new Dictionary<string, MediaPlayer>();

        /// <summary>
        /// Loads a bitmap from a file and adds it to the ImageDictionary.
        /// </summary>
        /// <param name="key">A unique id to give to bitmap.</param>
        /// <param name="file_name">Path to the file to load.</param>
        /// <returns>Returns true if the image has been loaded and added to the ImageDictionary.  Else false.</returns>
        static public async Task<bool> AddAudio(string key, string file_path)
        {

            MediaPlayer mp = new MediaPlayer();
            Windows.Storage.StorageFolder folder = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFolderAsync(@"Assets");
            Windows.Storage.StorageFile file = await folder.GetFileAsync(file_path);
            mp.Source = MediaSource.CreateFromStorageFile(file);
            

            if (null == mp)
                return false;

            int size_before_add = AudioDictionary.Count;
            AudioDictionary.Add(key, mp);
            int size_after_add = AudioDictionary.Count;
            return (size_before_add < size_after_add);
         
        }

    }
}