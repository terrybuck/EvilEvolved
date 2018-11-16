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
using Windows.Graphics.Imaging;
using Microsoft.Graphics.Canvas.Effects;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Shapes;

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
        /// Update the scene based on inputs
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
            foreach (Villain villain in villains)
            {
                villain.Update(dt, input);
                //reset the bosses hit image to default image after 300ms of no hits
                if (villain is Boss)
                {
                    villain.TimeSinceCollision = DateTime.Now - villain.LastCollision;
                    if (villain.TimeSinceCollision.TotalMilliseconds > 300 && villain.HurtImage)
                    {
                        villain.SetBitmapFromImageDictionary("Boss");
                    }
                }
                //update the hero's location to each villain
                foreach (Hero hero in heros)
                {
                    villain.HeroLocation = hero.Location;
                }
            }
            foreach (GenericItem gi in objects)
            {
                //update each generic item
                gi.Update(dt, input);

                //find out if any of the attacks hit the villains or hero
                if (gi is Attack)
                {
                    Attack attack = (Attack)gi;
                    switch (attack.Type)
                    {
                        case (Attack.AttackType.Hero_Arrow):
                            {
                                foreach (Villain villain in villains)
                                {
                                    if (RectHelper.Intersect(villain.BoundingRectangle, gi.BoundingRectangle) != Rect.Empty)
                                    {
                                        villain.TimeSinceCollision = DateTime.Now - villain.LastCollision;
                                        if (villain.TimeSinceCollision.TotalMilliseconds > villain.iFrames)
                                        {
                                            Message_Collision villain_collision = new Message_Collision("Arrow", gi, villain);
                                            MessageManager.AddMessageItem(villain_collision);
                                            villain.LastCollision = DateTime.Now;
                                            if (villain is Boss)
                                            {
                                                villain.SetBitmapFromImageDictionary("BossHurt");
                                                villain.HurtImage = true;
                                            }
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
                                            Message_Collision hero_collision = new Message_Collision("Arrow", gi, hero);
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

        /// <summary>
        /// Updates the scene based on incoming messages
        /// </summary>
        /// <param name="dt">Delta time since thew last update was called</param>
        /// <param name="message"> incoming message</param>
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
            foreach (Villain villain in villains)
            {
                //update each generic item
                villain.Update(dt, message);
            }

            if (message is Message_Attack)
            {
                Message_Attack mhe = (Message_Attack)message;

                switch (mhe.Type)
                {
                    case (Message_Attack.AttackType.Hero_Arrow):
                        {
                            //(Terry)TODO: add rotation to arrows, this might require using something other than CanvasBitmap images
                            Attack attack = new Attack(mhe.Name, mhe.DirectionX, mhe.DirectionY, mhe.Location, Attack.AttackType.Hero_Arrow, mhe.Range, mhe.Damage);
                            attack.SetBitmapFromImageDictionary("Arrow");
                            this.AddObject(attack);
                            break;
                        }
                    case (Message_Attack.AttackType.Minion_Arrow):
                        {
                            Attack arrow = new Attack(mhe.Name, mhe.DirectionX, mhe.DirectionY, mhe.Location, Attack.AttackType.Boss_Arrow, mhe.Range, mhe.Damage);
                            arrow.SetBitmapFromImageDictionary("Arrow");
                            this.AddObject(arrow);
                            break;
                        }
                }
            }

            //if two generic items on the canvas have collided find out what collided and apply aprpriate damage
            if (message is Message_Collision)
            {
                Message_Collision attackInfo = (Message_Collision)message;

                if (attackInfo.CollisionObject is Attack)
                {
                    Attack attack = (Attack)attackInfo.CollisionObject;

                    //if the boss is hit by an attack, increase score, reduce bosses health and remove the attack
                    if (attackInfo.Victim is Boss)
                    {
                        Boss boss = (Boss)attackInfo.Victim;
                            boss.CurrentHealth -= attack.Damage;
                            Score += attack.Damage;
                            _score_label.UpdateText("SCORE: " + Score);
                            objects.Remove(attackInfo.CollisionObject);
                      
                        if (boss.CurrentHealth <= 0.0f)
                        {
                            boss.Level += 1;
                            boss.MaxHealth += 100;
                            boss.CurrentHealth = boss.MaxHealth;
                        }

                    }
                    // if the attack hit a minion, remove the attack and the minion
                    else if (attackInfo.Victim is Villain)
                    {
                        Villain villain = (Villain)attackInfo.Victim;
                        villains.Remove(villain);
                        objects.Remove(attackInfo.CollisionObject);
                    }
                    //if the attack hit the hero, reduce hero's health and check for game over
                    else if (attackInfo.Victim is Hero)
                    {
                        Hero hero = (Hero)attackInfo.Victim;
                        hero.CurrentHealth -= attack.Damage;
                        objects.Remove(attackInfo.CollisionObject);
                        if (hero.CurrentHealth <= 0)
                        {
                            heros.Remove(hero);
                            //add tombstone image where hero died?
                            // If all heroes (spelled correctly this time) have died it's game over
                            if (heros.Count <= 0)
                            {
                                game_over = true;
                            }
                        }
                    }
                }
            }            
            // switch to game over scene
            if (game_over)
            {
                Message_SceneSwitch mss = new Message_SceneSwitch("Game Over Scene");
                MessageManager.AddMessageItem(mss);
            }

            if(message is Message_SpawnMinions)
            {
                Message_SpawnMinions spawn = (Message_SpawnMinions)message;
                Random r = new Random();
                for (int i = 0; i < spawn.NumberOfMinions; i++)
                {
                    Villain villain = new Villain("minion");
                    villain.Location = new System.Numerics.Vector2(r.Next(0, 1000), r.Next(0, 800));
                    villain.SetBitmapFromImageDictionary("MinionLeft");
                    this.AddObject(villain);
                }
            }
        }
        
        /// <summary>
        /// Reset clears all items and sets up the scene again 
        /// </summary>
        public override void Reset()
        {
            objects.Clear();
            heros.Clear();
            villains.Clear();
            SetupScene();
        }

        /// <summary>
        /// Adds a generic item to the appropriate list so that id=t can be displayed and interacted with on screne
        /// </summary>
        /// <param name="gi"> generic item to add to scene </param>
        /// <returns></returns>
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
            if (gi is Villain)
            {
                if (null == villains)
                    return false;
                int size_before_add = villains.Count;
                Villain villain = (Villain)gi;
                villains.Add(villain);
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
        /// <summary>
        /// Draws all the items to the canvas
        /// </summary>
        /// <param name="cds"></param>
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
            foreach (Villain villain in villains)
            {
                villain.Draw(cds);
            }

        }

        /// <summary>
        /// builds the initial scene
        /// </summary>
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

        //Making seperate lists for heros/villains
        protected List<Hero> heros = new List<Hero>();
        protected List<Villain> villains = new List<Villain>();

        bool game_over = false; //(Terry)TODO: I might make game over an event handler in the hero class?
        private EvilutionLabel _score_label;

    }

}
