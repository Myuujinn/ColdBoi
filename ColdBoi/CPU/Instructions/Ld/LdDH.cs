using System;

namespace ColdBoi.CPU.Instructions
{
    public class LdDH : Instruction
    {
        public const byte OPCODE = 0x54;
        public const string NAME = "ld";

        public LdDH(Processor processor) : base(processor, OPCODE, 0, 4, NAME)
        {
        }

        public override void Execute(params byte[] operands)
        {
            this.processor.Registers.DE.HigherByte = this.processor.Registers.HL.HigherByte;

#if DEBUG
            Console.WriteLine($"{this.processor.Registers.PC.Value:X4}: {this.Name} d, h");
#endif
        }
    }
}