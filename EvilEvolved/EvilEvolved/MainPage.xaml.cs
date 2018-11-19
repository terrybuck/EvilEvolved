using System.Numerics;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Microsoft.Graphics.Canvas.UI.Xaml;
using Windows.UI.Xaml.Input;
using EvilutionClass;
using Windows.System;
using Windows.UI.Text;
using System;
using System.Threading.Tasks;
using Windows.UI;
using static EvilutionClass.GameOverScene;
using static EvilutionClass.GameScene;
using static EvilutionClass.Hero;
using Windows.Storage;

namespace Evilution
{
    public sealed partial class MainPage : Page
    {
        private readonly GameTimer _gameTimer;
        private Vector2 _screenSize;

        public MainPage()
        {
            Focus(FocusState.Programmatic);
            this.InitializeComponent();

            _gameTimer = new GameTimer();
        }

        private void OnGameLoopStarting(ICanvasAnimatedControl sender, object args)
        {

        }

        private async Task<string> InputTextDialogAsync(string title)
        {
            TextBox inputTextBox = new TextBox();
            inputTextBox.AcceptsReturn = false;
            inputTextBox.Height = 32;
            ContentDialog dialog = new ContentDialog();
            dialog.Content = inputTextBox;
            dialog.Title = title;
            dialog.IsSecondaryButtonEnabled = true;
            dialog.PrimaryButtonText = "Ok";
            dialog.SecondaryButtonText = "Cancel";
            if (await dialog.ShowAsync() == ContentDialogResult.Primary)
                return inputTextBox.Text;
            else
                return "";
        }

        /// <summary>
        /// Assets are loaded in from the CreateResources method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private async void OnCreateResources(CanvasAnimatedControl sender, Microsoft.Graphics.Canvas.UI.CanvasCreateResourcesEventArgs args)
        {

            _screenSize = new Vector2((float)Cvs.Size.Width, (float)Cvs.Size.Height);

            //set parent canvas for image manager
            ImageManager.ParentCanvas = sender;

            //Animated Hero Images
            await ImageManager.AddImage("Hero_Up_1", @"Assets/Hero_Up_1.gif");
            await ImageManager.AddImage("Hero_Right_1", @"Assets/Hero_Right_1.gif");
            await ImageManager.AddImage("Hero_Left_1", @"Assets/Hero_Left_1.gif");
            await ImageManager.AddImage("Hero", @"Assets/Hero.gif");

            //Minion Images
            await ImageManager.AddImage("MinionLeft", @"Assets/MinionLeft.png");
            await ImageManager.AddImage("MinionRight", @"Assets/MinionRight.png");

            //
            await ImageManager.AddImage("Arrow", @"Assets/Arrow.png");
            await ImageManager.AddImage("Boss", @"Assets/BossEdit.png");
            await ImageManager.AddImage("BossHurt", @"Assets/Boss_Hurt.png");
            await ImageManager.AddImage("Title", @"Assets/Evilution.png");
            await ImageManager.AddImage("GameOver", @"Assets/GameOver.png");

            await AudioManager.AddAudio("Generic Title Scene", "TitleTheme.mp3");
            await AudioManager.AddAudio("Main Game Scene", "BattleTheme.mp3");
            await AudioManager.AddAudio("Game Over Scene", "GameOver.mp3");

            Storage.CreateFile();
            Storage.ReadFile();
            storageFile = await Storage.Storage_Folder.GetFileAsync("highscores.txt");

            // set up the scene
            var ts = new TitleScene((int)this._screenSize.X, (int)this._screenSize.Y);
            StoryBoard.AddScene(ts);
            StoryBoard.CurrentScene = ts;

            //create scenes
            var game_scene = new GameScene((int)this._screenSize.X, (int)this._screenSize.Y);
            var game_over_scene = new GameOverScene((int)this._screenSize.X, (int)this._screenSize.Y);
            var top_score_scene = new HighScoreScene((int)this._screenSize.X, (int)this._screenSize.Y);

            //add scenes to storyboard
            StoryBoard.AddScene(game_scene);
            StoryBoard.AddScene(game_over_scene);
            StoryBoard.AddScene(top_score_scene);

        }

        private void OnUpdate(ICanvasAnimatedControl sender, CanvasAnimatedUpdateEventArgs args)
        {
            this._gameTimer.OnUpdate(sender);
        }

        /// <summary>
        /// Draw function
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        void OnDraw(ICanvasAnimatedControl sender, CanvasAnimatedDrawEventArgs args)
        {
            // get the drawing session
            var cds = args.DrawingSession;

            if (null != StoryBoard.CurrentScene)
            {
                StoryBoard.CurrentScene.Draw(cds);
            }

        }

        private void OnGameLoopStopped(ICanvasAnimatedControl sender, object args)
        {

        }

        //private async void OnGameOver(object sender, GameOverEvent args)
        //{
        //    string text = await InputTextDialogAsync("You've got a high score, please enter your name:");
        //}

        #region ----------[Mouse inputs]

        private void CanvasControl_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
            var p = e.GetCurrentPoint((UIElement)sender);

            var mgi = new MouseGenericInput((float)p.Position.X, (float)p.Position.Y);
            mgi.Name = "mouse_move";
            mgi.MouseInputType = MouseGenericInput.MouseGenericInputType.MouseMove;
            mgi.IsLeftButtonPress = p.Properties.IsLeftButtonPressed;
            mgi.IsMiddleButtonPress = p.Properties.IsMiddleButtonPressed;
            mgi.IsRightButtonPress = p.Properties.IsRightButtonPressed;
            mgi.MouseDown = mgi.IsRightButtonPress | mgi.IsMiddleButtonPress | mgi.IsLeftButtonPress;

            InputManager.AddInputItem(mgi);

        }

        private void CanvasControl_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            e.Handled = true;
            

            var p = e.GetCurrentPoint((UIElement)sender);

            var mgi = new MouseGenericInput((float)p.Position.X, (float)p.Position.Y);
            mgi.Name = "mouse_down";
            mgi.MouseInputType = MouseGenericInput.MouseGenericInputType.MousePressed;
            mgi.IsLeftButtonPress = p.Properties.IsLeftButtonPressed;
            mgi.IsMiddleButtonPress = p.Properties.IsMiddleButtonPressed;
            mgi.IsRightButtonPress = p.Properties.IsRightButtonPressed;
            mgi.MouseDown = mgi.IsRightButtonPress | mgi.IsMiddleButtonPress | mgi.IsLeftButtonPress;

            InputManager.AddInputItem(mgi);
        }

        private void CanvasControl_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            var p = e.GetCurrentPoint((UIElement)sender);

            var mgi = new MouseGenericInput((float)p.Position.X, (float)p.Position.Y);
            mgi.Name = "mouse_up";
            mgi.MouseInputType = MouseGenericInput.MouseGenericInputType.MouseReleased;
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

            var gki = new GenericKeyboardInput();

            var CheckW = Windows.UI.Core.CoreWindow.GetForCurrentThread().GetAsyncKeyState(VirtualKey.W).HasFlag(Windows.UI.Core.CoreVirtualKeyStates.Down);
            var CheckA = Windows.UI.Core.CoreWindow.GetForCurrentThread().GetAsyncKeyState(VirtualKey.A).HasFlag(Windows.UI.Core.CoreVirtualKeyStates.Down);
            var CheckS = Windows.UI.Core.CoreWindow.GetForCurrentThread().GetAsyncKeyState(VirtualKey.S).HasFlag(Windows.UI.Core.CoreVirtualKeyStates.Down);
            var CheckD = Windows.UI.Core.CoreWindow.GetForCurrentThread().GetAsyncKeyState(VirtualKey.D).HasFlag(Windows.UI.Core.CoreVirtualKeyStates.Down);

            gki.IsWKeyPress = CheckW;
            gki.IsAKeyPress = CheckA;
            gki.IsSKeyPress = CheckS;
            gki.IsDKeyPress = CheckD;
            InputManager.AddInputItem(gki);

        }

        private void CanvasControl_KeyUp(object sender, KeyRoutedEventArgs e)
        {
            e.Handled = true;

            var gki = new GenericKeyboardInput();


            var CheckW = Windows.UI.Core.CoreWindow.GetForCurrentThread().GetAsyncKeyState(VirtualKey.W).HasFlag(Windows.UI.Core.CoreVirtualKeyStates.Down);
            var CheckA = Windows.UI.Core.CoreWindow.GetForCurrentThread().GetAsyncKeyState(VirtualKey.A).HasFlag(Windows.UI.Core.CoreVirtualKeyStates.Down);
            var CheckS = Windows.UI.Core.CoreWindow.GetForCurrentThread().GetAsyncKeyState(VirtualKey.S).HasFlag(Windows.UI.Core.CoreVirtualKeyStates.Down);
            var CheckD = Windows.UI.Core.CoreWindow.GetForCurrentThread().GetAsyncKeyState(VirtualKey.D).HasFlag(Windows.UI.Core.CoreVirtualKeyStates.Down);

            gki.IsWKeyPress = CheckW;
            gki.IsAKeyPress = CheckA;
            gki.IsSKeyPress = CheckS;
            gki.IsDKeyPress = CheckD;
            InputManager.AddInputItem(gki);

        }

        #endregion

        //HighScore Variables
        //public static float HighScore;
        public static string STRHighScore;
        StorageFile storageFile;

    }
}
