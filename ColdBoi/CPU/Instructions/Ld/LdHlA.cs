using System;

namespace ColdBoi.CPU.Instructions
{
    public class LdHlA : Instruction
    {
        public const byte OPCODE = 0x77;
        public const string NAME = "ld";

        public LdHlA(Processor processor) : base(processor, OPCODE, 0, 8, NAME)
        {
        }

        public override void Execute(params byte[] operands)
        {
            this.processor.Memory.Write(this.processor.Memory.Read(this.processor.Registers.HL.Value),
                this.processor.Registers.AF.HigherByte);

#if DEBUG
            Console.WriteLine($"{this.processor.Registers.PC.Value:X4}: {this.Name} (hl), a");
#endif
        }
    }
}