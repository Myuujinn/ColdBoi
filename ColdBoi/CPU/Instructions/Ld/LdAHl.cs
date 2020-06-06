using System;

namespace ColdBoi.CPU.Instructions
{
    public class LdAHl : Instruction
    {
        public const byte OPCODE = 0x7e;
        public const string NAME = "ld";

        public LdAHl(Processor processor) : base(processor, OPCODE, 0, 8, NAME)
        {
        }

        public override void Execute(params byte[] operands)
        {
            this.processor.Registers.AF.HigherByte = this.processor.Memory.Content[this.processor.Registers.HL.Value];

#if DEBUG
            Console.WriteLine($"{this.processor.Registers.PC.Value:X4}: {this.Name} a, (hl)");
#endif
        }
    }
}