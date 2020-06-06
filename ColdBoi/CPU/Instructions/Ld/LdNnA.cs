using System;

namespace ColdBoi.CPU.Instructions
{
    public class LdNnA : Instruction
    {
        public const byte OPCODE = 0xea;
        public const string NAME = "ld";

        public LdNnA(Processor processor) : base(processor, OPCODE, 2, 12, NAME)
        {
        }

        public override void Execute(params byte[] operands)
        {
            var address = (ushort) (operands[0] + (operands[1] << 8));
            this.processor.Memory.Write(address, this.processor.Registers.AF.HigherByte);

#if DEBUG
            Console.WriteLine($"{this.processor.Registers.PC.Value:X4}: {this.Name} ({address:X4}), a");
#endif
        }
    }
}