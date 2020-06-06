using System;

namespace ColdBoi.CPU.Instructions
{
    public class CpN : Instruction
    {
        public const byte OPCODE = 0xFE;
        public const string NAME = "cp";

        public CpN(Processor processor) : base(processor, OPCODE, 1, 8, NAME)
        {
        }

        public override void Execute(params byte[] operands)
        {
            var value = operands[0];

            this.processor.Registers.Zero.Value = this.processor.Registers.AF.HigherByte == value;
            this.processor.Registers.Subtract.Value = true;
            this.processor.Registers.HalfCarry.Value = (value & 0x0f) > (this.processor.Registers.AF.HigherByte & 0x0f);
            this.processor.Registers.Carry.Value = this.processor.Registers.AF.HigherByte < value;

#if DEBUG
            Console.WriteLine($"{this.processor.Registers.PC.Value:X4}: {this.Name} {value:X2}");
#endif
        }
    }
}