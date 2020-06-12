using System;

namespace ColdBoi.CPU.Instructions
{
    public class IncSp : Instruction
    {
        public const byte OPCODE = 0x33;
        public const string NAME = "inc";

        public IncSp(Processor processor) : base(processor, OPCODE, 0, 8, NAME)
        {
        }

        public override void Execute(params byte[] operands)
        {
            this.processor.Registers.SP.Value += 1;

#if DEBUG
            Console.WriteLine($"{this.processor.Registers.PC.Value:X4}: {this.Name} sp");
#endif
        }
    }
}