using System;

namespace ColdBoi.CPU.Instructions
{
    public class LdBHl : Instruction
    {
        public const byte OPCODE = 0x46;
        public const string NAME = "ld";

        public LdBHl(Processor processor) : base(processor, OPCODE, 0, 8, NAME)
        {
        }

        public override void Execute(params byte[] operands)
        {
            this.processor.Registers.BC.HigherByte = this.processor.Memory.Read(this.processor.Registers.HL.Value);

#if DEBUG
            Console.WriteLine($"{this.processor.Registers.PC.Value:X4}: {this.Name} b, (hl)");
#endif
        }
    }
}