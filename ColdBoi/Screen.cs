using Microsoft.Xna.Framework;

namespace ColdBoi
{
    public class Screen
    {
        public const int SCREEN_WIDTH = 160;
        public const int SCREEN_HEIGHT = 144;

        public Color[] FrameBuffer { get; }

        public Screen()
        {
            this.FrameBuffer = new Color[SCREEN_WIDTH * SCREEN_HEIGHT];
        }
    }
}