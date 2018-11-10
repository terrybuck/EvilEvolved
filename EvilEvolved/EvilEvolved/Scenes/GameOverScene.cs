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
            GenericItem title = new GenericItem("Title");
            title.Location = new System.Numerics.Vector2(500, 200);
            title.SetBitmapFromImageDictionary("GameOver");
            this.AddObject(title);
            CenterObject(title, true, false);


            _start_button = new EvilutionButton("Main Menu", Colors.White, 350, 50);
            CenterObject(_start_button);
            this.AddObject(_start_button);



            // event callbacks
            _start_button.ButtonClick += _start_button_ButtonClick;

            void _start_button_ButtonClick(object sender, EvilutionButton_Event e)
            {
                // create the scene switch message to switch the current scene to the main game scene
                Message_SceneSwitch mss = new Message_SceneSwitch("Generic Title Scene");
                MessageManager.AddMessageItem(mss);
            }

        }
        private EvilutionButton _start_button;
    }
}
