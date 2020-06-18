using System;

namespace ColdBoi.CPU.Instructions
{
    public class XorL : Instruction
    {
        public const byte OPCODE = 0xad;
        public const string NAME = "xor";

        public XorL(Processor processor) : base(processor, OPCODE, 0, 4, NAME)
        {
        }

        public override void Execute(params byte[] operands)
        {
            this.processor.Registers.ResetFlags();

            this.processor.Registers.AF.HigherByte ^= this.processor.Registers.HL.LowerByte;

            if (this.processor.Registers.AF.HigherByte == 0)
                this.processor.Registers.Zero.Value = true;
            
#if DEBUG
            Console.WriteLine($"{this.processor.Registers.PC.Value:X4}: {this.Name} l");
#endif
        }
    }
}