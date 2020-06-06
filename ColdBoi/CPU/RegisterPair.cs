namespace ColdBoi.CPU
{
    public class RegisterPair : IRegister
    {
        public byte LowerByte { get; set; }
        public byte HigherByte { get; set; }

        public ushort Value
        {
            get => (ushort) (LowerByte + (HigherByte << 8));
            set
            {
                this.LowerByte = (byte) value;
                this.HigherByte = (byte) (value >> 8);
            }
        }
    }
}