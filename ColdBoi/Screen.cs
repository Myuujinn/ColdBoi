using System;
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
            var random = new Random();
            for (var i = 0; i < this.FrameBuffer.Length; i++)
            {
                this.FrameBuffer[i] = new Color(random.Next(256), random.Next(256), random.Next(256));
            }
        }

        public void SetPixel(int x, int y, Color color)
        {
            this.FrameBuffer[x + y * SCREEN_WIDTH] = color;
        }
    }
}