using System;

namespace ColdBoi.CPU.Instructions
{
    public class LdCD : Instruction
    {
        public const byte OPCODE = 0x4a;
        public const string NAME = "ld";

        public LdCD(Processor processor) : base(processor, OPCODE, 0, 4, NAME)
        {
        }

        public override void Execute(params byte[] operands)
        {
            this.processor.Registers.BC.LowerByte = this.processor.Registers.DE.HigherByte;

#if DEBUG
            Console.WriteLine($"{this.processor.Registers.PC.Value:X4}: {this.Name} c, d");
#endif
        }
    }
}