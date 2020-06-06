using System;

namespace ColdBoi.CPU.Instructions
{
    public class LdLH : Instruction
    {
        public const byte OPCODE = 0x6c;
        public const string NAME = "ld";

        public LdLH(Processor processor) : base(processor, OPCODE, 0, 4, NAME)
        {
        }

        public override void Execute(params byte[] operands)
        {
            this.processor.Registers.HL.LowerByte = this.processor.Registers.HL.HigherByte;

#if DEBUG
            Console.WriteLine($"{this.processor.Registers.PC.Value:X4}: {this.Name} l, h");
#endif
        }
    }
}