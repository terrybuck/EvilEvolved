using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;

namespace EvilutionClass
{
    /// <summary>
    /// A title scene where users can choose to start a new game, see high scores etc...
    /// </summary>
    public class GameOverScene : GenericScene
    {
        /// <summary>
        /// Title scene initializer
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public GameOverScene(int width, int height) : base("Game Over Scene")
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
        /// Set up the initial state of the scene
        /// </summary>
        public override void SetupScene()
        {

            // design the scene manually          
            GenericItem title = new GenericItem("Title");
            title.Location = new System.Numerics.Vector2(500, 100);
            title.SetBitmapFromImageDictionary("GameOver");
            this.AddObject(title);
            CenterObject(title, true, false);


            _start_button = new EvilutionButton("Main Menu", Colors.White, 350, 50);
            CenterObject(_start_button);
            this.AddObject(_start_button);

            if(StoryBoard.SceneHistory.Count > 0)
                Score = StoryBoard.SceneHistory.Peek().Score;

            _score_label = new EvilutionLabel("SCORE: " + Score, Colors.White, (uint)(this._width * 0.90f), 100);
            _score_label.Y = _start_button.Y - 200;
            _score_label.FontSize = 50;
            CenterObject(_score_label, true, false);
            this.AddObject(_score_label);



            // event callbacks
            _start_button.ButtonClick += _start_button_ButtonClick;

            void _start_button_ButtonClick(object sender, EvilutionButton_Event e)
            {
                // create the scene switch message to switch the current scene to the main game scene
                Message_SceneSwitch mss = new Message_SceneSwitch("Generic Title Scene");
                MessageManager.AddMessageItem(mss);
            }

        }

        private EvilutionLabel _score_label;
        private EvilutionButton _start_button;
    }
}
