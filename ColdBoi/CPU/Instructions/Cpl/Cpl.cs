using System;

namespace ColdBoi.CPU.Instructions
{
    public class Cpl : Instruction
    {
        public const byte OPCODE = 0x2f;
        public const string NAME = "cpl";

        public Cpl(Processor processor) : base(processor, OPCODE, 0, 4, NAME)
        {
        }

        public override void Execute(params byte[] operands)
        {
            this.processor.Registers.AF.HigherByte = (byte) ~this.processor.Registers.AF.HigherByte;

            this.processor.Registers.Subtract.Value = true;
            this.processor.Registers.HalfCarry.Value = true;
#if DEBUG
            Console.WriteLine($"{this.processor.Registers.PC.Value:X4}: {this.Name}");
#endif
        }
    }
}