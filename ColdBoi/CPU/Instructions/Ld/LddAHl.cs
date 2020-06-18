using System;

namespace ColdBoi.CPU.Instructions
{
    public class LddAHl : Instruction
    {
        public const byte OPCODE = 0x3a;
        public const string NAME = "ldd";

        public LddAHl(Processor processor) : base(processor, OPCODE, 0, 8, NAME)
        {
        }

        public override void Execute(params byte[] operands)
        {
            this.processor.Registers.AF.HigherByte = this.processor.Memory.Content[this.processor.Registers.HL.Value];
            this.processor.Registers.HL.Value -= 1;
            
#if DEBUG
            Console.WriteLine($"{this.processor.Registers.PC.Value:X4}: {this.Name} a, (hl)");
#endif
        }
    }
}