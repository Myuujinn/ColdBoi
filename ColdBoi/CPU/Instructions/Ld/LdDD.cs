using System;

namespace ColdBoi.CPU.Instructions
{
    public class LdDD : Instruction
    {
        public const byte OPCODE = 0x52;
        public const string NAME = "ld";

        public LdDD(Processor processor) : base(processor, OPCODE, 0, 4, NAME)
        {
        }

        public override void Execute(params byte[] operands)
        {
            this.processor.Registers.DE.HigherByte = this.processor.Registers.DE.HigherByte;

#if DEBUG
            Console.WriteLine($"{this.processor.Registers.PC.Value:X4}: {this.Name} d, d");
#endif
        }
    }
}