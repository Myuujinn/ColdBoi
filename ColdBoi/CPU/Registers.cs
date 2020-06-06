using System;

namespace ColdBoi.CPU
{
    public class Registers
    {
        public RegisterPair AF { get; set; }
        public RegisterPair BC { get; set; }
        public RegisterPair DE { get; set; }
        public RegisterPair HL { get; set; }
        public BigRegister SP { get; set; }
        public BigRegister PC { get; set; }

        public byte Flag
        {
            get => AF.LowerByte;
            set { this.AF.LowerByte = value; }
        }

        public Flag Zero { get; private set; }
        public Flag Subtract { get; private set; }
        public Flag HalfCarry { get; private set; }
        public Flag Carry { get; private set; }

        public Registers()
        {
            this.AF = new RegisterPair();
            this.BC = new RegisterPair();
            this.DE = new RegisterPair();
            this.HL = new RegisterPair();
            this.SP = new BigRegister();
            this.PC = new BigRegister();

            this.Zero = new Flag(this.AF, FlagType.Zero);
            this.Subtract = new Flag(this.AF, FlagType.Subtract);
            this.HalfCarry = new Flag(this.AF, FlagType.HalfCarry);
            this.Carry = new Flag(this.AF, FlagType.Carry);
        }

        public void ResetFlags()
        {
            this.Flag = 0;
        }

        public void Dump()
        {
            Console.WriteLine(
                $"AF: {this.AF.Value:X4}, BC: {this.BC.Value:X4}, DE: {this.DE.Value:X4}, HL: {this.HL.Value:X4}, SP: {this.SP.Value:X4}, PC: {this.PC.Value:X4}");
            Console.WriteLine(
                $"Zero: {this.Zero.Value}, Subtract: {this.Subtract.Value}, HalfCarry: {this.HalfCarry.Value}, Carry: {this.Carry.Value},");
        }
    }
}