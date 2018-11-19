using Evilution;
using System;
using Windows.Storage;

namespace EvilutionClass
{ 
    class Storage
    {
        public static StorageFolder Storage_Folder = ApplicationData.Current.LocalFolder;
        public static async void CreateFile()
        {
            try
            {
                await Storage_Folder.CreateFileAsync("highscores.txt", CreationCollisionOption.OpenIfExists);
            }
            catch
            {
                 //TODO: problem creating file message
            }
        }
        public static async void ReadFile()
        {
            try
            {
                StorageFile DataFile = await Storage_Folder.GetFileAsync("highscores.txt");
                MainPage.STRHighScore = await FileIO.ReadTextAsync(DataFile);
            }
            catch
            {
                //TODO: Problem reading file message
            }
        }
    }
}
