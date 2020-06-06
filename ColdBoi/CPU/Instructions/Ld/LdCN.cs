using System;

namespace ColdBoi.CPU.Instructions
{
    public class LdCN : Instruction
    {
        public const byte OPCODE = 0x0e;
        public const string NAME = "ld";

        public LdCN(Processor processor) : base(processor, OPCODE, 1, 8, NAME)
        {
        }

        public override void Execute(params byte[] operands)
        {
            var value = operands[0];
            this.processor.Registers.BC.LowerByte = value;

#if DEBUG
            Console.WriteLine($"{this.processor.Registers.PC.Value:X4}: {this.Name} c, {value:X2}");
#endif
        }
    }
}