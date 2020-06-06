using System;

namespace ColdBoi.CPU.Instructions
{
    public class LdHC : Instruction
    {
        public const byte OPCODE = 0x61;
        public const string NAME = "ld";

        public LdHC(Processor processor) : base(processor, OPCODE, 0, 4, NAME)
        {
        }

        public override void Execute(params byte[] operands)
        {
            this.processor.Registers.HL.HigherByte = this.processor.Registers.BC.LowerByte;

#if DEBUG
            Console.WriteLine($"{this.processor.Registers.PC.Value:X4}: {this.Name} h, c");
#endif
        }
    }
}