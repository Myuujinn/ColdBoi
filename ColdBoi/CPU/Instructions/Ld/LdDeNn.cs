using System;

namespace ColdBoi.CPU.Instructions
{
    public class LdDeNn : Instruction
    {
        public const byte OPCODE = 0x11;
        public const string NAME = "ld";

        public LdDeNn(Processor processor) : base(processor, OPCODE, 2, 12, NAME)
        {
        }

        public override void Execute(params byte[] operands)
        {
            var value = (ushort) (operands[0] + (operands[1] << 8));
            this.processor.Registers.DE.Value = value;

#if DEBUG
            Console.WriteLine($"{this.processor.Registers.PC.Value:X4}: {this.Name} de, {value:X4}");
#endif
        }
    }
}