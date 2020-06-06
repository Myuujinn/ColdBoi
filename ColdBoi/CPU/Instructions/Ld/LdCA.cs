using System;

namespace ColdBoi.CPU.Instructions
{
    public class LdCA : Instruction
    {
        public const byte OPCODE = 0x4f;
        public const string NAME = "ld";

        public LdCA(Processor processor) : base(processor, OPCODE, 0, 4, NAME)
        {
        }

        public override void Execute(params byte[] operands)
        {
            this.processor.Registers.BC.LowerByte = this.processor.Registers.AF.HigherByte;

#if DEBUG
            Console.WriteLine($"{this.processor.Registers.PC.Value:X4}: {this.Name} c, a");
#endif
        }
    }
}