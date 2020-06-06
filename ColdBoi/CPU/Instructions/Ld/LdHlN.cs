using System;

namespace ColdBoi.CPU.Instructions
{
    public class LdHlN : Instruction
    {
        public const byte OPCODE = 0x36;
        public const string NAME = "ld";

        public LdHlN(Processor processor) : base(processor, OPCODE, 1, 12, NAME)
        {
        }

        public override void Execute(params byte[] operands)
        {
            var value = operands[0];
            this.processor.Memory.Write(this.processor.Registers.HL.Value, value);

#if DEBUG
            Console.WriteLine($"{this.processor.Registers.PC.Value:X4}: {this.Name} (hl), {value:X2}");
#endif
        }
    }
}