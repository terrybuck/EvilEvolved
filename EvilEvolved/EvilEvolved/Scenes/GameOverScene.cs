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

        public void SetupScene()
        {

            // design the scene manually          
            _title_label = new EvilutionLabel("Game Over", Colors.White, (uint)(this._width * 0.90f), 100);
            _title_label.Y = 20;
            _title_label.FontSize = 50;
            CenterObject(_title_label, true, false);


            _start_button = new EvilutionButton("Main Menu", Colors.White, 350, 50);

            CenterObject(_start_button);

            this.AddObject(_title_label);
            this.AddObject(_start_button);



            // event callbacks
            _start_button.ButtonClick += _start_button_ButtonClick;

            void _start_button_ButtonClick(object sender, EvilutionButton_Event e)
            {
                // create the scene switch message to switch the current scene to the main game scene
                Message_SceneSwitch mss = new Message_SceneSwitch("Generic Title Scene");
                InputManager.AddInputItem(mss);
            }

        }
        private EvilutionLabel _title_label;
        private EvilutionButton _start_button;
    }
}
