using System;

namespace ColdBoi.CPU.Instructions
{
    public class LdDHl : Instruction
    {
        public const byte OPCODE = 0x56;
        public const string NAME = "ld";

        public LdDHl(Processor processor) : base(processor, OPCODE, 0, 8, NAME)
        {
        }

        public override void Execute(params byte[] operands)
        {
            this.processor.Registers.DE.HigherByte = this.processor.Memory.Content[this.processor.Registers.HL.Value];

#if DEBUG
            Console.WriteLine($"{this.processor.Registers.PC.Value:X4}: {this.Name} d, (hl)");
#endif
        }
    }
}