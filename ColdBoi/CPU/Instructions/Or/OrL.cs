using System;

namespace ColdBoi.CPU.Instructions
{
    public class OrL : Instruction
    {
        public const byte OPCODE = 0xb5;
        public const string NAME = "or";

        public OrL(Processor processor) : base(processor, OPCODE, 0, 4, NAME)
        {
        }

        public override void Execute(params byte[] operands)
        {
            this.processor.Registers.ResetFlags();

            this.processor.Registers.AF.HigherByte |= this.processor.Registers.HL.LowerByte;

            this.processor.Registers.Zero.Value = this.processor.Registers.AF.HigherByte == 0;

#if DEBUG
            Console.WriteLine($"{this.processor.Registers.PC.Value:X4}: {this.Name} h");
#endif
        }
    }
}