using System;

namespace ColdBoi.CPU.Instructions
{
    public class LdBcNn : Instruction
    {
        public const byte OPCODE = 0x01;
        public const string NAME = "ld";

        public LdBcNn(Processor processor) : base(processor, OPCODE, 2, 12, NAME)
        {
        }

        public override void Execute(params byte[] operands)
        {
            var value = (ushort) (operands[0] + (operands[1] << 8));
            this.processor.Registers.BC.Value = value;

#if DEBUG
            Console.WriteLine($"{this.processor.Registers.PC.Value:X4}: {this.Name} hl, {value:X4}");
#endif
        }
    }
}