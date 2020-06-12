using System;

namespace ColdBoi.CPU.Instructions
{
    public class LdiHlA : Instruction
    {
        public const byte OPCODE = 0x22;
        public const string NAME = "ldi";

        public LdiHlA(Processor processor) : base(processor, OPCODE, 0, 8, NAME)
        {
        }

        public override void Execute(params byte[] operands)
        {
            this.processor.Memory.Write(this.processor.Registers.HL.Value, this.processor.Registers.AF.HigherByte);
            this.processor.Registers.HL.Value += 1;
            
#if DEBUG
            Console.WriteLine($"{this.processor.Registers.PC.Value:X4}: {this.Name} (hl), a");
#endif
        }
    }
}