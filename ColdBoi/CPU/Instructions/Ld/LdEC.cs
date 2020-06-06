using System;

namespace ColdBoi.CPU.Instructions
{
    public class LdEC : Instruction
    {
        public const byte OPCODE = 0x59;
        public const string NAME = "ld";

        public LdEC(Processor processor) : base(processor, OPCODE, 0, 4, NAME)
        {
        }

        public override void Execute(params byte[] operands)
        {
            this.processor.Registers.DE.LowerByte = this.processor.Registers.BC.LowerByte;

#if DEBUG
            Console.WriteLine($"{this.processor.Registers.PC.Value:X4}: {this.Name} e, c");
#endif
        }
    }
}