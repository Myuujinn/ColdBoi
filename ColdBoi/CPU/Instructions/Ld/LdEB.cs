using System;

namespace ColdBoi.CPU.Instructions
{
    public class LdEB : Instruction
    {
        public const byte OPCODE = 0x58;
        public const string NAME = "ld";

        public LdEB(Processor processor) : base(processor, OPCODE, 0, 4, NAME)
        {
        }

        public override void Execute(params byte[] operands)
        {
            this.processor.Registers.DE.LowerByte = this.processor.Registers.BC.HigherByte;

#if DEBUG
            Console.WriteLine($"{this.processor.Registers.PC.Value:X4}: {this.Name} e, b");
#endif
        }
    }
}