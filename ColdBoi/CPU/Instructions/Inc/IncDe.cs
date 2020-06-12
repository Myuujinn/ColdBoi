using System;

namespace ColdBoi.CPU.Instructions
{
    public class IncDe : Instruction
    {
        public const byte OPCODE = 0x13;
        public const string NAME = "inc";

        public IncDe(Processor processor) : base(processor, OPCODE, 0, 8, NAME)
        {
        }

        public override void Execute(params byte[] operands)
        {
            this.processor.Registers.DE.Value += 1;

#if DEBUG
            Console.WriteLine($"{this.processor.Registers.PC.Value:X4}: {this.Name} de");
#endif
        }
    }
}