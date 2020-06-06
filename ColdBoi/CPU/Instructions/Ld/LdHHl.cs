using System;

namespace ColdBoi.CPU.Instructions
{
    public class LdHHl : Instruction
    {
        public const byte OPCODE = 0x66;
        public const string NAME = "ld";

        public LdHHl(Processor processor) : base(processor, OPCODE, 0, 8, NAME)
        {
        }

        public override void Execute(params byte[] operands)
        {
            this.processor.Registers.HL.HigherByte = this.processor.Memory.Content[this.processor.Registers.HL.Value];

#if DEBUG
            Console.WriteLine($"{this.processor.Registers.PC.Value:X4}: {this.Name} h, (hl)");
#endif
        }
    }
}