using System;

namespace ColdBoi.CPU.Instructions
{
    public class LdCL : Instruction
    {
        public const byte OPCODE = 0x4d;
        public const string NAME = "ld";

        public LdCL(Processor processor) : base(processor, OPCODE, 0, 4, NAME)
        {
        }

        public override void Execute(params byte[] operands)
        {
            this.processor.Registers.BC.LowerByte = this.processor.Registers.HL.LowerByte;

#if DEBUG
            Console.WriteLine($"{this.processor.Registers.PC.Value:X4}: {this.Name} c, l");
#endif
        }
    }
}