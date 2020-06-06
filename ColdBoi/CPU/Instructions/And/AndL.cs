using System;

namespace ColdBoi.CPU.Instructions
{
    public class AndL : Instruction
    {
        public const byte OPCODE = 0xa5;
        public const string NAME = "and";

        public AndL(Processor processor) : base(processor, OPCODE, 0, 4, NAME)
        {
        }

        public override void Execute(params byte[] operands)
        {
            this.processor.Registers.ResetFlags();
            
            this.processor.Registers.AF.HigherByte &= this.processor.Registers.HL.LowerByte;

            this.processor.Registers.Zero.Value = this.processor.Registers.AF.HigherByte == 0;
            this.processor.Registers.HalfCarry.Value = true;

#if DEBUG
            Console.WriteLine($"{this.processor.Registers.PC.Value:X4}: {this.Name} l");
#endif
        }
    }
}