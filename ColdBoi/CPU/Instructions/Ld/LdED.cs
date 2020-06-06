using System;

namespace ColdBoi.CPU.Instructions
{
    public class LdED : Instruction
    {
        public const byte OPCODE = 0x5a;
        public const string NAME = "ld";

        public LdED(Processor processor) : base(processor, OPCODE, 0, 4, NAME)
        {
        }

        public override void Execute(params byte[] operands)
        {
            this.processor.Registers.DE.LowerByte = this.processor.Registers.DE.HigherByte;

#if DEBUG
            Console.WriteLine($"{this.processor.Registers.PC.Value:X4}: {this.Name} e, d");
#endif
        }
    }
}