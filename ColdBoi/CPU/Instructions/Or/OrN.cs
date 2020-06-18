using System;

namespace ColdBoi.CPU.Instructions
{
    public class OrN : Instruction
    {
        public const byte OPCODE = 0xf6;
        public const string NAME = "or";

        public OrN(Processor processor) : base(processor, OPCODE, 1, 4, NAME)
        {
        }

        public override void Execute(params byte[] operands)
        {
            this.processor.Registers.ResetFlags();

            var value = operands[0];
            this.processor.Registers.AF.HigherByte |= value;

            this.processor.Registers.Zero.Value = this.processor.Registers.AF.HigherByte == 0;

#if DEBUG
            Console.WriteLine($"{this.processor.Registers.PC.Value:X4}: {this.Name} a, {value:X2}");
#endif
        }
    }
}