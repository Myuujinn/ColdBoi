using System;

namespace ColdBoi.CPU.Instructions
{
    public class IncC : Instruction
    {
        public const byte OPCODE = 0x0c;
        public const string NAME = "inc";

        public IncC(Processor processor) : base(processor, OPCODE, 0, 4, NAME)
        {
        }

        public override void Execute(params byte[] operands)
        {
            this.processor.Registers.HalfCarry.Value = (this.processor.Registers.BC.LowerByte & 0x0f) == 0x0f;

            this.processor.Registers.BC.LowerByte += 1;

            this.processor.Registers.Zero.Value = this.processor.Registers.BC.LowerByte == 0;
            this.processor.Registers.Subtract.Value = false;

#if DEBUG
            Console.WriteLine($"{this.processor.Registers.PC.Value:X4}: {this.Name} c");
#endif
        }
    }
}