using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Media.Playback;
using Windows.Media;
using Windows.UI;
using Windows.UI.Xaml;

namespace EvilutionClass
{
    /// <summary>
    /// The scene that contains the main game
    /// </summary>
    public class GameScene : GenericScene
    {
        /// <summary>
        /// Main game scene initializer
        /// </summary>
        /// <param name="width"> width of the rendered canvas</param>
        /// <param name="height"> height of the rendered canvas</param>
        public GameScene(int width, int height) : base("Main Game Scene")
        {
            this._width = width;
            this._height = height;
 
            

            //sets the bosses health
            Boss_MaxHealth = 500.0f;
            Boss_CurrentHealth = Boss_MaxHealth;
            
            //sets the hero's health
            Hero_MaxHealth = 500.0f;
            Hero_CurrentHealth = Hero_MaxHealth;

            //variables used to determine if the hero is invulnerable
            LastHeroCollision = DateTime.Now;
            Hero_iFrames = 50;

            //variables used to determine if the boss is invulnerable
            LastBossCollision = DateTime.Now;
            Boss_iFrames = 50;
            Boss_Level_Up = false;



            //Keep track of score
            Score = 0.0f;

            //Draw the initialized scene
            SetupScene();
        }

        /// <summary>
        /// Update the scene
        /// </summary>
        /// <param name="dt"> A delta time since the last update was called </param>
        /// <param name="input"> Generic input </param>
        public override void Update(TimeSpan dt, GenericInput input)
        {
            if(mp.PlaybackSession.PlaybackState != MediaPlaybackState.Playing)
            {
                mp.PlaybackSession.Position = TimeSpan.Zero;
                mp.Play();
            }
            foreach (GenericItem gi in objects)
            {
                //update each generic item
                gi.Update(dt, input);

                //give the boss the Hero's location and update the scene with the location of the boss
                if (gi is Boss boss)
                {
                    if (Boss_Level_Up)
                    {
                        boss.Level += 1;
                        boss.TimeBetweenAttacks = 250 + 750 / boss.Level;
                        boss.Velocity += (0.5f / 60.0f);
                        Boss_Level_Up = false;
                    }
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
                                    if (TimeSinceBossCollision.TotalMilliseconds > Boss_iFrames)
                                    {
                                        Message_Collision boss_collision = new Message_Collision("Arrow", gi, Attack.AttackType.Hero_Arrow, attack.Damage);
                                        MessageManager.AddMessageItem(boss_collision);
                                        LastBossCollision = DateTime.Now;
                                    }
                                }
                                break;
                            }
                        case (Attack.AttackType.Boss_Arrow):
                            {
                                if (RectHelper.Intersect(HeroHitbox, gi.BoundingRectangle) != Rect.Empty)
                                {
                                    TimeSinceHeroCollision = DateTime.Now - LastHeroCollision;
                                    if (TimeSinceHeroCollision.TotalMilliseconds > Hero_iFrames)
                                    {
                                        Message_Collision hero_collision = new Message_Collision("Arrow", gi, Attack.AttackType.Boss_Arrow, attack.Damage);
                                        MessageManager.AddMessageItem(hero_collision);
                                        LastHeroCollision = DateTime.Now;
                                    }
                                }
                                break;

                            }
                    }

                }

            }
        }

        public override void Update(TimeSpan dt, GenericMessage message)
        {
            foreach (GenericItem gi in objects)
            {
                //update each generic item
                gi.Update(dt, message);
            }
                if (message is Message_Attack)
                {

                    Message_Attack mhe = (Message_Attack)message;

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

                if (message is Message_Collision)
                {
                    Message_Collision attackInfo = (Message_Collision)message;
                    switch (attackInfo.Type)
                    {
                        case (Attack.AttackType.Boss_Arrow):
                            {
                                Hero_CurrentHealth -= attackInfo.Damage;
                                objects.Remove(attackInfo.CollisionItem);
                                break;
                            }
                        case (Attack.AttackType.Hero_Arrow):
                            {
                                Score += attackInfo.Damage;
                                _score_label.UpdateText("SCORE: " + Score);
                                Boss_CurrentHealth -= attackInfo.Damage;
                                objects.Remove(attackInfo.CollisionItem);
                                break;
                            }
                    }
                    if (Hero_CurrentHealth <= 0.0f)
                    {
                        if (null != mp)
                        {
                            mp.Pause();
                            mp.PlaybackSession.Position = TimeSpan.Zero;
                        }

                        objects.Clear();
                        SetupScene();
                        Message_SceneSwitch mss = new Message_SceneSwitch("Game Over Scene");
                        MessageManager.AddMessageItem(mss);
                    }
                    if (Boss_CurrentHealth <= 0.0f)
                    {
                        Boss_Level_Up = true;
                        Boss_MaxHealth += 100;
                        Boss_CurrentHealth = Boss_MaxHealth;
                    }
                

            }
        }

        public void SetupScene()
        {
            Boss_MaxHealth = 500.0f;
            Hero_MaxHealth = 500.0f;
            Boss_CurrentHealth = Boss_MaxHealth;
            Hero_CurrentHealth = Hero_MaxHealth;

            Score = 0.0f;

            _score_label = new EvilutionLabel("SCORE: " + Score, Colors.White, (uint)(this._width * 0.90f), 100);
            _score_label.Y = 20;
            _score_label.FontSize = 50;
            CenterObject(_score_label, true, false);
            this.AddObject(_score_label);

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

        //properties to set I-Frames for hero and boss so they dont get hit multiple times by 1 attack
        TimeSpan TimeSinceHeroCollision { get; set; }
        DateTime LastHeroCollision { get; set; }
        int Hero_iFrames { get; set; }
        TimeSpan TimeSinceBossCollision { get; set; }
        DateTime LastBossCollision { get; set; }
        int Boss_iFrames { get; set; }

        //indicates to the boss that he has died and thereby leveled up
        bool Boss_Level_Up { get; set; }

        private EvilutionLabel _score_label;
        float Score { get; set; }
    }
}
