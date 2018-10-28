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

using Evilution;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace EvilEvolved
{
    public sealed partial class MainPage : Page
    {
        public bool IsAllImagesLoaded = false;

        List<GenericItem> objects = new List<GenericItem>();

        public MainPage()
        {
            this.InitializeComponent();
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

            if (IsAllImagesLoaded)
            {
                foreach (GenericItem gi in objects)
                {
                    gi.Draw(cds);
                }
            }
        }

        /// <summary>
        /// Assets are loaded in from the CreateResources method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private async void CanvasControl_CreateResources(CanvasControl sender, Microsoft.Graphics.Canvas.UI.CanvasCreateResourcesEventArgs args)
        {

            //set parent canvas for image manager
            Manage_Imported_Images.ParentCanvas = sender;
            //add hero sprite to image dictionary
            await Manage_Imported_Images.AddImage("Hero", @"Assets/amg1_fr2.gif");

            Random r = new Random();
            for (int i = 0; i < 50; i++)
            {

                GenericItem gi = new GenericItem("test");
                gi.Location = new System.Numerics.Vector2(r.Next(0, 1000), r.Next(0, 800));
                gi.SetBitmapFromImageDictionary("Hero");
                objects.Add(gi);
            }

            
            IsAllImagesLoaded = true;
            //indicate the canvas content needs to be redrawn
            sender.Invalidate();
        }
    }
}
