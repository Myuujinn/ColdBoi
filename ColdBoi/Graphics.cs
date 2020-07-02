using System;
using System.Collections.Generic;
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
        public const ushort GRAPHICS_CONTROL = 0xff40;
        public const ushort LCD_STATUS = 0xff41;
        public const ushort SCROLL_Y = 0xff42;
        public const ushort SCROLL_X = 0xff43;
        public const ushort SCANLINE = 0xff44;
        public const ushort LCD_Y_COMPARE = 0xff45;
        public const ushort WINDOW_Y = 0xff4a;
        public const ushort WINDOW_X = 0xff4b;
        public const ushort OAM_START = 0xfe00;

        private const int SCANLINE_CYCLE = 456;
        private const int MODE_2_BOUNDS = SCANLINE_CYCLE - 80;
        private const int MODE_3_BOUNDS = SCANLINE_CYCLE - 172;
        private const int SCANLINES = 153;
        private const int VBLANK_START = 144;
        private const int SPRITES = 40;
        private const int SPRITE_OAM_SIZE = 4;
        private const byte SPRITE_X_OFFSET = 8;
        private const byte SPRITE_Y_OFFSET = 16;
        private const int SPRITE_X_SIZE = 8;
        private const int SPRITE_Y_SIZE = 8;

        private Color[] palette;
        private int scanlineWalker;

        private readonly Processor processor;
        private readonly Screen screen;

        public Memory Memory => this.processor.Memory;
        public Background Background { get; }

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

        public byte ScrollX
        {
            get => this.Memory.Content[SCROLL_X];
            set => this.Memory.Write(SCROLL_X, value);
        }

        public byte ScrollY
        {
            get => this.Memory.Content[SCROLL_Y];
            set => this.Memory.Write(SCROLL_Y, value);
        }

        public byte WindowX
        {
            get => (byte) (this.Memory.Content[WINDOW_X] - 7);
            set => this.Memory.Write(WINDOW_X, value);
        }

        public byte WindowY
        {
            get => this.Memory.Content[WINDOW_Y];
            set => this.Memory.Write(WINDOW_Y, value);
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
        public ushort BackgroundTileMap => (ushort) (Bit.IsSet(this.Control, 3) ? 0x9c00 : 0x9800);
        public bool WindowEnable => Bit.IsSet(this.Control, 5);
        public ushort WindowTileMap => (ushort) (Bit.IsSet(this.Control, 6) ? 0x9c00 : 0x9800);
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

            this.Background = new Background(this);

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
            if (this.BgDisplay)
                DrawTiles();

            if (this.ObjDisplay)
                DrawSprites();
        }

        private void DrawTiles()
        {
            var cachedTiles = new Dictionary<int, Tile>();

            var drawingWindow = this.WindowEnable && this.WindowY <= this.Scanline;
            var posY = (int) this.Scanline;
            posY += drawingWindow ? this.ScrollY : -this.WindowY;
            var tileRow = posY / 8 * 32;

            for (var pixel = 0; pixel < Screen.SCREEN_WIDTH; pixel++)
            {
                var posX = drawingWindow && pixel > this.WindowX ? pixel - this.WindowX : pixel + this.ScrollX;
                var tileColumn = posX / Tile.WIDTH;
                var mapAddress = this.BackgroundTileMap + tileRow + tileColumn;
                var tileNumber = this.Memory.Content[mapAddress];

                if (!cachedTiles.TryGetValue(tileNumber, out var tile))
                {
                    tile = new Tile(this, tileNumber);
                    cachedTiles.Add(tileNumber, tile);
                }

                var color = this.palette[tile.Data[posY % 8][posX % 8]];

                this.screen.SetPixel(pixel, this.Scanline, color);
            }
        }

        private void DrawSprites()
        {
            var cachedTiles = new Dictionary<int, Tile>();

            for (var sprite = 0; sprite < SPRITES; sprite++)
            {
                var spriteIndex = sprite * SPRITE_OAM_SIZE;
                var posY = this.processor.Memory.Content[OAM_START + spriteIndex];
                var posX = this.processor.Memory.Content[OAM_START + spriteIndex + 1];
                var tileNumber = this.processor.Memory.Content[OAM_START + spriteIndex + 2];
                var attributes = new Attributes(this.processor.Memory.Content[OAM_START + spriteIndex + 3]);
                
                if (posX == 0 && posY == 0 && tileNumber == 0 && attributes.Value == 0)
                    continue;

                posX -= SPRITE_X_OFFSET;
                posY -= SPRITE_Y_OFFSET;
                    
                if (!IsIntersectingWithScanline(posY, SPRITE_Y_SIZE))
                    continue;
                
                if (!cachedTiles.TryGetValue(tileNumber, out var tile))
                {
                    tile = new Tile(this, tileNumber);
                    cachedTiles.Add(tileNumber, tile);
                }

                var line = this.Scanline - posY;
                var tileData = tile.FromAttributes(attributes);

                for (var tilePixel = 0; tilePixel < SPRITE_X_SIZE; tilePixel++)
                {
                    var color = this.palette[tileData[line][tilePixel]];

                    if (color == Color.White) // White is transparent for sprites
                        continue;

                    var pixel = posX - tilePixel + SPRITE_X_SIZE - 1;
                    
                    if (pixel < 0 || pixel > Screen.SCREEN_WIDTH) // Just to be sure
                        continue;

                    this.screen.SetPixel(pixel, this.Scanline, color);
                }
            }
        }

        private bool IsIntersectingWithScanline(int y, int size)
        {
            return this.Scanline >= y && this.Scanline < y + size;
        }
    }
}