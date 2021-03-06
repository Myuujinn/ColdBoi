using System;

namespace ColdBoi.CPU.Instructions
{
    public class LdBD : Instruction
    {
        public const byte OPCODE = 0x42;
        public const string NAME = "ld";

        public LdBD(Processor processor) : base(processor, OPCODE, 0, 4, NAME)
        {
        }

        public override void Execute(params byte[] operands)
        {
            this.processor.Registers.BC.HigherByte = this.processor.Registers.DE.HigherByte;

#if DEBUG
            Console.WriteLine($"{this.processor.Registers.PC.Value:X4}: {this.Name} b, d");
#endif
        }
    }
}