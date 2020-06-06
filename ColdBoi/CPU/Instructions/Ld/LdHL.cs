using System;

namespace ColdBoi.CPU.Instructions
{
    public class LdHL : Instruction
    {
        public const byte OPCODE = 0x65;
        public const string NAME = "ld";

        public LdHL(Processor processor) : base(processor, OPCODE, 0, 4, NAME)
        {
        }

        public override void Execute(params byte[] operands)
        {
            this.processor.Registers.HL.HigherByte = this.processor.Registers.HL.LowerByte;

#if DEBUG
            Console.WriteLine($"{this.processor.Registers.PC.Value:X4}: {this.Name} h, l");
#endif
        }
    }
}