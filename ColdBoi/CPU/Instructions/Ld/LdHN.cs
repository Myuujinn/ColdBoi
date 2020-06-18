using System;

namespace ColdBoi.CPU.Instructions
{
    public class LdHN : Instruction
    {
        public const byte OPCODE = 0x26;
        public const string NAME = "ld";

        public LdHN(Processor processor) : base(processor, OPCODE, 1, 8, NAME)
        {
        }

        public override void Execute(params byte[] operands)
        {
            var value = operands[0];
            this.processor.Registers.HL.HigherByte = value;

#if DEBUG
            Console.WriteLine($"{this.processor.Registers.PC.Value:X4}: {this.Name} h, {value:X2}");
#endif
        }
    }
}