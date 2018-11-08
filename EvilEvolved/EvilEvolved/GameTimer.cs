using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Windows.UI.Xaml;
using Microsoft.Graphics.Canvas.UI.Xaml;

namespace EvilutionClass
{
    public class GameTimer
    {
        public GameTimer(CanvasControl surface = null, int UPS = 60, int FPS = 30)
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

                if (StoryBoard.Update(dt, gi))
                {

                }
                else
                {
                    if (null != StoryBoard.CurrentScene)
                    {
                        StoryBoard.CurrentScene.Update(dt, gi);
                        gi = InputManager.PeekAndTake(typeof(MouseGenericInput));
                        while (gi is MouseGenericInput)
                        {
                            StoryBoard.CurrentScene.Update(TimeSpan.Zero, gi);
                            gi = InputManager.PeekAndTake(typeof(MouseGenericInput));
                        }
                        gi = InputManager.PeekAndTake(typeof(Message_Attack));
                        while (gi is Message_Attack)
                        {
                            StoryBoard.CurrentScene.Update(TimeSpan.Zero, gi);
                            gi = InputManager.PeekAndTake(typeof(Message_Attack));
                        }                        
                        gi = InputManager.PeekAndTake(typeof(Message_Collision));
                        while (gi is Message_Collision)
                        {
                            StoryBoard.CurrentScene.Update(TimeSpan.Zero, gi);
                            gi = InputManager.PeekAndTake(typeof(Message_Collision));
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

        //Properties
        public bool IsUpdating = false;

        private DispatcherTimer UpdateTimer;

        public TimeSpan TotalAppTime { get; set; }

        public DateTime LastUpdateTime { get; set; }

        public DateTime LastDrawTime { get; set; }

        public TimeSpan TimeBetweenDraw { get; set; }

        public CanvasControl ParentCanvas { get; set; }


    }
}
