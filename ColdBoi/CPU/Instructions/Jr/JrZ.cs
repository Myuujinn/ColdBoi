using System;

namespace ColdBoi.CPU.Instructions
{
    public class JrZ : Instruction
    {
        public const byte CYCLES_NO_JUMP = 8;
        public const byte CYCLES_JUMP = 12;
        public const byte OPCODE = 0x28;
        public const string NAME = "jr";

        public JrZ(Processor processor) : base(processor, OPCODE, 1, CYCLES_NO_JUMP, NAME)
        {
        }

        public override void Execute(params byte[] operands)
        {
            var value = (sbyte) operands[0];

#if DEBUG
            Console.WriteLine(
                $"{this.processor.Registers.PC.Value:X4}: {this.Name} z, {this.processor.Registers.PC.Value + value + this.Length:X4}");
#endif

            if (this.processor.Registers.Zero.Value)
            {
                this.processor.Registers.PC.Value += (ushort) value;
                this.Cycles = CYCLES_JUMP;
            }
            else
            {
                this.Cycles = CYCLES_NO_JUMP;
            }
        }
    }
}