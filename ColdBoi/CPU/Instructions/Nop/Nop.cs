using System;

namespace ColdBoi.CPU.Instructions
{
    public class Nop : Instruction
    {
        public const byte OPCODE = 0x00;
        public const string NAME = "nop";

        public Nop(Processor processor) : base(processor, OPCODE, 0, 4, NAME)
        {
        }

        public override void Execute(params byte[] operands)
        {
#if DEBUG
            Console.WriteLine($"{this.processor.Registers.PC.Value:X4}: {this.Name}");
#endif
            // do nothing
        }
    }
}