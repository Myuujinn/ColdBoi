using System;

namespace ColdBoi.CPU.Instructions
{
    public class LddHlA : Instruction
    {
        public const byte OPCODE = 0x32;
        public const string NAME = "ldd";

        public LddHlA(Processor processor) : base(processor, OPCODE, 0, 8, NAME)
        {
        }

        public override void Execute(params byte[] operands)
        {
            this.processor.Memory.Write(this.processor.Registers.HL.Value, this.processor.Registers.AF.HigherByte);
            this.processor.Registers.HL.Value -= 1;
#if DEBUG
            Console.WriteLine($"{this.processor.Registers.PC.Value:X4}: {this.Name} (hl), a");
#endif
        }
    }
}