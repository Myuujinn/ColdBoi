using System;

namespace ColdBoi.CPU.Instructions
{
    public class JrN : Instruction
    {
        public const byte OPCODE = 0x18;
        public const string NAME = "jr";

        public JrN(Processor processor) : base(processor, OPCODE, 1, 12, NAME)
        {
        }

        public override void Execute(params byte[] operands)
        {
            var value = (sbyte) operands[0];

#if DEBUG
            Console.WriteLine(
                $"{this.processor.Registers.PC.Value:X4}: {this.Name} {this.processor.Registers.PC.Value + value + this.Length:X4}");
#endif

            this.processor.Registers.PC.Value += (ushort) value;
        }
    }
}