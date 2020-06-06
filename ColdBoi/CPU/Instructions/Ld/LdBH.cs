using System;

namespace ColdBoi.CPU.Instructions
{
    public class LdBH : Instruction
    {
        public const byte OPCODE = 0x44;
        public const string NAME = "ld";

        public LdBH(Processor processor) : base(processor, OPCODE, 0, 4, NAME)
        {
        }

        public override void Execute(params byte[] operands)
        {
            this.processor.Registers.BC.HigherByte = this.processor.Registers.HL.HigherByte;

#if DEBUG
            Console.WriteLine($"{this.processor.Registers.PC.Value:X4}: {this.Name} b, h");
#endif
        }
    }
}