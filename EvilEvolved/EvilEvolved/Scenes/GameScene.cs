using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml;

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

            //if(input is Message_ArrowHitbox)
            //{
            //    Message_ArrowHitbox arrowLocation = (Message_ArrowHitbox)input;
            //    if(RectHelper.Intersect(BossHitbox, arrowLocation.Hitbox) == Rect.Empty)
            //    {
                    
            //    }
            //}
        }
             

        public void SetupScene()
        {
            Hero hero = new Hero("Hero");
            hero.Location = new System.Numerics.Vector2(700,700);
            hero.SetBitmapFromImageDictionary("Hero");
            this.AddObject(hero);
            HeroHitbox = hero.BoundingRectangle;

            Boss boss = new Boss("Boss");
            boss.Location = new System.Numerics.Vector2(1000, 70);
            boss.SetBitmapFromImageDictionary("Boss");
            this.AddObject(boss);
            BossHitbox = boss.BoundingRectangle;

        }

        //properties
        Rect HeroHitbox;
        Rect BossHitbox;
    }
}
