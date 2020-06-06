using System;

namespace ColdBoi.CPU.Instructions
{
    public class LdCE : Instruction
    {
        public const byte OPCODE = 0x4b;
        public const string NAME = "ld";

        public LdCE(Processor processor) : base(processor, OPCODE, 0, 4, NAME)
        {
        }

        public override void Execute(params byte[] operands)
        {
            this.processor.Registers.BC.LowerByte = this.processor.Registers.DE.LowerByte;

#if DEBUG
            Console.WriteLine($"{this.processor.Registers.PC.Value:X4}: {this.Name} c, e");
#endif
        }
    }
}