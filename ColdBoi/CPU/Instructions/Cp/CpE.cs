using System;

namespace ColdBoi.CPU.Instructions
{
    public class CpE : Instruction
    {
        public const byte OPCODE = 0xbb;
        public const string NAME = "cp";

        public CpE(Processor processor) : base(processor, OPCODE, 0, 4, NAME)
        {
        }

        public override void Execute(params byte[] operands)
        {
            var value = this.processor.Registers.DE.LowerByte;

            this.processor.Registers.Zero.Value = this.processor.Registers.AF.HigherByte == value;
            this.processor.Registers.Subtract.Value = true;
            this.processor.Registers.HalfCarry.Value = (value & 0x0f) > (this.processor.Registers.AF.HigherByte & 0x0f);
            this.processor.Registers.Carry.Value = this.processor.Registers.AF.HigherByte < value;

#if DEBUG
            Console.WriteLine($"{this.processor.Registers.PC.Value:X4}: {this.Name} e");
#endif
        }
    }
}