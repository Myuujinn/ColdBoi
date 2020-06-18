using System;
using Microsoft.Xna.Framework;

namespace ColdBoi.PPU
{
    public class Background
    {
        private const int BACKGROUND_WIDTH = 256;
        private const int BACKGROUND_HEIGHT = 256;
        
        private Graphics graphics;

        public Background(Graphics graphics)
        {
            this.graphics = graphics;
        }
    }
}