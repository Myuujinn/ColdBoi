using System;

namespace ColdBoi.CPU.Instructions
{
    public class LdEE : Instruction
    {
        public const byte OPCODE = 0x5b;
        public const string NAME = "ld";

        public LdEE(Processor processor) : base(processor, OPCODE, 0, 4, NAME)
        {
        }

        public override void Execute(params byte[] operands)
        {
            this.processor.Registers.DE.LowerByte = this.processor.Registers.DE.LowerByte;

#if DEBUG
            Console.WriteLine($"{this.processor.Registers.PC.Value:X4}: {this.Name} e, e");
#endif
        }
    }
}