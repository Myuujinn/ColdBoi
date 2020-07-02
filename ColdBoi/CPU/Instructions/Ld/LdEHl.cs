using System;

namespace ColdBoi.CPU.Instructions
{
    public class LdEHl : Instruction
    {
        public const byte OPCODE = 0x5e;
        public const string NAME = "ld";

        public LdEHl(Processor processor) : base(processor, OPCODE, 0, 8, NAME)
        {
        }

        public override void Execute(params byte[] operands)
        {
            this.processor.Registers.DE.LowerByte = this.processor.Memory.Read(this.processor.Registers.HL.Value);

#if DEBUG
            Console.WriteLine($"{this.processor.Registers.PC.Value:X4}: {this.Name} e, (hl)");
#endif
        }
    }
}