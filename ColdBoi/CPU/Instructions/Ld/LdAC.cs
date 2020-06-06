using System;

namespace ColdBoi.CPU.Instructions
{
    public class LdAC : Instruction
    {
        public const byte OPCODE = 0x79;
        public const string NAME = "ld";

        public LdAC(Processor processor) : base(processor, OPCODE, 0, 4, NAME)
        {
        }

        public override void Execute(params byte[] operands)
        {
            this.processor.Registers.AF.HigherByte = this.processor.Registers.BC.LowerByte;

#if DEBUG
            Console.WriteLine($"{this.processor.Registers.PC.Value:X4}: {this.Name} a, c");
#endif
        }
    }
}