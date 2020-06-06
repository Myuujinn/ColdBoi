using System;

namespace ColdBoi.CPU.Instructions
{
    public class LdEA : Instruction
    {
        public const byte OPCODE = 0x5f;
        public const string NAME = "ld";

        public LdEA(Processor processor) : base(processor, OPCODE, 0, 4, NAME)
        {
        }

        public override void Execute(params byte[] operands)
        {
            this.processor.Registers.DE.LowerByte = this.processor.Registers.AF.HigherByte;

#if DEBUG
            Console.WriteLine($"{this.processor.Registers.PC.Value:X4}: {this.Name} e, a");
#endif
        }
    }
}