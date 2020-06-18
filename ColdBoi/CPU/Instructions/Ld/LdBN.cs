using System;

namespace ColdBoi.CPU.Instructions
{
    public class LdBN : Instruction
    {
        public const byte OPCODE = 0x06;
        public const string NAME = "ld";

        public LdBN(Processor processor) : base(processor, OPCODE, 1, 8, NAME)
        {
        }

        public override void Execute(params byte[] operands)
        {
            var value = operands[0];
            this.processor.Registers.BC.HigherByte = value;

#if DEBUG
            Console.WriteLine($"{this.processor.Registers.PC.Value:X4}: {this.Name} b, {value:X2}");
#endif
        }
    }
}