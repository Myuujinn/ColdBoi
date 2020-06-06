using System;

namespace ColdBoi.CPU.Instructions
{
    public class DecHl : Instruction
    {
        public const byte OPCODE = 0x2b;
        public const string NAME = "dec";

        public DecHl(Processor processor) : base(processor, OPCODE, 0, 8, NAME)
        {
        }

        public override void Execute(params byte[] operands)
        {
            this.processor.Registers.HL.Value -= 1;
            
#if DEBUG
            Console.WriteLine($"{this.processor.Registers.PC.Value:X4}: {this.Name} hl");
#endif
        }
    }
}