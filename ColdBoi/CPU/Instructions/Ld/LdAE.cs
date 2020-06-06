using System;

namespace ColdBoi.CPU.Instructions
{
    public class LdAE : Instruction
    {
        public const byte OPCODE = 0x7b;
        public const string NAME = "ld";

        public LdAE(Processor processor) : base(processor, OPCODE, 0, 4, NAME)
        {
        }

        public override void Execute(params byte[] operands)
        {
            this.processor.Registers.AF.HigherByte = this.processor.Registers.DE.LowerByte;

#if DEBUG
            Console.WriteLine($"{this.processor.Registers.PC.Value:X4}: {this.Name} a, e");
#endif
        }
    }
}