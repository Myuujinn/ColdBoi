using System;

namespace ColdBoi.CPU.Instructions
{
    public class DecSp : Instruction
    {
        public const byte OPCODE = 0x3b;
        public const string NAME = "dec";

        public DecSp(Processor processor) : base(processor, OPCODE, 0, 8, NAME)
        {
        }

        public override void Execute(params byte[] operands)
        {
            this.processor.Registers.SP.Value -= 1;
            
#if DEBUG
            Console.WriteLine($"{this.processor.Registers.PC.Value:X4}: {this.Name} sp");
#endif
        }
    }
}