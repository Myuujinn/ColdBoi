using System;

namespace ColdBoi.CPU.Instructions
{
    public class LdDL : Instruction
    {
        public const byte OPCODE = 0x55;
        public const string NAME = "ld";

        public LdDL(Processor processor) : base(processor, OPCODE, 0, 4, NAME)
        {
        }

        public override void Execute(params byte[] operands)
        {
            this.processor.Registers.DE.HigherByte = this.processor.Registers.HL.LowerByte;

#if DEBUG
            Console.WriteLine($"{this.processor.Registers.PC.Value:X4}: {this.Name} d, l");
#endif
        }
    }
}