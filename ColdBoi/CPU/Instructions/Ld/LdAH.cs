using System;

namespace ColdBoi.CPU.Instructions
{
    public class LdAH : Instruction
    {
        public const byte OPCODE = 0x7c;
        public const string NAME = "ld";

        public LdAH(Processor processor) : base(processor, OPCODE, 0, 4, NAME)
        {
        }

        public override void Execute(params byte[] operands)
        {
            this.processor.Registers.AF.HigherByte = this.processor.Registers.HL.HigherByte;

#if DEBUG
            Console.WriteLine($"{this.processor.Registers.PC.Value:X4}: {this.Name} a, h");
#endif
        }
    }
}