namespace ColdBoi.PPU
{
    public class Tile
    {
        private const int TILE_WIDTH = 0x8;
        private const int TILE_HEIGHT = 0x8;
        private const int TILE_SIZE = 0x10;
        private const int LINE_SIZE = 0x2;
        
        public byte[][] Data => Generate();
        public int Number { get; }

        private readonly Memory memory;
        
        public Tile(Memory memory, int tileNumber)
        {
            this.Number = tileNumber;
            this.memory = memory;
        }

        private byte[][] Generate()
        {
            var tileData = new byte[TILE_HEIGHT][];
            var dataAddress = (ushort) (Graphics.VRAM_START + this.Number * TILE_SIZE);
            
            for (var i = 0; i < TILE_HEIGHT; i++)
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
            
            var lineData = new byte[TILE_WIDTH];
            for (var i = 0; i < LINE_SIZE; i++)
            {
                var lineBit = 7;
                for (var bit = 0; bit < TILE_WIDTH; bit++)
                {
                    if (Bit.IsSet(lineValues[i], bit))
                        lineData[lineBit] = Bit.Set(lineData[lineBit], i, true);
                    lineBit -= 1;
                }
            }

            return lineData;
        }
    }
}