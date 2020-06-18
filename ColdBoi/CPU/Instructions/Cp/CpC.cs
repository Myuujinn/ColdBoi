using System;

namespace ColdBoi.CPU.Instructions
{
    public class CpC : Instruction
    {
        public const byte OPCODE = 0xb9;
        public const string NAME = "cp";

        public CpC(Processor processor) : base(processor, OPCODE, 0, 4, NAME)
        {
        }

        public override void Execute(params byte[] operands)
        {
            var value = this.processor.Registers.BC.LowerByte;

            this.processor.Registers.Zero.Value = this.processor.Registers.AF.HigherByte == value;
            this.processor.Registers.Subtract.Value = true;
            this.processor.Registers.HalfCarry.Value = (value & 0x0f) > (this.processor.Registers.AF.HigherByte & 0x0f);
            this.processor.Registers.Carry.Value = this.processor.Registers.AF.HigherByte < value;

#if DEBUG
            Console.WriteLine($"{this.processor.Registers.PC.Value:X4}: {this.Name} c");
#endif
        }
    }
}