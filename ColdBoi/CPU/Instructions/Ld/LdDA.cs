using System;

namespace ColdBoi.CPU.Instructions
{
    public class LdDA : Instruction
    {
        public const byte OPCODE = 0x57;
        public const string NAME = "ld";

        public LdDA(Processor processor) : base(processor, OPCODE, 0, 4, NAME)
        {
        }

        public override void Execute(params byte[] operands)
        {
            this.processor.Registers.DE.HigherByte = this.processor.Registers.AF.HigherByte;

#if DEBUG
            Console.WriteLine($"{this.processor.Registers.PC.Value:X4}: {this.Name} d, a");
#endif
        }
    }
}