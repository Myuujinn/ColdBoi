using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ColdBoi
{
    public class ColdBoi : Game
    {
        private GameBoy GameBoy { get; set; }
        private GraphicsDeviceManager GraphicsDeviceManager { get; }

        private string romPath;
        private int scale;
        private SpriteBatch spriteBatch;
        private Texture2D pixel;

        public ColdBoi(string romPath, int scale = 2)
        {
            this.romPath = romPath;
            this.scale = scale;

            this.GraphicsDeviceManager = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = Screen.SCREEN_WIDTH * scale,
                PreferredBackBufferHeight = Screen.SCREEN_HEIGHT * scale,
                IsFullScreen = false,
                SynchronizeWithVerticalRetrace = true
            };

            this.IsFixedTimeStep = false;
        }

        protected override void Initialize()
        {
            base.Initialize();

            this.GameBoy = new GameBoy(this.romPath);
            this.Window.Title = $"ColdBoi - {this.GameBoy.Processor.Memory.RomName}";

            this.spriteBatch = new SpriteBatch(this.GraphicsDevice);

            this.pixel = new Texture2D(this.GraphicsDevice, 1, 1);
            this.pixel.SetData(new[] {Color.White});
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            var timeElapsed = gameTime.ElapsedGameTime.TotalSeconds;
            this.GameBoy.Update(timeElapsed);
        }

        protected override void Draw(GameTime gameTime)
        {
            this.GraphicsDevice.Clear(Color.CornflowerBlue);
            this.GraphicsDevice.Textures[0] = null;

            var frameBuffer = this.GameBoy.Screen.FrameBuffer;

            this.spriteBatch.Begin();

            for (var i = 0; i < frameBuffer.Length; i++)
            {
                this.spriteBatch.Draw(pixel,
                    new Vector2(i % Screen.SCREEN_WIDTH * scale, i / Screen.SCREEN_WIDTH * scale),
                    null,
                    frameBuffer[i],
                    0.0f,
                    Vector2.Zero,
                    this.scale,
                    SpriteEffects.None,
                    1.0f);
            }

            this.spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}