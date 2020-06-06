using System;

namespace ColdBoi.CPU.Instructions
{
    public class LdAN : Instruction
    {
        public const byte OPCODE = 0x3e;
        public const string NAME = "ld";

        public LdAN(Processor processor) : base(processor, OPCODE, 1, 8, NAME)
        {
        }

        public override void Execute(params byte[] operands)
        {
            var value = operands[0];
            this.processor.Registers.AF.HigherByte = value;

#if DEBUG
            Console.WriteLine($"{this.processor.Registers.PC.Value:X4}: {this.Name} a, {value:X2}");
#endif
        }
    }
}