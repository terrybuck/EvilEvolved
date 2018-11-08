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
            Boss_CurrentHealth = Boss_MaxHealth;
            Boss_MaxHealth = 500.0f;
            Hero_CurrentHealth = Hero_MaxHealth;
            Hero_MaxHealth = 500.0f;
            LastHeroCollision = DateTime.Now;
            Hero_iFrames = 50;
            LastBossCollision = DateTime.Now;
            Boss_iFrames = 50;
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
                //update each generic item
                gi.Update(dt, input);

                //give the boss the Hero's location and update the scene with the location of the boss
                if (gi is Boss boss)
                {
                    boss.HeroLocation = new Vector2((float)HeroHitbox.Left + (float)(HeroHitbox.Width / 2), (float)HeroHitbox.Y + (float)(HeroHitbox.Height / 2));
                    BossHitbox = gi.BoundingRectangle;     
                }

                //update the scene with the location of the hero
                if (gi is Hero)
                {
                   HeroHitbox = gi.BoundingRectangle;
                }
                
                //find out if any of the attacks landed
                if (gi is Attack)
                {
                    Attack attack = (Attack)gi;
                    switch (attack.Type)
                    {
                        case (Attack.AttackType.Hero_Arrow):
                            {

                                if (RectHelper.Intersect(BossHitbox, gi.BoundingRectangle) != Rect.Empty)
                                {
                                    TimeSinceBossCollision = DateTime.Now - LastBossCollision;
                                    LastBossCollision = DateTime.Now;
                                    if (TimeSinceBossCollision.TotalMilliseconds > Boss_iFrames)
                                    {
                                        Message_Collision boss_collision = new Message_Collision("Arrow", gi, Attack.AttackType.Hero_Arrow, attack.Damage);
                                        InputManager.AddInputItem(boss_collision);
                                    }
                                }
                                break;
                            }
                        case (Attack.AttackType.Boss_Arrow):
                            {
                                if (RectHelper.Intersect(HeroHitbox, gi.BoundingRectangle) != Rect.Empty)
                                {
                                    TimeSinceHeroCollision = DateTime.Now - LastHeroCollision;
                                    LastHeroCollision = DateTime.Now;
                                    if (TimeSinceHeroCollision.TotalMilliseconds > Hero_iFrames)
                                    {
                                        Message_Collision hero_collision = new Message_Collision("Arrow", gi, Attack.AttackType.Boss_Arrow, attack.Damage);
                                        InputManager.AddInputItem(hero_collision);
                                    }
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
                            //TODO: add range and damage properties to a type of attack and pass it to the item rather than use magic numbers
                            Attack attack = new Attack(mhe.Name, mhe.DirectionX, mhe.DirectionY, mhe.Location, Attack.AttackType.Hero_Arrow, 100, 100.0f);
                            attack.SetBitmapFromImageDictionary("Arrow");
                            this.AddObject(attack);
                            break;
                        }
                    case (Message_Attack.AttackType.Boss_Arrow):
                        {
                            Attack arrow = new Attack(mhe.Name, mhe.DirectionX, mhe.DirectionY, mhe.Location, Attack.AttackType.Boss_Arrow, 200, 100.0f);
                            arrow.SetBitmapFromImageDictionary("Arrow");
                            this.AddObject(arrow);
                            break;
                        }
                }
            }

            if (input is Message_Collision)
            {
                Message_Collision attackInfo = (Message_Collision)input;
                switch(attackInfo.Type)
                {
                    case (Attack.AttackType.Boss_Arrow):
                        {
                            Hero_CurrentHealth -= attackInfo.Damage;
                            objects.Remove(attackInfo.CollisionItem);
                            break;
                        }
                    case (Attack.AttackType.Hero_Arrow):
                        {
                            Boss_CurrentHealth -= attackInfo.Damage;
                            objects.Remove(attackInfo.CollisionItem);
                            break;
                        }
                }
                if(Hero_CurrentHealth <= 0.0f)
                {
                    Hero_CurrentHealth = Hero_MaxHealth;
                    Message_SceneSwitch mss = new Message_SceneSwitch("Game Over Scene");
                    InputManager.AddInputItem(mss);
                }
                if (Boss_CurrentHealth <= 0.0f)
                {
                    Boss_CurrentHealth = Boss_MaxHealth;
                    Message_SceneSwitch mss = new Message_SceneSwitch("Game Over Scene");
                    InputManager.AddInputItem(mss);
                }
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

        //currently making the scene keep the hero and boss health.... I dont like this but it's simple for now and limits the input messages needed
        //I'd prefer to have the Hero and Boss HP as attributes of the hero and Boss
        public float Boss_MaxHealth { get; set; }
        public float Boss_CurrentHealth { get; set; }
        public float Hero_MaxHealth { get; set; }
        public float Hero_CurrentHealth { get; set; }
        TimeSpan TimeSinceHeroCollision { get; set; }
        DateTime LastHeroCollision { get; set; }
        int Hero_iFrames { get; set; }
        TimeSpan TimeSinceBossCollision { get; set; }
        DateTime LastBossCollision { get; set; }
        int Boss_iFrames { get; set; }
    }
}
