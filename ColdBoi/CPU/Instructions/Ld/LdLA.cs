using System;

namespace ColdBoi.CPU.Instructions
{
    public class LdLA : Instruction
    {
        public const byte OPCODE = 0x6f;
        public const string NAME = "ld";

        public LdLA(Processor processor) : base(processor, OPCODE, 0, 4, NAME)
        {
        }

        public override void Execute(params byte[] operands)
        {
            this.processor.Registers.HL.LowerByte = this.processor.Registers.AF.HigherByte;

#if DEBUG
            Console.WriteLine($"{this.processor.Registers.PC.Value:X4}: {this.Name} l, a");
#endif
        }
    }
}