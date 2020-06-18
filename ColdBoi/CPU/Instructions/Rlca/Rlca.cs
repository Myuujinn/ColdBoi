using System;

namespace ColdBoi.CPU.Instructions
{
    public class Rlca : Instruction
    {
        public const byte OPCODE = 0x07;
        public const string NAME = "rlca";

        public Rlca(Processor processor) : base(processor, OPCODE, 0, 4, NAME)
        {
        }

        public override void Execute(params byte[] operands)
        {
            var carry = (byte) ((this.processor.Registers.AF.HigherByte & 0x80) >> 7);
	
            this.processor.Registers.AF.HigherByte <<= 1;
            this.processor.Registers.AF.HigherByte += carry;

            this.processor.Registers.Zero.Value = this.processor.Registers.AF.HigherByte == 0;
            this.processor.Registers.Subtract.Value = false;
            this.processor.Registers.HalfCarry.Value = false;
            this.processor.Registers.Carry.Value = carry > 0;

#if DEBUG
            Console.WriteLine($"{this.processor.Registers.PC.Value:X4}: {this.Name}");
#endif
        }
    }
}