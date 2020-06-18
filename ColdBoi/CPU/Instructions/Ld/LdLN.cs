using System;

namespace ColdBoi.CPU.Instructions
{
    public class LdLN : Instruction
    {
        public const byte OPCODE = 0x2e;
        public const string NAME = "ld";

        public LdLN(Processor processor) : base(processor, OPCODE, 1, 8, NAME)
        {
        }

        public override void Execute(params byte[] operands)
        {
            var value = operands[0];
            this.processor.Registers.HL.LowerByte = value;

#if DEBUG
            Console.WriteLine($"{this.processor.Registers.PC.Value:X4}: {this.Name} l, {value:X2}");
#endif
        }
    }
}