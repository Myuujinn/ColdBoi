using System;
using Microsoft.Xna.Framework;

namespace ColdBoi
{
    public class Graphics : ITickable
    {
        private const float HORIZONTAL_SYNC = 9198000;
        private const float VERTICAL_SYNC = 59.73f;

        private const ushort GRAPHICS_CONTROL = 0xff40;
        private const ushort LCD_STATUS = 0xff41;
        private const ushort SCROLL_Y = 0xff42;
        private const ushort SCROLL_X = 0xff43;
        public const ushort SCANLINE = 0xff44;

        private const int SCANLINE_CYCLE = 456;
        private const int SCANLINES = 153;
        private const int VBLANK_START = 144;

        private Color[] palette;
        private int scanlineWalker;
        private Memory memory;

        public byte Control
        {
            get => this.memory.Content[GRAPHICS_CONTROL];
            set => this.memory.Write(GRAPHICS_CONTROL, value);
        }

        public byte LcdStatus
        {
            get => this.memory.Content[LCD_STATUS];
            set => this.memory.Write(LCD_STATUS, value);
        }

        public byte ScrollY
        {
            get => this.memory.Content[SCROLL_Y];
            set => this.memory.Write(SCROLL_Y, value);
        }

        public byte ScrollX
        {
            get => this.memory.Content[SCROLL_X];
            set => this.memory.Write(SCROLL_X, value);
        }

        public byte Scanline
        {
            get => this.memory.Content[SCANLINE];
            set => this.memory.Write(SCANLINE, value);
        }

        public bool BgDisplay => (this.Control & 1) > 0;
        public bool ObjDisplay => (this.Control & (1 << 1)) > 0;
        public int ObjSize => (this.Control & (1 << 2)) > 0 ? 8 * 8 : 8 * 16;
        public bool WindowEnable => (this.Control & (1 << 5)) > 0;
        public bool LcdDisplayEnable => (this.Control & (1 << 7)) > 0;

        public enum LcdMode : byte
        {
            HBlank = 0,
            VBlank = 1,
            OamScan = 2,
            HDraw = 3
        }

        public Graphics(Memory memory)
        {
            this.palette = new[] {Color.White, Color.Silver, new Color(96, 96, 96), Color.Black};
            this.scanlineWalker = 0;
            this.memory = memory;
        }

        private void ResetLcd()
        {
            this.scanlineWalker = 0;
            this.memory.Write(SCANLINE, 0);
            this.LcdStatus &= 252;
            this.LcdStatus |= 1;
        }

        public void Tick()
        {
            if (!this.LcdDisplayEnable)
            {
                ResetLcd();
                return;
            }

            this.scanlineWalker += 1;

            if (this.scanlineWalker < SCANLINE_CYCLE)
                return;

            this.scanlineWalker = 0;
            this.Scanline += 1;

            if (this.Scanline == VBLANK_START)
                TriggerVBlank();
            else if (this.Scanline > SCANLINES)
                this.Scanline = 0;
            else if (this.Scanline < VBLANK_START)
                DrawScanline();
        }

        private void TriggerVBlank()
        {
        }

        private void DrawScanline()
        {
            
        }
    }
}