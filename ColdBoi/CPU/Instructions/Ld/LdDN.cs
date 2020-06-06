using System;

namespace ColdBoi.CPU.Instructions
{
    public class LdDN : Instruction
    {
        public const byte OPCODE = 0x16;
        public const string NAME = "ld";

        public LdDN(Processor processor) : base(processor, OPCODE, 1, 8, NAME)
        {
        }

        public override void Execute(params byte[] operands)
        {
            var value = operands[0];
            this.processor.Registers.DE.HigherByte = value;

#if DEBUG
            Console.WriteLine($"{this.processor.Registers.PC.Value:X4}: {this.Name} d, {value:X2}");
#endif
        }
    }
}