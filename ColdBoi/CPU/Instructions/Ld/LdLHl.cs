using System;

namespace ColdBoi.CPU.Instructions
{
    public class LdLHl : Instruction
    {
        public const byte OPCODE = 0x6e;
        public const string NAME = "ld";

        public LdLHl(Processor processor) : base(processor, OPCODE, 0, 8, NAME)
        {
        }

        public override void Execute(params byte[] operands)
        {
            this.processor.Registers.HL.LowerByte = this.processor.Memory.Read(this.processor.Registers.HL.Value);

#if DEBUG
            Console.WriteLine($"{this.processor.Registers.PC.Value:X4}: {this.Name} l, (hl)");
#endif
        }
    }
}