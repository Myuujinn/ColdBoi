using System;

namespace ColdBoi.CPU.Instructions
{
    public class LdSpNn : Instruction
    {
        public const byte OPCODE = 0x31;
        public const string NAME = "ld";

        public LdSpNn(Processor processor) : base(processor, OPCODE, 2, 12, NAME)
        {
        }

        public override void Execute(params byte[] operands)
        {
            var value = (ushort) (operands[0] + (operands[1] << 8));
            this.processor.Registers.SP.Value = value;

#if DEBUG
            Console.WriteLine($"{this.processor.Registers.PC.Value:X4}: {this.Name} sp, {value:X4}");
#endif
        }
    }
}