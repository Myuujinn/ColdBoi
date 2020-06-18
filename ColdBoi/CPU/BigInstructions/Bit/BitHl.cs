using System;

namespace ColdBoi.CPU.BigInstructions
{
    public class BitHl : BigInstruction
    {
        public const string NAME = "bit";

        private int bitNumber;

        public BitHl(Processor processor, ushort opCode, int bitNumber) : base(processor, opCode, 0, 16, NAME)
        {
            this.bitNumber = bitNumber;
        }

        public override void Execute(params byte[] operands)
        {
            var value = this.processor.Memory.Content[this.processor.Registers.HL.Value];
            this.processor.Registers.Zero.Value = !global::ColdBoi.Bit.IsSet(value, this.bitNumber);
            this.processor.Registers.Subtract.Value = false;
            this.processor.Registers.HalfCarry.Value = true;

#if DEBUG
            Console.WriteLine($"{this.processor.Registers.PC.Value:X4}: {this.Name} {this.bitNumber}, (hl)");
#endif
        }
    }
}