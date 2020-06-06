using System;

namespace ColdBoi.CPU.Instructions
{
    public class DecB : Instruction
    {
        public const byte OPCODE = 0x05;
        public const string NAME = "dec";

        public DecB(Processor processor) : base(processor, OPCODE, 0, 4, NAME)
        {
        }

        public override void Execute(params byte[] operands)
        {
            this.processor.Registers.HalfCarry.Value = (this.processor.Registers.BC.HigherByte & 0x0f) == 0;

            this.processor.Registers.BC.HigherByte -= 1;

            this.processor.Registers.Zero.Value = this.processor.Registers.BC.HigherByte == 0;
            this.processor.Registers.Subtract.Value = true;

#if DEBUG
            Console.WriteLine($"{this.processor.Registers.PC.Value:X4}: {this.Name} b");
#endif
        }
    }
}