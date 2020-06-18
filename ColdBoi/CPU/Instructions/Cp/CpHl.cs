using System;

namespace ColdBoi.CPU.Instructions
{
    public class CpHl : Instruction
    {
        public const byte OPCODE = 0xbe;
        public const string NAME = "cp";

        public CpHl(Processor processor) : base(processor, OPCODE, 1, 8, NAME)
        {
        }

        public override void Execute(params byte[] operands)
        {
            var value = this.processor.Memory.Content[this.processor.Registers.HL.Value];

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