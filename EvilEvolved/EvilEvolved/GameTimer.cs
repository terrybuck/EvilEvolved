using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Windows.UI.Xaml;
using Microsoft.Graphics.Canvas.UI.Xaml;

namespace EvilutionClass
{
    /// <summary>
    /// The timer that dictates the Game Loop.
    /// </summary>
    public class GameTimer
    {
        /// <summary>
        /// GameTimer constructor, sets default values.
        /// </summary>
        public GameTimer(CanvasControl surface = null, int UPS = 120, int FPS = 60)
        {

            UpdateTimer = new DispatcherTimer();
            UpdateTimer.Interval = TimeSpan.FromMilliseconds(1000 / UPS); //60ups
            UpdateTimer.Tick += UpdateTimer_Tick;

            //set FPS
            TimeBetweenDraw = TimeSpan.FromMilliseconds(1000 / FPS); //30fps

            //initialise LastUpdate and LastDraw to current time
            LastUpdateTime = DateTime.Now;
            LastDrawTime = DateTime.Now;

            ParentCanvas = surface;

            UpdateTimer.Start();

        }

        /// <summary>
        /// Update timer for the game, this should run 120 times a second (default).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void UpdateTimer_Tick(object sender, object e)
        {
            if (!IsUpdating)
            {
                IsUpdating = true;
                //find delta t
                TimeSpan dt = DateTime.Now - LastUpdateTime;

                //Update LastUpdateTime
                LastUpdateTime = DateTime.Now;

                //Update the total app time
                TotalAppTime += dt;

                //get the current input from the input manager
                GenericInput gi = InputManager.Update();
                GenericMessage gm = MessageManager.Update();

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
                TimeSpan dt_Draw = DateTime.Now - LastDrawTime;
                if(dt_Draw.Milliseconds > TimeBetweenDraw.Milliseconds)
                {
                    //call refresh
                    if(null != ParentCanvas)
                    {
                        ParentCanvas.Invalidate();
                        LastDrawTime = DateTime.Now;
                    }
                    
                }

                IsUpdating = false;
            }
        }

        #region -----[Properties]

        private bool IsUpdating = false;
        private DispatcherTimer UpdateTimer;
        private TimeSpan TotalAppTime;
        private DateTime LastUpdateTime;
        private DateTime LastDrawTime;
        private TimeSpan TimeBetweenDraw;
        private CanvasControl ParentCanvas;

        #endregion
    }
}
