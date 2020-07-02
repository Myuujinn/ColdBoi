using System;

namespace ColdBoi.CPU.Instructions
{
    public class LdABc : Instruction
    {
        public const byte OPCODE = 0x0a;
        public const string NAME = "ld";

        public LdABc(Processor processor) : base(processor, OPCODE, 0, 8, NAME)
        {
        }

        public override void Execute(params byte[] operands)
        {
            this.processor.Registers.AF.HigherByte = this.processor.Memory.Read(this.processor.Registers.BC.Value);

#if DEBUG
            Console.WriteLine($"{this.processor.Registers.PC.Value:X4}: {this.Name} a, (bc)");
#endif
        }
    }
}