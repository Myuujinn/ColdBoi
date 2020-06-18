using System;

namespace ColdBoi.CPU.Instructions
{
    public class SubD : Instruction
    {
        public const byte OPCODE = 0x92;
        public const string NAME = "sub";

        public SubD(Processor processor) : base(processor, OPCODE, 0, 4, NAME)
        {
        }

        public override void Execute(params byte[] operands)
        {
            var value = this.processor.Registers.DE.HigherByte;

            this.processor.Registers.Carry.Value = value > this.processor.Registers.AF.HigherByte;
            this.processor.Registers.HalfCarry.Value = (value & 0x0f) > (this.processor.Registers.AF.HigherByte & 0x0f);
            this.processor.Registers.Subtract.Value = true;

            this.processor.Registers.AF.HigherByte -= value;

            this.processor.Registers.Zero.Value = this.processor.Registers.AF.HigherByte == 0;
#if DEBUG
            Console.WriteLine($"{this.processor.Registers.PC.Value:X4}: {this.Name} d");
#endif
        }
    }
}