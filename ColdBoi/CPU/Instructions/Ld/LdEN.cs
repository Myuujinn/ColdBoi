using System;

namespace ColdBoi.CPU.Instructions
{
    public class LdEN : Instruction
    {
        public const byte OPCODE = 0x1e;
        public const string NAME = "ld";

        public LdEN(Processor processor) : base(processor, OPCODE, 1, 8, NAME)
        {
        }

        public override void Execute(params byte[] operands)
        {
            var value = operands[0];
            this.processor.Registers.DE.LowerByte = value;

#if DEBUG
            Console.WriteLine($"{this.processor.Registers.PC.Value:X4}: {this.Name} e, {value:X2}");
#endif
        }
    }
}