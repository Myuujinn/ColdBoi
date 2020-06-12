using System;

namespace ColdBoi.CPU.Instructions
{
    public class LdDeA : Instruction
    {
        public const byte OPCODE = 0x12;
        public const string NAME = "ld";

        public LdDeA(Processor processor) : base(processor, OPCODE, 0, 8, NAME)
        {
        }

        public override void Execute(params byte[] operands)
        {
            this.processor.Memory.Write(this.processor.Registers.DE.Value, this.processor.Registers.AF.HigherByte);

#if DEBUG
            Console.WriteLine($"{this.processor.Registers.PC.Value:X4}: {this.Name} (de), a");
#endif
        }
    }
}