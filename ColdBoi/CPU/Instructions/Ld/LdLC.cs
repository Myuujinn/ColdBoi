using System;

namespace ColdBoi.CPU.Instructions
{
    public class LdLC : Instruction
    {
        public const byte OPCODE = 0x69;
        public const string NAME = "ld";

        public LdLC(Processor processor) : base(processor, OPCODE, 0, 4, NAME)
        {
        }

        public override void Execute(params byte[] operands)
        {
            this.processor.Registers.HL.LowerByte = this.processor.Registers.BC.LowerByte;

#if DEBUG
            Console.WriteLine($"{this.processor.Registers.PC.Value:X4}: {this.Name} l, c");
#endif
        }
    }
}