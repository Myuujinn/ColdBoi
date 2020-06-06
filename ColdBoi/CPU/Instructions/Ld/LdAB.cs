using System;

namespace ColdBoi.CPU.Instructions
{
    public class LdAB : Instruction
    {
        public const byte OPCODE = 0x78;
        public const string NAME = "ld";

        public LdAB(Processor processor) : base(processor, OPCODE, 0, 4, NAME)
        {
        }

        public override void Execute(params byte[] operands)
        {
            this.processor.Registers.AF.HigherByte = this.processor.Registers.BC.HigherByte;

#if DEBUG
            Console.WriteLine($"{this.processor.Registers.PC.Value:X4}: {this.Name} a, b");
#endif
        }
    }
}