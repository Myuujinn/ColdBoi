using System;

namespace ColdBoi.CPU.BigInstructions
{
    public class SwapA : BigInstruction
    {
        public const ushort OPCODE = 0xcb37;
        public const string NAME = "swap";

        public SwapA(Processor processor) : base(processor, OPCODE, 0, 8, NAME)
        {
        }

        public override void Execute(params byte[] operands)
        {
            this.processor.Registers.ResetFlags();

            this.processor.Registers.AF.HigherByte = (byte) (((this.processor.Registers.AF.HigherByte & 0xf) << 4) |
                                                             ((this.processor.Registers.AF.HigherByte & 0xf0) >> 4));

            this.processor.Registers.Zero.Value = this.processor.Registers.AF.HigherByte == 0;
#if DEBUG
            Console.WriteLine($"{this.processor.Registers.PC.Value:X4}: {this.Name} a");
#endif
        }
    }
}