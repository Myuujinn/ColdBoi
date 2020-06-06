using System;

namespace ColdBoi.CPU.Instructions
{
    public class LdBE : Instruction
    {
        public const byte OPCODE = 0x43;
        public const string NAME = "ld";

        public LdBE(Processor processor) : base(processor, OPCODE, 0, 4, NAME)
        {
        }

        public override void Execute(params byte[] operands)
        {
            this.processor.Registers.BC.HigherByte = this.processor.Registers.DE.LowerByte;

#if DEBUG
            Console.WriteLine($"{this.processor.Registers.PC.Value:X4}: {this.Name} b, e");
#endif
        }
    }
}