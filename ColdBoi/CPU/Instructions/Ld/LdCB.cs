using System;

namespace ColdBoi.CPU.Instructions
{
    public class LdCB : Instruction
    {
        public const byte OPCODE = 0x48;
        public const string NAME = "ld";

        public LdCB(Processor processor) : base(processor, OPCODE, 0, 4, NAME)
        {
        }

        public override void Execute(params byte[] operands)
        {
            this.processor.Registers.BC.LowerByte = this.processor.Registers.BC.HigherByte;

#if DEBUG
            Console.WriteLine($"{this.processor.Registers.PC.Value:X4}: {this.Name} c, b");
#endif
        }
    }
}