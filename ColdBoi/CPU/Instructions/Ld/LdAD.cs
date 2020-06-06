using System;

namespace ColdBoi.CPU.Instructions
{
    public class LdAD : Instruction
    {
        public const byte OPCODE = 0x7a;
        public const string NAME = "ld";

        public LdAD(Processor processor) : base(processor, OPCODE, 0, 4, NAME)
        {
        }

        public override void Execute(params byte[] operands)
        {
            this.processor.Registers.AF.HigherByte = this.processor.Registers.DE.HigherByte;

#if DEBUG
            Console.WriteLine($"{this.processor.Registers.PC.Value:X4}: {this.Name} a, d");
#endif
        }
    }
}