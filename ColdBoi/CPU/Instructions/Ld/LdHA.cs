using System;

namespace ColdBoi.CPU.Instructions
{
    public class LdHA : Instruction
    {
        public const byte OPCODE = 0x67;
        public const string NAME = "ld";

        public LdHA(Processor processor) : base(processor, OPCODE, 0, 4, NAME)
        {
        }

        public override void Execute(params byte[] operands)
        {
            this.processor.Registers.HL.HigherByte = this.processor.Registers.AF.HigherByte;

#if DEBUG
            Console.WriteLine($"{this.processor.Registers.PC.Value:X4}: {this.Name} h, a");
#endif
        }
    }
}