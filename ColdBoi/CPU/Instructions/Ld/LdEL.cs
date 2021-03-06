using System;

namespace ColdBoi.CPU.Instructions
{
    public class LdEL : Instruction
    {
        public const byte OPCODE = 0x5d;
        public const string NAME = "ld";

        public LdEL(Processor processor) : base(processor, OPCODE, 0, 4, NAME)
        {
        }

        public override void Execute(params byte[] operands)
        {
            this.processor.Registers.DE.LowerByte = this.processor.Registers.HL.LowerByte;

#if DEBUG
            Console.WriteLine($"{this.processor.Registers.PC.Value:X4}: {this.Name} e, l");
#endif
        }
    }
}