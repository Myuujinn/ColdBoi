using System;

namespace ColdBoi.CPU.Instructions
{
    public class DecDe : Instruction
    {
        public const byte OPCODE = 0x1b;
        public const string NAME = "dec";

        public DecDe(Processor processor) : base(processor, OPCODE, 0, 8, NAME)
        {
        }

        public override void Execute(params byte[] operands)
        {
            this.processor.Registers.DE.Value -= 1;
            
#if DEBUG
            Console.WriteLine($"{this.processor.Registers.PC.Value:X4}: {this.Name} de");
#endif
        }
    }
}