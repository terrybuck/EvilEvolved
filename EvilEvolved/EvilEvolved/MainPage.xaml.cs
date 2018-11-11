using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Microsoft.Graphics.Canvas.UI.Xaml;
using Microsoft.Graphics.Canvas;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

using EvilutionClass;
using Windows.Graphics.Imaging;
using Windows.System;
using static EvilutionClass.MouseGenericInput;
using static EvilutionClass.GenericKeyboardInput;
using Windows.Media.Playback;
using Windows.Media.Core;

namespace Evilution
{
    public sealed partial class MainPage : Page
    {
        public bool IsAllImagesLoaded = false;

        public MainPage()
        {
            Focus(FocusState.Programmatic);
            this.InitializeComponent();
        }

        /// <summary>
        /// Assets are loaded in from the CreateResources method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private async void CanvasControl_CreateResources(CanvasControl sender, Microsoft.Graphics.Canvas.UI.CanvasCreateResourcesEventArgs args)
        {

            //set parent canvas for image manager
            ImageManager.ParentCanvas = sender;




            #region -------[Load images]

            await ImageManager.AddImage("Hero", @"Assets/Hero.gif");
            await ImageManager.AddImage("Arrow", @"Assets/Arrow.png");
            await ImageManager.AddImage("Boss", @"Assets/BossEdit.png");
            await ImageManager.AddImage("Title", @"Assets/Evilution.png");
            await ImageManager.AddImage("GameOver", @"Assets/GameOver.png");

            #endregion

            #region -------[Load audio]

            await AudioManager.AddAudio("Generic Title Scene", "TitleTheme.mp3");
            await AudioManager.AddAudio("Main Game Scene", "BattleTheme.mp3");
            await AudioManager.AddAudio("Game Over Scene", "GameOver.mp3");

            #endregion

            // set scene
            CanvasControl cc = sender;
            TitleScene ts = new TitleScene((int)cc.RenderSize.Width, (int)cc.RenderSize.Height);
            StoryBoard.AddScene(ts);
            StoryBoard.CurrentScene = ts;

            //create scenes
            GameScene game_scene = new GameScene((int)cc.RenderSize.Width, (int)cc.RenderSize.Height);

            GameOverScene game_over_scene = new GameOverScene((int)cc.RenderSize.Width, (int)cc.RenderSize.Height);

            //add scenes to storyboard
            StoryBoard.AddScene(game_scene);
            StoryBoard.AddScene(game_over_scene);

            IsAllImagesLoaded = true;

            GameTimer gt = new GameTimer(sender, 60, 30);

        }

        /// <summary>
        /// Draw function
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        void CanvasControl_Draw(CanvasControl sender, CanvasDrawEventArgs args)
        {
            // get the drawing session
            CanvasDrawingSession cds = args.DrawingSession;

            if (null != StoryBoard.CurrentScene)
            {
                StoryBoard.CurrentScene.Draw(cds);
            }
        }

        #region ----------[Mouse inputs]

        private void CanvasControl_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
            Windows.UI.Input.PointerPoint p = e.GetCurrentPoint((UIElement)sender);

            MouseGenericInput mgi = new MouseGenericInput((float)p.Position.X, (float)p.Position.Y);
            mgi.Name = "mouse_move";
            mgi.MouseInputType = MouseGenericInputType.MouseMove;
            mgi.IsLeftButtonPress = p.Properties.IsLeftButtonPressed;
            mgi.IsMiddleButtonPress = p.Properties.IsMiddleButtonPressed;
            mgi.IsRightButtonPress = p.Properties.IsRightButtonPressed;
            mgi.MouseDown = mgi.IsRightButtonPress | mgi.IsMiddleButtonPress | mgi.IsLeftButtonPress;

            InputManager.AddInputItem(mgi);

        }

        private void CanvasControl_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            e.Handled = true;
            

            Windows.UI.Input.PointerPoint p = e.GetCurrentPoint((UIElement)sender);

            MouseGenericInput mgi = new MouseGenericInput((float)p.Position.X, (float)p.Position.Y);
            mgi.Name = "mouse_down";
            mgi.MouseInputType = MouseGenericInputType.MousePressed;
            mgi.IsLeftButtonPress = p.Properties.IsLeftButtonPressed;
            mgi.IsMiddleButtonPress = p.Properties.IsMiddleButtonPressed;
            mgi.IsRightButtonPress = p.Properties.IsRightButtonPressed;
            mgi.MouseDown = mgi.IsRightButtonPress | mgi.IsMiddleButtonPress | mgi.IsLeftButtonPress;

            InputManager.AddInputItem(mgi);
        }

        private void CanvasControl_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            Windows.UI.Input.PointerPoint p = e.GetCurrentPoint((UIElement)sender);

            MouseGenericInput mgi = new MouseGenericInput((float)p.Position.X, (float)p.Position.Y);
            mgi.Name = "mouse_up";
            mgi.MouseInputType = MouseGenericInputType.MouseReleased;
            mgi.IsLeftButtonPress = p.Properties.IsLeftButtonPressed;
            mgi.IsMiddleButtonPress = p.Properties.IsMiddleButtonPressed;
            mgi.IsRightButtonPress = p.Properties.IsRightButtonPressed;
            mgi.MouseDown = mgi.IsRightButtonPress | mgi.IsMiddleButtonPress | mgi.IsLeftButtonPress;

            InputManager.AddInputItem(mgi);
        }


        #endregion

        #region ----------[Keyboard inputs]

        private void CanvasControl_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            e.Handled = true;

            GenericKeyboardInput gki = new GenericKeyboardInput();

            bool CheckW = Windows.UI.Core.CoreWindow.GetForCurrentThread().GetAsyncKeyState(VirtualKey.W).HasFlag(Windows.UI.Core.CoreVirtualKeyStates.Down);
            bool CheckA = Windows.UI.Core.CoreWindow.GetForCurrentThread().GetAsyncKeyState(VirtualKey.A).HasFlag(Windows.UI.Core.CoreVirtualKeyStates.Down);
            bool CheckS = Windows.UI.Core.CoreWindow.GetForCurrentThread().GetAsyncKeyState(VirtualKey.S).HasFlag(Windows.UI.Core.CoreVirtualKeyStates.Down);
            bool CheckD = Windows.UI.Core.CoreWindow.GetForCurrentThread().GetAsyncKeyState(VirtualKey.D).HasFlag(Windows.UI.Core.CoreVirtualKeyStates.Down);

            gki.IsWKeyPress = CheckW;
            gki.IsAKeyPress = CheckA;
            gki.IsSKeyPress = CheckS;
            gki.IsDKeyPress = CheckD;
            InputManager.AddInputItem(gki);

        }
        private void CanvasControl_KeyUp(object sender, KeyRoutedEventArgs e)
        {
            e.Handled = true;

            GenericKeyboardInput gki = new GenericKeyboardInput();


            bool CheckW = Windows.UI.Core.CoreWindow.GetForCurrentThread().GetAsyncKeyState(VirtualKey.W).HasFlag(Windows.UI.Core.CoreVirtualKeyStates.Down);
            bool CheckA = Windows.UI.Core.CoreWindow.GetForCurrentThread().GetAsyncKeyState(VirtualKey.A).HasFlag(Windows.UI.Core.CoreVirtualKeyStates.Down);
            bool CheckS = Windows.UI.Core.CoreWindow.GetForCurrentThread().GetAsyncKeyState(VirtualKey.S).HasFlag(Windows.UI.Core.CoreVirtualKeyStates.Down);
            bool CheckD = Windows.UI.Core.CoreWindow.GetForCurrentThread().GetAsyncKeyState(VirtualKey.D).HasFlag(Windows.UI.Core.CoreVirtualKeyStates.Down);

            gki.IsWKeyPress = CheckW;
            gki.IsAKeyPress = CheckA;
            gki.IsSKeyPress = CheckS;
            gki.IsDKeyPress = CheckD;
            InputManager.AddInputItem(gki);

        }

        #endregion
    }
}
