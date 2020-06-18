using System;

namespace ColdBoi.PPU
{
    public class Tile
    {
        public const int WIDTH = 0x8;
        public const int HEIGHT = 0x8;
        public const int SIZE = 0x10;
        public const int LINE_SIZE = 0x2;
        public const int DATA_SELECT_BIT = 4;
        
        public byte[][] Data => cachedData ??= Generate();
        private byte[][] cachedData;
        public byte[][] FlippedX => GenerateFlippedX(this.Data);
        public byte[][] FlippedY => GenerateFlippedY(this.Data);
        public byte[][] FlippedXY => GenerateFlippedX(GenerateFlippedY(this.Data));
        public int Number { get; }

        private readonly Graphics graphics;
        private Memory memory => graphics.Memory;
        private ushort TileDataAddress =>
            (ushort) (Bit.IsSet(this.graphics.Control, DATA_SELECT_BIT) ? 0x8000 : 0x8800);

        public Tile(Graphics graphics, int tileNumber)
        {
            this.Number = tileNumber;
            this.graphics = graphics;
        }

        private byte[][] Generate()
        {
            var tileData = new byte[HEIGHT][];
            var dataAddress = (ushort) (this.TileDataAddress + this.Number * SIZE);

            for (var i = 0; i < HEIGHT; i++)
            {
                tileData[i] = GenerateLine(dataAddress);
                dataAddress += LINE_SIZE;
            }

            return tileData;
        }

        private byte[] GenerateLine(ushort address)
        {
            var lineValues = new byte[LINE_SIZE];
            for (var i = 0; i < LINE_SIZE; i++)
            {
                lineValues[i] = this.memory.Content[address + i];
            }

            var lineData = new byte[WIDTH];
            for (var i = 0; i < LINE_SIZE; i++)
            {
                var lineBit = 7;
                for (var bit = 0; bit < WIDTH; bit++)
                {
                    if (Bit.IsSet(lineValues[i], bit))
                        lineData[lineBit] = Bit.Set(lineData[lineBit], i, true);
                    lineBit -= 1;
                }
            }

            return lineData;
        }
        
        private byte[][] GenerateFlippedX(byte[][] data)
        {
            var flippedData = new byte[data.Length][];

            for (var i = 0; i < data.Length; i++)
            {
                flippedData[i] = new byte[data[i].Length];
                
                Array.Copy(data[i], flippedData[i], data[i].Length);
                Array.Reverse(flippedData[i]);
            }

            return flippedData;
        }
        
        private byte[][] GenerateFlippedY(byte[][] data)
        {
            var flippedData = new byte[data.Length][];
            
            Array.Copy(data, flippedData, data.Length);
            Array.Reverse(flippedData);

            return flippedData;
        }

        public byte[][] FromAttributes(Attributes attributes)
        {
            var tileData = this.FlippedX; // design flaw
                
            if (attributes.XFlip && attributes.YFlip)
                tileData = this.FlippedXY;
            else if (attributes.XFlip)
                tileData = this.Data; // yeah.. this is bad
            else if (attributes.YFlip)
                tileData = this.FlippedY;

            return tileData;
        }
    }
}