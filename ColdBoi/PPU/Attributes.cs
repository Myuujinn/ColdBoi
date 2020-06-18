namespace ColdBoi.PPU
{
    public class Attributes
    {
        public byte Value { get; }

        private const ushort PALETTE_ADDRESS = 0xff48;
        private const ushort PALETTE_ADDRESS_SET = 0xff49;

        private const int PALETTE_BIT = 4;
        private const int X_FLIP_BIT = 5;
        private const int Y_FLIP_BIT = 6;
        private const int BACKGROUND_PRIORITY_BIT = 7;

        public ushort PaletteAddress => Bit.IsSet(this.Value, PALETTE_BIT) ? PALETTE_ADDRESS_SET : PALETTE_ADDRESS;
        public bool XFlip => Bit.IsSet(this.Value, X_FLIP_BIT);
        public bool YFlip => Bit.IsSet(this.Value, Y_FLIP_BIT);
        public bool BackgroundPriority => Bit.IsSet(this.Value, BACKGROUND_PRIORITY_BIT);
        
        public Attributes(byte attribute)
        {
            this.Value = attribute;
        }
    }
}