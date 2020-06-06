using System;

namespace ColdBoi.CPU.Instructions
{
    public class LdHB : Instruction
    {
        public const byte OPCODE = 0x60;
        public const string NAME = "ld";

        public LdHB(Processor processor) : base(processor, OPCODE, 0, 4, NAME)
        {
        }

        public override void Execute(params byte[] operands)
        {
            this.processor.Registers.HL.HigherByte = this.processor.Registers.BC.HigherByte;

#if DEBUG
            Console.WriteLine($"{this.processor.Registers.PC.Value:X4}: {this.Name} h, b");
#endif
        }
    }
}