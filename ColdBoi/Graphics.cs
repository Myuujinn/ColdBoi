using System;
using ColdBoi.PPU;
using Microsoft.Xna.Framework;

namespace ColdBoi
{
    public class Graphics : ITickable
    {
        public enum Mode : byte
        {
            HBlank = 0,
            VBlank = 1,
            OamScan = 2,
            HDraw = 3
        }

        public const ushort VRAM_START = 0x8000;
        private const ushort GRAPHICS_CONTROL = 0xff40;
        private const ushort LCD_STATUS = 0xff41;
        private const ushort SCROLL_Y = 0xff42;
        private const ushort SCROLL_X = 0xff43;
        public const ushort SCANLINE = 0xff44;
        private const ushort LCD_Y_COMPARE = 0xff45;

        private const int SCANLINE_CYCLE = 456;
        private const int MODE_2_BOUNDS = SCANLINE_CYCLE - 80;
        private const int MODE_3_BOUNDS = SCANLINE_CYCLE - 172;
        private const int SCANLINES = 153;
        private const int VBLANK_START = 144;

        private Color[] palette;
        private int scanlineWalker;

        private readonly Processor processor;
        private readonly Screen screen;
        private Memory Memory => this.processor.Memory;

        public byte Control
        {
            get => this.Memory.Content[GRAPHICS_CONTROL];
            set => this.Memory.Write(GRAPHICS_CONTROL, value);
        }

        public byte LcdStatus
        {
            get => this.Memory.Content[LCD_STATUS];
            set => this.Memory.Write(LCD_STATUS, value);
        }

        public byte ScrollY
        {
            get => this.Memory.Content[SCROLL_Y];
            set => this.Memory.Write(SCROLL_Y, value);
        }

        public byte ScrollX
        {
            get => this.Memory.Content[SCROLL_X];
            set => this.Memory.Write(SCROLL_X, value);
        }

        public byte Scanline
        {
            get => this.Memory.Content[SCANLINE];
            set => this.Memory.Content[SCANLINE] = value; // can't use memory.write because it will be intercepted
        }

        public byte YCompare
        {
            get => this.Memory.Content[LCD_Y_COMPARE];
            set => this.Memory.Write(LCD_Y_COMPARE, value);
        }

        public bool BgDisplay => Bit.IsSet(this.Control, 0);
        public bool ObjDisplay => Bit.IsSet(this.Control, 1);
        public int ObjSize => Bit.IsSet(this.Control, 2) ? 8 * 8 : 8 * 16;
        public bool WindowEnable => Bit.IsSet(this.Control, 5);
        public bool LcdDisplayEnable => Bit.IsSet(this.Control, 7);

        private const byte HBLANK_INTERRUPT_ENABLE_BIT = 3;
        private const byte VBLANK_INTERRUPT_ENABLE_BIT = 4;
        private const byte OAM_SCAN_INTERRUPT_ENABLE_BIT = 5;
        private const byte Y_COMPARE_INTERRUPT_ENABLE_BIT = 6;

        private bool HBlankInterruptEnabled => Bit.IsSet(this.LcdStatus, HBLANK_INTERRUPT_ENABLE_BIT);
        private bool VBlankInterruptEnabled => Bit.IsSet(this.LcdStatus, VBLANK_INTERRUPT_ENABLE_BIT);
        private bool OamScanInterruptEnabled => Bit.IsSet(this.LcdStatus, OAM_SCAN_INTERRUPT_ENABLE_BIT);
        private bool YCompareInterruptEnabled => Bit.IsSet(this.LcdStatus, Y_COMPARE_INTERRUPT_ENABLE_BIT);


        private Mode LcdMode
        {
            get => (Mode) (this.LcdStatus & 0x3);
            set
            {
                this.LcdStatus = Bit.Set(this.LcdStatus, 0, false);
                this.LcdStatus = Bit.Set(this.LcdStatus, 1, false);
                this.LcdStatus |= (byte) value;
            }
        }

        public Graphics(Processor processor, Screen screen)
        {
            this.processor = processor;
            this.screen = screen;

            this.palette = new[] {Color.White, Color.Silver, new Color(96, 96, 96), Color.Black};
            this.scanlineWalker = 0;
        }

        private void ResetLcd()
        {
            this.scanlineWalker = SCANLINE_CYCLE;
            this.Scanline = 0;
            this.LcdStatus &= 0xfc;
            this.LcdMode = Mode.VBlank;
        }

        private void UpdateLcd()
        {
            if (!this.LcdDisplayEnable)
            {
                ResetLcd();
                return;
            }

            Mode mode;
            var status = this.LcdStatus;
            var scanline = this.Scanline;
            var requestInterrupt = false;

            if (scanline >= VBLANK_START)
            {
                mode = Mode.VBlank;
                status = Bit.Set(status, 0, true);
                status = Bit.Set(status, 1, false);
                requestInterrupt = this.VBlankInterruptEnabled;
            }
            else if (this.scanlineWalker >= MODE_2_BOUNDS)
            {
                mode = Mode.OamScan;
                status = Bit.Set(status, 0, false);
                status = Bit.Set(status, 1, true);
                requestInterrupt = this.OamScanInterruptEnabled;
            }
            else if (this.scanlineWalker >= MODE_3_BOUNDS)
            {
                mode = Mode.HDraw;
                status = Bit.Set(status, 0, true);
                status = Bit.Set(status, 1, true);
            }
            else
            {
                mode = Mode.HBlank;
                status = Bit.Set(status, 0, false);
                status = Bit.Set(status, 1, false);
                requestInterrupt = this.HBlankInterruptEnabled;
            }
            
            if (requestInterrupt && mode != this.LcdMode)
                this.processor.Interrupts.Trigger(Interrupts.Type.Lcd);
            
            // Checking the coincidence flag 
            if (scanline == this.YCompare)
            {
                status = Bit.Set(status, 2, true);
                if (this.YCompareInterruptEnabled)
                    this.processor.Interrupts.Trigger(Interrupts.Type.Lcd);
            }
            else
            {
                status = Bit.Set(status, 2, false);
            }
            
            this.LcdStatus = status;
        }
        
        public void Tick(int timesToTick)
        {
            UpdateLcd();

            if (this.LcdDisplayEnable)
                this.scanlineWalker -= timesToTick;

            if (this.scanlineWalker > 0)
                return;

            this.scanlineWalker += SCANLINE_CYCLE;
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
            this.processor.Interrupts.Trigger(Interrupts.Type.VBlank);
        }

        private void DrawScanline()
        {
        }

        public void RenderTileSet()
        {
            for (var tileNumber = 0; tileNumber < (144 / 8) * (160 / 8); tileNumber++)
            {
                var tile = new Tile(this.processor.Memory, tileNumber);
                var tileData = tile.Data;

                for (var y = 0; y < tileData.Length; y++)
                {
                    for (var x = 0; x < tileData[y].Length; x++)
                    {
                        this.screen.FrameBuffer[(tileNumber * 8 % 160) + x + (y + tileNumber * 8 / 160 * 8) * 160] =
                            this.palette[tileData[y][x]];
                    }
                }
            }
        }
    }
}