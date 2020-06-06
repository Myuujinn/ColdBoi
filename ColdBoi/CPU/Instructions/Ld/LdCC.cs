using System;

namespace ColdBoi.CPU.Instructions
{
    public class LdCC : Instruction
    {
        public const byte OPCODE = 0x49;
        public const string NAME = "ld";

        public LdCC(Processor processor) : base(processor, OPCODE, 0, 4, NAME)
        {
        }

        public override void Execute(params byte[] operands)
        {
            this.processor.Registers.BC.LowerByte = this.processor.Registers.BC.LowerByte;

#if DEBUG
            Console.WriteLine($"{this.processor.Registers.PC.Value:X4}: {this.Name} c, c");
#endif
        }
    }
}