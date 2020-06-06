using System;

namespace ColdBoi.CPU.Instructions
{
    public class DecBc : Instruction
    {
        public const byte OPCODE = 0x0b;
        public const string NAME = "dec";

        public DecBc(Processor processor) : base(processor, OPCODE, 0, 8, NAME)
        {
        }

        public override void Execute(params byte[] operands)
        {
            this.processor.Registers.BC.Value -= 1;
            
#if DEBUG
            Console.WriteLine($"{this.processor.Registers.PC.Value:X4}: {this.Name} bc");
#endif
        }
    }
}