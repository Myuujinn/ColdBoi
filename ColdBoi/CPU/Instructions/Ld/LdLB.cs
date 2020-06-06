using System;

namespace ColdBoi.CPU.Instructions
{
    public class LdLB : Instruction
    {
        public const byte OPCODE = 0x68;
        public const string NAME = "ld";

        public LdLB(Processor processor) : base(processor, OPCODE, 0, 4, NAME)
        {
        }

        public override void Execute(params byte[] operands)
        {
            this.processor.Registers.HL.LowerByte = this.processor.Registers.BC.HigherByte;

#if DEBUG
            Console.WriteLine($"{this.processor.Registers.PC.Value:X4}: {this.Name} l, b");
#endif
        }
    }
}