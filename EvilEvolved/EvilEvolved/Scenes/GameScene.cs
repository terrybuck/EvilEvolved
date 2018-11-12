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
using Microsoft.Graphics.Canvas;

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
            if (mp.PlaybackSession.PlaybackState != MediaPlaybackState.Playing)
            {
                mp.PlaybackSession.Position = TimeSpan.Zero;
                mp.Play();
            }

            foreach (Hero hero in heros)
            {
                //update each generic item
                hero.Update(dt, input);
            }
            foreach (Boss boss in villains)
            {
                //update each generic item
                boss.Update(dt, input);
                boss.TimeSinceCollision = DateTime.Now - boss.LastCollision;
                if(boss.TimeSinceCollision.TotalMilliseconds > 300 && boss.HurtImage)
                {
                    boss.SetBitmapFromImageDictionary("Boss");
                }
                foreach (Hero hero in heros)
                {
                    boss.HeroLocation = hero.Location;
                }
            }
            foreach (GenericItem gi in objects)
            {
                //update each generic item
                gi.Update(dt, input);

                //find out if any of the attacks landed
                if (gi is Attack)
                {
                    Attack attack = (Attack)gi;
                    switch (attack.Type)
                    {
                        case (Attack.AttackType.Hero_Arrow):
                            {
                                foreach (Boss boss in villains)
                                {
                                    if (RectHelper.Intersect(boss.BoundingRectangle, gi.BoundingRectangle) != Rect.Empty)
                                    {
                                        boss.TimeSinceCollision = DateTime.Now - boss.LastCollision;
                                        if (boss.TimeSinceCollision.TotalMilliseconds > boss.iFrames)
                                        {
                                            Message_Collision boss_collision = new Message_Collision("Arrow", gi, Attack.AttackType.Hero_Arrow, attack.Damage);
                                            MessageManager.AddMessageItem(boss_collision);
                                            boss.LastCollision = DateTime.Now;
                                            boss.SetBitmapFromImageDictionary("BossHurt");
                                            boss.HurtImage = true;
                                        }
                                    }
                                }
                                break;
                            }
                        case (Attack.AttackType.Boss_Arrow):
                            {
                                foreach (Hero hero in heros)
                                {
                                    if (RectHelper.Intersect(hero.BoundingRectangle, gi.BoundingRectangle) != Rect.Empty)
                                    {
                                        hero.TimeSinceCollision = DateTime.Now - hero.LastCollision;
                                        if (hero.TimeSinceCollision.TotalMilliseconds > hero.iFrames)
                                        {
                                            Message_Collision hero_collision = new Message_Collision("Arrow", gi, Attack.AttackType.Boss_Arrow, attack.Damage);
                                            MessageManager.AddMessageItem(hero_collision);
                                            hero.LastCollision = DateTime.Now;
                                        }
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
            foreach (Hero hero in heros)
            {
                //update each generic item
                hero.Update(dt, message);
            }
            foreach (Boss boss in villains)
            {
                //update each generic item
                boss.Update(dt, message);
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
                            foreach (Hero hero in heros)
                            {
                                hero.CurrentHealth -= attackInfo.Damage;
                                objects.Remove(attackInfo.CollisionItem);
                            }
                            break;
                        }
                        case (Attack.AttackType.Hero_Arrow):
                            {
                            foreach (Boss boss in villains)
                            {
                                Score += attackInfo.Damage;
                                _score_label.UpdateText("SCORE: " + Score);
                                boss.CurrentHealth -= attackInfo.Damage;
                                objects.Remove(attackInfo.CollisionItem);
                            }
                                break;
                            }
                    }
                foreach (Hero hero in heros)
                {
                    if (hero.CurrentHealth <= 0.0f)
                    {
                        game_over = true;
                        break;
                    }
                }
                if (game_over)
                { 
                        Message_SceneSwitch mss = new Message_SceneSwitch("Game Over Scene");
                        MessageManager.AddMessageItem(mss);
                }

                foreach (Boss boss in villains)
                {
                    if (boss.CurrentHealth <= 0.0f)
                    {
                        boss.Level += 1;
                        boss.MaxHealth += 100;
                        boss.CurrentHealth = boss.MaxHealth;
                    }

                }
            }
        }

        public override void Reset()
        {
            objects.Clear();
            heros.Clear();
            villains.Clear();
            SetupScene();
        }


        public override bool AddObject(GenericItem gi)
        {
            if (gi is Hero)
            {
                if (null == heros)
                    return false;
                int size_before_add = heros.Count;
                Hero hero = (Hero)gi;
                heros.Add(hero);
                int size_after_add = heros.Count;
                if (size_after_add > size_before_add)
                    return true;
            }
            if (gi is Boss)
            {
                if (null == villains)
                    return false;
                int size_before_add = villains.Count;
                Boss boss = (Boss)gi;
                villains.Add(boss);
                int size_after_add = villains.Count;
                if (size_after_add > size_before_add)
                    return true;
            }
            else
            {
                if (null == objects)
                    return false;

                int size_before_add = objects.Count;
                objects.Add(gi);
                int size_after_add = objects.Count;
                if (size_after_add > size_before_add)
                    return true;
            }
            return false;

        }
        public override void Draw(CanvasDrawingSession cds)
        {

            foreach (GenericItem gi in objects)
            {
                gi.Draw(cds);
            }
            foreach (Hero hero in heros)
            {
                hero.Draw(cds);
            }
            foreach (Boss boss in villains)
            {
                boss.Draw(cds);
            }

        }

        public override void SetupScene()
        {

            Score = 0.0f;
            game_over = false;

            _score_label = new EvilutionLabel("SCORE: " + Score, Colors.White, (uint)(this._width * 0.90f), 100);
            _score_label.Y = 20;
            _score_label.FontSize = 50;
            CenterObject(_score_label, true, false);
            this.AddObject(_score_label);

            Hero hero = new Hero("Hero");
            hero.Location = new System.Numerics.Vector2(700, 700);
            hero.SetBitmapFromImageDictionary("Hero");
            this.AddObject(hero);


            Boss boss = new Boss("Boss");
            boss.Location = new System.Numerics.Vector2(1000, 150);
            boss.SetBitmapFromImageDictionary("Boss");
            this.AddObject(boss);

        }

        //properties

        //Makeing seperate lists for heros/villains
        protected List<Hero> heros = new List<Hero>();
        protected List<Boss> villains = new List<Boss>();
        bool game_over = false;


        private EvilutionLabel _score_label;
        float Score { get; set; }

    }
}
