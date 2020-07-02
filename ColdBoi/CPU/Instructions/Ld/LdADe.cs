using System;

namespace ColdBoi.CPU.Instructions
{
    public class LdADe : Instruction
    {
        public const byte OPCODE = 0x1a;
        public const string NAME = "ld";

        public LdADe(Processor processor) : base(processor, OPCODE, 0, 8, NAME)
        {
        }

        public override void Execute(params byte[] operands)
        {
            this.processor.Registers.AF.HigherByte = this.processor.Memory.Read(this.processor.Registers.DE.Value);

#if DEBUG
            Console.WriteLine($"{this.processor.Registers.PC.Value:X4}: {this.Name} a, (de)");
#endif
        }
    }
}