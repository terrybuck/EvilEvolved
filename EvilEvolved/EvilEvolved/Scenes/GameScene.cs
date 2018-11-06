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
            foreach (GenericItem gi in objects)
            {
                gi.Update(dt, input);
            }

            if (input is Message_HeroAttack)
            {
                Message_HeroAttack mhe = (Message_HeroAttack)input;

                    Arrow arrow= new Arrow(mhe.Name, mhe.DirectionX, mhe.DirectionY, mhe.Location);
                    arrow.SetBitmapFromImageDictionary("Arrow");
                    this.AddObject(arrow);
            }
        }

        public void SetupScene()
        {
            Hero hero = new Hero("Hero");
            hero.Location = new System.Numerics.Vector2(700,700);
            hero.SetBitmapFromImageDictionary("Hero");
            this.AddObject(hero);

            GenericItem boss = new GenericItem("Boss");
            boss.Location = new System.Numerics.Vector2(1000, 70);
            boss.SetBitmapFromImageDictionary("Boss");
            this.AddObject(boss);

        }
    }
}
