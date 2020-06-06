using System;

namespace ColdBoi.CPU.Instructions
{
    public class LdBC : Instruction
    {
        public const byte OPCODE = 0x41;
        public const string NAME = "ld";

        public LdBC(Processor processor) : base(processor, OPCODE, 0, 4, NAME)
        {
        }

        public override void Execute(params byte[] operands)
        {
            this.processor.Registers.BC.HigherByte = this.processor.Registers.BC.LowerByte;

#if DEBUG
            Console.WriteLine($"{this.processor.Registers.PC.Value:X4}: {this.Name} b, c");
#endif
        }
    }
}