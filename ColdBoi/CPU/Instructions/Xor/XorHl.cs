using System;

namespace ColdBoi.CPU.Instructions
{
    public class XorHl : Instruction
    {
        public const byte OPCODE = 0xae;
        public const string NAME = "xor";

        public XorHl(Processor processor) : base(processor, OPCODE, 0, 8, NAME)
        {
        }

        public override void Execute(params byte[] operands)
        {
            this.processor.Registers.ResetFlags();

            this.processor.Registers.AF.HigherByte ^= this.processor.Memory.Content[this.processor.Registers.HL.Value];

            if (this.processor.Registers.AF.HigherByte == 0)
                this.processor.Registers.Zero.Value = true;
            
#if DEBUG
            Console.WriteLine($"{this.processor.Registers.PC.Value:X4}: {this.Name} (hl)");
#endif
        }
    }
}