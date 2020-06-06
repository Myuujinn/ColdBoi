using System;

namespace ColdBoi.CPU.Instructions
{
    public class LdAL : Instruction
    {
        public const byte OPCODE = 0x7d;
        public const string NAME = "ld";

        public LdAL(Processor processor) : base(processor, OPCODE, 0, 4, NAME)
        {
        }

        public override void Execute(params byte[] operands)
        {
            this.processor.Registers.AF.HigherByte = this.processor.Registers.HL.LowerByte;

#if DEBUG
            Console.WriteLine($"{this.processor.Registers.PC.Value:X4}: {this.Name} a, l");
#endif
        }
    }
}