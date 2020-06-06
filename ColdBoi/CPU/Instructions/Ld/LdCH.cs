using System;

namespace ColdBoi.CPU.Instructions
{
    public class LdCH : Instruction
    {
        public const byte OPCODE = 0x4c;
        public const string NAME = "ld";

        public LdCH(Processor processor) : base(processor, OPCODE, 0, 4, NAME)
        {
        }

        public override void Execute(params byte[] operands)
        {
            this.processor.Registers.BC.LowerByte = this.processor.Registers.HL.HigherByte;

#if DEBUG
            Console.WriteLine($"{this.processor.Registers.PC.Value:X4}: {this.Name} c, h");
#endif
        }
    }
}