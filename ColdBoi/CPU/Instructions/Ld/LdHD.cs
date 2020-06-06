using System;

namespace ColdBoi.CPU.Instructions
{
    public class LdHD : Instruction
    {
        public const byte OPCODE = 0x62;
        public const string NAME = "ld";

        public LdHD(Processor processor) : base(processor, OPCODE, 0, 4, NAME)
        {
        }

        public override void Execute(params byte[] operands)
        {
            this.processor.Registers.HL.HigherByte = this.processor.Registers.DE.HigherByte;

#if DEBUG
            Console.WriteLine($"{this.processor.Registers.PC.Value:X4}: {this.Name} h, d");
#endif
        }
    }
}