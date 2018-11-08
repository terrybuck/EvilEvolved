using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml;

namespace EvilutionClass
{
    /// <summary>
    /// The scene that contains the main game
    /// </summary>
    public class GameScene : GenericScene
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="width"> width of the rendered canvas</param>
        /// <param name="height"> height of the rendered canvas</param>
        public GameScene(int width, int height) : base("Main Game Scene")
        {
            this._width = width;
            this._height = height;
            SetupScene();
        }

        /// <summary>
        /// Update the scene
        /// </summary>
        /// <param name="dt"> A delta time since the last update was called </param>
        /// <param name="input"> Generic input </param>
        public override void Update(TimeSpan dt, GenericInput input)
        {
            foreach (GenericItem gi in objects)
            {
                gi.Update(dt, input);

                if (gi is Boss boss)
                {
                    boss.HeroLocation = new Vector2((float)HeroHitbox.Left + (float)(HeroHitbox.Width / 2), (float)HeroHitbox.Y + (float)(HeroHitbox.Height / 2));
                    BossHitbox = gi.BoundingRectangle;     
                }

                if (gi is Hero)
                {
                   HeroHitbox = gi.BoundingRectangle;
                }
                               
                if (gi is Attack)
                {
                    Attack attack = (Attack)gi;
                    switch (attack.Type)
                    {
                        case (Attack.AttackType.Hero_Arrow):
                            {

                                if (RectHelper.Intersect(BossHitbox, gi.BoundingRectangle) != Rect.Empty)
                                {
                                    Message_Collision boss_collision = new Message_Collision("Arrow", gi);
                                    InputManager.AddInputItem(boss_collision);
                                }
                                break;
                            }
                        case (Attack.AttackType.Boss_Arrow):
                            {
                                if (RectHelper.Intersect(HeroHitbox, gi.BoundingRectangle) != Rect.Empty)
                                {
                                    Message_Collision hero_collision = new Message_Collision("Arrow", gi);
                                    InputManager.AddInputItem(hero_collision);
                                }
                                break;

                            }
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
                            Attack attack = new Attack(mhe.Name, mhe.DirectionX, mhe.DirectionY, mhe.Location, Attack.AttackType.Hero_Arrow, 100);
                            attack.SetBitmapFromImageDictionary("Arrow");
                            this.AddObject(attack);
                            break;
                        }
                    case (Message_Attack.AttackType.Boss_Arrow):
                        {
                            Attack arrow = new Attack(mhe.Name, mhe.DirectionX, mhe.DirectionY, mhe.Location, Attack.AttackType.Boss_Arrow, 200);
                            arrow.SetBitmapFromImageDictionary("Arrow");
                            this.AddObject(arrow);
                            break;
                        }
                }
            }

            if (input is Message_Collision)
            {
                Message_Collision arrowLocation = (Message_Collision)input;
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
