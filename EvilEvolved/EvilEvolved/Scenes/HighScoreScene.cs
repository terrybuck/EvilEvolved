using System;
using System.IO;
using System.Numerics;
using Windows.Storage;
using Windows.UI;

namespace EvilutionClass
{
    /// <summary>
    /// A title scene where users can choose to start a new game, see high scores etc...
    /// </summary>
    public class HighScoreScene : GenericScene
    {
        /// <summary>
        /// Title scene initializer
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public HighScoreScene(int width, int height) : base("Top Score Scene")
        {
            this._width = width;
            this._height = height;
            SetupScene();
        }


        public override void Update(TimeSpan dt, GenericInput input)
        {
            base.Update(dt, input);
        }

        /// <summary>
        /// Setup the initial state of the scene
        /// </summary>
        public override void SetupScene()
        {
            // design the scene manually   
            //Title scene is the only sene that starts playing audio by default
            if (AudioManager.AudioDictionary.TryGetValue("Generic Title Scene", out mp))
            {
                mp.Play();
            }

            //Evilution Title
            GenericItem title = new GenericItem("Title");
            title.Location = new System.Numerics.Vector2(500, 200);
            title.SetBitmapFromImageDictionary("Title");
            this.AddObject(title);
            CenterObject(title, true, false);

            //Buttons
            _start_button = new EvilutionButton("Main Menu", Colors.White, 350, 50);
            _start_button.Location = new Vector2(0, this._height - 100);
            //Center Buttons
            CenterObject(_start_button, true, false);

            this.AddObject(_start_button);



            // event callbacks
            _start_button.ButtonClick += _start_button_ButtonClick;

            void _start_button_ButtonClick(object sender, EvilutionButton_Event e)
            {
                // create the scene switch message to switch the current scene to the main game scene
                Message_SceneSwitch mss = new Message_SceneSwitch("Generic Title Scene");
                MessageManager.AddMessageItem(mss);
            }


            ////// Get the path of the save game
            //string fullpath = Path.Combine(ApplicationData.Current.LocalFolder.DisplayName, HighScoresFilename);

            ////If the file doesn't exist, make a fake one...
            //// Create the data to save
            //HighScoreData data = new HighScoreData(5);
            //data.PlayerName[0] = "Neil";
            //data.Score[0] = 5000;

            //data.PlayerName[1] = "Shawn";
            //data.Score[1] = 4000;

            //data.PlayerName[2] = "Mark";
            //data.Score[2] = 3000;

            //data.PlayerName[3] = "Cindy";
            //data.Score[3] = 2000;

            //data.PlayerName[4] = "Sam";
            //data.Score[4] = 1000;

            //HighScoreData.SaveHighScores(data, fullpath);

        }

        public readonly string HighScoresFilename = "highscores.lst";

        private EvilutionButton _start_button;
    }
}
