using System;

namespace ColdBoi.CPU.Instructions
{
    public class LdBA : Instruction
    {
        public const byte OPCODE = 0x47;
        public const string NAME = "ld";

        public LdBA(Processor processor) : base(processor, OPCODE, 0, 4, NAME)
        {
        }

        public override void Execute(params byte[] operands)
        {
            this.processor.Registers.BC.HigherByte = this.processor.Registers.AF.HigherByte;

#if DEBUG
            Console.WriteLine($"{this.processor.Registers.PC.Value:X4}: {this.Name} b, a");
#endif
        }
    }
}