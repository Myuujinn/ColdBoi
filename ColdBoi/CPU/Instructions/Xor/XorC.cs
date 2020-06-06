using System;

namespace ColdBoi.CPU.Instructions
{
    public class XorC : Instruction
    {
        public const byte OPCODE = 0xa9;
        public const string NAME = "xor";

        public XorC(Processor processor) : base(processor, OPCODE, 0, 4, NAME)
        {
        }

        public override void Execute(params byte[] operands)
        {
            this.processor.Registers.ResetFlags();

            this.processor.Registers.AF.HigherByte ^= this.processor.Registers.BC.LowerByte;

            if (this.processor.Registers.AF.HigherByte == 0)
                this.processor.Registers.Zero.Value = true;
            
#if DEBUG
            Console.WriteLine($"{this.processor.Registers.PC.Value:X4}: {this.Name} c");
#endif
        }
    }
}