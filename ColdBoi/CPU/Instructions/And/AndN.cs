using System;

namespace ColdBoi.CPU.Instructions
{
    public class AndN : Instruction
    {
        public const byte OPCODE = 0xe6;
        public const string NAME = "and";

        public AndN(Processor processor) : base(processor, OPCODE, 1, 8, NAME)
        {
        }

        public override void Execute(params byte[] operands)
        {
            this.processor.Registers.ResetFlags();

            var value = operands[0];

            this.processor.Registers.AF.HigherByte &= value;

            this.processor.Registers.Zero.Value = this.processor.Registers.AF.HigherByte == 0;
            this.processor.Registers.HalfCarry.Value = true;

#if DEBUG
            Console.WriteLine($"{this.processor.Registers.PC.Value:X4}: {this.Name} {value:X2}");
#endif
        }
    }
}