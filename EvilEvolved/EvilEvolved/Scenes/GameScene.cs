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

                if (gi is Boss)
                {
                    BossHitbox = gi.BoundingRectangle;
                }

                if (gi is Hero)
                {
                   HeroHitbox = gi.BoundingRectangle;
                }
                               
                if (gi is Arrow)
                {
 
                    if (RectHelper.Intersect(BossHitbox, gi.BoundingRectangle) != Rect.Empty)
                    {
                        Message_BossCollision boss_collision = new Message_BossCollision("Arrow", gi);
                        InputManager.AddInputItem(boss_collision);
                    }

                }
               
            }

            if (input is Message_Attack)
            {

                Message_Attack mhe = (Message_Attack)input;

                switch (mhe.Type)
                {
                    case (Message_Attack.AttackType.Hero_Arrow):
                        {
                            Arrow arrow = new Arrow(mhe.Name, mhe.DirectionX, mhe.DirectionY, mhe.Location);
                            arrow.SetBitmapFromImageDictionary("Arrow");
                            this.AddObject(arrow);
                            break;
                        }
                }
            }

            if (input is Message_BossCollision)
            {
                Message_BossCollision arrowLocation = (Message_BossCollision)input;
                objects.Remove(arrowLocation.CollisionItem);
            }


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
