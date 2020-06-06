using System;

namespace ColdBoi.CPU.Instructions
{
    public class LdANn : Instruction
    {
        public const byte OPCODE = 0xfa;
        public const string NAME = "ld";

        public LdANn(Processor processor) : base(processor, OPCODE, 2, 16, NAME)
        {
        }

        public override void Execute(params byte[] operands)
        {
            var address = (ushort) (operands[0] + (operands[1] << 8));
            this.processor.Registers.AF.HigherByte = this.processor.Memory.Content[address];

#if DEBUG
            Console.WriteLine($"{this.processor.Registers.PC.Value:X4}: {this.Name} a, ({address:X4})");
#endif
        }
    }
}