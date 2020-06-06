using System;

namespace ColdBoi.CPU.Instructions
{
    public class LdBB : Instruction
    {
        public const byte OPCODE = 0x40;
        public const string NAME = "ld";

        public LdBB(Processor processor) : base(processor, OPCODE, 0, 4, NAME)
        {
        }

        public override void Execute(params byte[] operands)
        {
            this.processor.Registers.BC.HigherByte = this.processor.Registers.BC.HigherByte;

#if DEBUG
            Console.WriteLine($"{this.processor.Registers.PC.Value:X4}: {this.Name} b, b");
#endif
        }
    }
}