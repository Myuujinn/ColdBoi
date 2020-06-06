using System;

namespace ColdBoi.CPU.Instructions
{
    public class LdCHl : Instruction
    {
        public const byte OPCODE = 0x4e;
        public const string NAME = "ld";

        public LdCHl(Processor processor) : base(processor, OPCODE, 0, 8, NAME)
        {
        }

        public override void Execute(params byte[] operands)
        {
            this.processor.Registers.BC.LowerByte = this.processor.Memory.Content[this.processor.Registers.HL.Value];

#if DEBUG
            Console.WriteLine($"{this.processor.Registers.PC.Value:X4}: {this.Name} c, (hl)");
#endif
        }
    }
}