using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Core;
using Windows.Media.Playback;
using Windows.UI;

namespace EvilutionClass
{
    /// <summary>
    /// A title scene where users can choose to start a new game, see high scores etc...
    /// </summary>
    public class TitleScene : GenericScene
    {
        /// <summary>
        /// Title scene initializer
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public TitleScene(int width, int height) : base("Generic Title Scene")
        {
            this._width = width;
            this._height = height;
            SetupScene();
        }


        public override void Update(TimeSpan dt, GenericInput input)
        {
            base.Update(dt, input);
        }


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
            _start_button = new EvilutionButton("New Game", Colors.White, 350, 50);
            _top_score_button = new EvilutionButton("Top Scores", Colors.White, 350, 50);
            _credits_button = new EvilutionButton("Credits", Colors.DarkGray, 350, 50);

            //Center Buttons
            CenterObject(_start_button);
            _top_score_button.Location = new Vector2(_start_button.Location.X, _start_button.Location.Y + _start_button.Size.Height + 10);
            _credits_button.Location = new Vector2(_top_score_button.Location.X, _top_score_button.Location.Y + _top_score_button.Size.Height + 10);

            //this.AddObject(_title_label);
            this.AddObject(_start_button);
            this.AddObject(_top_score_button);
            this.AddObject(_credits_button);


            // event callbacks
            _start_button.ButtonClick += _start_button_ButtonClick;
            _top_score_button.ButtonClick += _top_score_button_ButtonClick;
            _credits_button.ButtonClick += _credits_button_ButtonClick;

            void _credits_button_ButtonClick(object sender, EvilutionButton_Event e)
            {
                // create the scene switch message to switch the current scene to the credits scene
                Message_SceneSwitch mss = new Message_SceneSwitch("Credits Scene");
                MessageManager.AddMessageItem(mss);
            }
            void _top_score_button_ButtonClick(object sender, EvilutionButton_Event e)
            {
                // create the scene switch message to switch the current scene to the top score scene
                Message_SceneSwitch mss = new Message_SceneSwitch("Top Score Scene");
                MessageManager.AddMessageItem(mss);
            }
            void _start_button_ButtonClick(object sender, EvilutionButton_Event e)
            {
                // create the scene switch message to switch the current scene to the main game scene
                Message_SceneSwitch mss = new Message_SceneSwitch("Main Game Scene");
                MessageManager.AddMessageItem(mss);
            }
    }
        //private EvilutionLabel _title_label;
        private EvilutionButton _start_button;
        private EvilutionButton _top_score_button;
        private EvilutionButton _credits_button;
    }
}
