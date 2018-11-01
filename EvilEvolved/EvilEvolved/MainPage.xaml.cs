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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace EvilEvolved
{
    public sealed partial class MainPage : Page
    {
        public bool IsAllImagesLoaded = false;

        GenericScene gs = new GenericScene("test");

        public MainPage()
        {
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
            //add hero sprite to image dictionary
            await ImageManager.AddImage("Hero", @"Assets/imageedit_4_4742766674.gif");

            GenericScene gs = new GenericScene("test");

            Random r = new Random();
            for (int i = 0; i < 1; i++)
            {

                GenericItem gi = new GenericItem("test");
                gi.Location = new System.Numerics.Vector2(r.Next(0, 1000), r.Next(0, 800));
                gi.SetBitmapFromImageDictionary("Hero");
                gs.AddObject(gi);
            }

            EvilutionButton eb = new EvilutionButton("My First Button");

            gs.AddObject(eb);
            eb.Location = new System.Numerics.Vector2(400, 400);

            StoryBoard.AddScene(gs);
            StoryBoard.CurrentScene = gs;
            BitmapSize button_size;
            button_size.Width = 250;
            button_size.Height = 50;
            eb.Size = button_size;

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

            if(null != StoryBoard.CurrentScene)
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
    }
}
