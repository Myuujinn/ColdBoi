using System;

namespace ColdBoi.CPU.Instructions
{
    public class XorN : Instruction
    {
        public const byte OPCODE = 0xee;
        public const string NAME = "xor";

        public XorN(Processor processor) : base(processor, OPCODE, 1, 8, NAME)
        {
        }

        public override void Execute(params byte[] operands)
        {
            this.processor.Registers.ResetFlags();
            
            var value = operands[0];
            this.processor.Registers.AF.HigherByte ^= value;

            if (this.processor.Registers.AF.HigherByte == 0)
                this.processor.Registers.Zero.Value = true;
            
#if DEBUG
            Console.WriteLine($"{this.processor.Registers.PC.Value:X4}: {this.Name} {value}");
#endif
        }
    }
}