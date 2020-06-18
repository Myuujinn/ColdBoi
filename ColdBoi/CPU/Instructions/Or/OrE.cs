using System;

namespace ColdBoi.CPU.Instructions
{
    public class OrE : Instruction
    {
        public const byte OPCODE = 0xb3;
        public const string NAME = "or";

        public OrE(Processor processor) : base(processor, OPCODE, 0, 4, NAME)
        {
        }

        public override void Execute(params byte[] operands)
        {
            this.processor.Registers.ResetFlags();

            this.processor.Registers.AF.HigherByte |= this.processor.Registers.DE.LowerByte;

            this.processor.Registers.Zero.Value = this.processor.Registers.AF.HigherByte == 0;

#if DEBUG
            Console.WriteLine($"{this.processor.Registers.PC.Value:X4}: {this.Name} e");
#endif
        }
    }
}