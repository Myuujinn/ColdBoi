using System;

namespace ColdBoi.CPU.Instructions
{
    public class DecC : Instruction
    {
        public const byte OPCODE = 0x0D;
        public const string NAME = "dec";

        public DecC(Processor processor) : base(processor, OPCODE, 0, 4, NAME)
        {
        }

        public override void Execute(params byte[] operands)
        {
            this.processor.Registers.HalfCarry.Value = (this.processor.Registers.BC.LowerByte & 0x0f) == 0;

            this.processor.Registers.BC.LowerByte -= 1;

            this.processor.Registers.Zero.Value = this.processor.Registers.BC.LowerByte == 0;
            this.processor.Registers.Subtract.Value = true;

#if DEBUG
            Console.WriteLine($"{this.processor.Registers.PC.Value:X4}: {this.Name} c");
#endif
        }
    }
}