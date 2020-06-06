using System;

namespace ColdBoi.CPU.Instructions
{
    public class LdLD : Instruction
    {
        public const byte OPCODE = 0x6a;
        public const string NAME = "ld";

        public LdLD(Processor processor) : base(processor, OPCODE, 0, 4, NAME)
        {
        }

        public override void Execute(params byte[] operands)
        {
            this.processor.Registers.HL.LowerByte = this.processor.Registers.DE.HigherByte;

#if DEBUG
            Console.WriteLine($"{this.processor.Registers.PC.Value:X4}: {this.Name} l, d");
#endif
        }
    }
}