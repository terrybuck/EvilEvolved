using System;
using Evilution;
using Microsoft.Graphics.Canvas.UI.Xaml;

namespace EvilutionClass
{
    /// <summary>
    /// The timer that dictates the Game Loop.
    /// </summary>
    public class GameTimer
    {
        private bool IsUpdating = false;
        private TimeSpan TotalAppTime;
        private DateTime LastUpdateTime;
        private DateTime LastDrawTime;
        private TimeSpan TimeBetweenDraw;

        /// <summary>
        /// GameTimer constructor, sets default values.
        /// </summary>
        public GameTimer()
        {
            //initialise LastUpdate and LastDraw to current time
            LastUpdateTime = DateTime.Now;
            LastDrawTime = DateTime.Now;
        }

        /// <summary>
        /// Update timer for the game, this should run 120 times a second (default).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnUpdate(ICanvasAnimatedControl sender)
        {
            //find delta t
            var dt = DateTime.Now - LastUpdateTime;

            //Update LastUpdateTime
            LastUpdateTime = DateTime.Now;

            //Update the total app time
            TotalAppTime += dt;

            //get the current input from the input manager
            var gi = InputManager.Update();
            var gm = MessageManager.Update();

            if (StoryBoard.Update(dt, gm))
            {

            }
            else
            {
                if (null != StoryBoard.CurrentScene)
                {
                    StoryBoard.CurrentScene.Update(dt, gi);
                    StoryBoard.CurrentScene.Update(dt, gm);
                    gi = InputManager.PeekAndTake(typeof(MouseGenericInput));
                    while (gi is MouseGenericInput)
                    {
                        StoryBoard.CurrentScene.Update(TimeSpan.Zero, gi);
                        gi = InputManager.PeekAndTake(typeof(MouseGenericInput));
                    }
                    gm = MessageManager.PeekAndTake(typeof(Message_Attack));
                    while (gm is Message_Attack)
                    {
                        StoryBoard.CurrentScene.Update(TimeSpan.Zero, gm);
                        gm = MessageManager.PeekAndTake(typeof(Message_Attack));
                    }                        
                    gi = InputManager.PeekAndTake(typeof(Message_Collision));
                    while (gm is Message_Collision)
                    {
                        StoryBoard.CurrentScene.Update(TimeSpan.Zero, gm);
                        gm = MessageManager.PeekAndTake(typeof(Message_Collision));
                    }
                }
            }

            //Figure out if we need to refresh the screen
            var dt_Draw = DateTime.Now - LastDrawTime;
            if(dt_Draw.Milliseconds > TimeBetweenDraw.Milliseconds)
            {
                LastDrawTime = DateTime.Now;
                
            }            
        }
    }
}
