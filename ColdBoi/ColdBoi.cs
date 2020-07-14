using System;
using System.IO;
using ColdBoi.Interface;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Myra;
using Myra.Graphics2D.UI;
using Myra.Graphics2D.UI.File;

namespace ColdBoi
{
    public class ColdBoi : Game
    {
        private GameBoy GameBoy { get; set; }
        private GraphicsDeviceManager GraphicsDeviceManager { get; }

        private string romPath;
        private int scale;

        private UI ui;
        private Desktop desktop;

        private SpriteBatch spriteBatch;
        private Texture2D pixel;

        private FileStream logFile;
        private StreamWriter logStream;

        public ColdBoi(string romPath, int scale)
        {
            this.romPath = romPath;
            this.scale = scale;

            this.GraphicsDeviceManager = new GraphicsDeviceManager(this)
            {
                IsFullScreen = false,
                SynchronizeWithVerticalRetrace = true
            };

            this.IsFixedTimeStep = true;
            this.IsMouseVisible = true;

            this.logFile = new FileStream("log.txt", FileMode.Create);
            this.logStream = new StreamWriter(this.logFile);
            Console.SetOut(this.logStream);
        }

        private void OpenRom(object sender, EventArgs eventArgs)
        {
            var dlg = new FileDialog(FileDialogMode.OpenFile)
            {
                Filter = "*.gb|*.gbc"
            };

            if (!string.IsNullOrEmpty(romPath))
            {
                dlg.FilePath = romPath;
            }

            dlg.Closed += (s, a) =>
            {
                if (!dlg.Result)
                {
                    return;
                }

                var filePath = dlg.FilePath;
                if (string.IsNullOrEmpty(filePath))
                {
                    return;
                }

                this.romPath = filePath;
                this.Initialize();
            };

            dlg.ShowModal(desktop);
        }

        private void Quit(object sender, EventArgs genericEventArgs)
        {
            Exit();
        }

        protected override void Initialize()
        {
            this.GameBoy = new GameBoy(this.romPath);
            this.Window.Title = $"ColdBoi - {this.GameBoy.Processor.Memory.RomName}";

            this.spriteBatch = new SpriteBatch(this.GraphicsDevice);

            this.pixel = new Texture2D(this.GraphicsDevice, 1, 1);
            this.pixel.SetData(new[] {Color.White});

            base.Initialize();
        }

        protected override void LoadContent()
        {
            LoadUi();
        }

        private void LoadUi()
        {
            MyraEnvironment.Game = this;

            this.ui = new UI();
            this.ui.menuItemOpen.Selected += this.OpenRom;
            this.ui.menuItemQuit.Selected += this.Quit;

            this.desktop = new Desktop
            {
                Root = this.ui
            };

            this.ui.LayoutUpdated += (sender, args) =>
            {
                UpdateWindow();
            };
        }

        private void UpdateWindow()
        {
            this.GraphicsDeviceManager.PreferredBackBufferWidth = Screen.SCREEN_WIDTH * scale;
            this.GraphicsDeviceManager.PreferredBackBufferHeight =
                Screen.SCREEN_HEIGHT * scale + this.ui.MenuHeight;
            
            this.GraphicsDeviceManager.ApplyChanges();
        }

        protected override void UnloadContent()
        {
            this.logStream.Close();
        }

        protected override void Update(GameTime gameTime)
        {
            this.GameBoy.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            this.GraphicsDevice.Clear(Color.CornflowerBlue);
            this.GraphicsDevice.Textures[0] = null;

            var frameBuffer = this.GameBoy.Screen.FrameBuffer;
            var yOffset = this.ui.MenuHeight;

            this.spriteBatch.Begin();

            for (var i = 0; i < frameBuffer.Length; i++)
            {
                this.spriteBatch.Draw(pixel,
                    new Vector2(i % Screen.SCREEN_WIDTH * scale, i / Screen.SCREEN_WIDTH * scale + yOffset),
                    null,
                    frameBuffer[i],
                    0.0f,
                    Vector2.Zero,
                    this.scale,
                    SpriteEffects.None,
                    1.0f);
            }

            this.spriteBatch.End();

            desktop.Render();

            base.Draw(gameTime);
        }
    }
}