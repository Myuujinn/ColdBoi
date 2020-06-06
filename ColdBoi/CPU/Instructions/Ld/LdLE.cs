using System;

namespace ColdBoi.CPU.Instructions
{
    public class LdLE : Instruction
    {
        public const byte OPCODE = 0x6b;
        public const string NAME = "ld";

        public LdLE(Processor processor) : base(processor, OPCODE, 0, 4, NAME)
        {
        }

        public override void Execute(params byte[] operands)
        {
            this.processor.Registers.HL.LowerByte = this.processor.Registers.DE.LowerByte;

#if DEBUG
            Console.WriteLine($"{this.processor.Registers.PC.Value:X4}: {this.Name} l, e");
#endif
        }
    }
}