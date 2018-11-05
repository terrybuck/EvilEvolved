using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvilutionClass
{
    public class GameScene : GenericScene
    {
        public GameScene(int width, int height) : base("Main Game Scene")
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
            Random r = new Random();
            Hero hero = new Hero("Hero");
            hero.Location = new System.Numerics.Vector2(r.Next(0, 1000), r.Next(0, 800));
            hero.SetBitmapFromImageDictionary("Hero");
            this.AddObject(hero);

        }
    }
}
