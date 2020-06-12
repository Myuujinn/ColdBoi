using System;

namespace ColdBoi.CPU.Instructions
{
    public class IncBc : Instruction
    {
        public const byte OPCODE = 0x03;
        public const string NAME = "inc";

        public IncBc(Processor processor) : base(processor, OPCODE, 0, 8, NAME)
        {
        }

        public override void Execute(params byte[] operands)
        {
            this.processor.Registers.BC.Value += 1;

#if DEBUG
            Console.WriteLine($"{this.processor.Registers.PC.Value:X4}: {this.Name} bc");
#endif
        }
    }
}