using System;

namespace ColdBoi.CPU.Instructions
{
    public class LdHE : Instruction
    {
        public const byte OPCODE = 0x63;
        public const string NAME = "ld";

        public LdHE(Processor processor) : base(processor, OPCODE, 0, 4, NAME)
        {
        }

        public override void Execute(params byte[] operands)
        {
            this.processor.Registers.HL.HigherByte = this.processor.Registers.DE.LowerByte;

#if DEBUG
            Console.WriteLine($"{this.processor.Registers.PC.Value:X4}: {this.Name} h, e");
#endif
        }
    }
}