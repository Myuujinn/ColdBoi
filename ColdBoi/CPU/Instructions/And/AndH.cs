using System;

namespace ColdBoi.CPU.Instructions
{
    public class AndH : Instruction
    {
        public const byte OPCODE = 0xa4;
        public const string NAME = "and";

        public AndH(Processor processor) : base(processor, OPCODE, 0, 4, NAME)
        {
        }

        public override void Execute(params byte[] operands)
        {
            this.processor.Registers.ResetFlags();
            
            this.processor.Registers.AF.HigherByte &= this.processor.Registers.HL.HigherByte;

            this.processor.Registers.Zero.Value = this.processor.Registers.AF.HigherByte == 0;
            this.processor.Registers.HalfCarry.Value = true;

#if DEBUG
            Console.WriteLine($"{this.processor.Registers.PC.Value:X4}: {this.Name} h");
#endif
        }
    }
}