using System;

namespace ColdBoi.CPU
{
    public enum FlagType : byte
    {
        Carry = 4,
        HalfCarry = 5,
        Subtract = 6,
        Zero = 7
    }

    public class Flag
    {
        public FlagType Type { get; protected set; }

        private readonly RegisterPair af;
        private byte BitNumber => (byte) this.Type;
        private byte FlagRegister => this.af.LowerByte;

        public bool Value
        {
            get => (this.FlagRegister & (1 << this.BitNumber)) > 0;
            set
            {
                this.af.LowerByte &= (byte) ~(1 << this.BitNumber);
                this.af.LowerByte |= (byte) (Convert.ToByte(value) << this.BitNumber);
            }
        }

        public Flag(RegisterPair af, FlagType type)
        {
            this.af = af;
            this.Type = type;
        }
    }
}