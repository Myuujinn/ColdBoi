using System;

namespace ColdBoi.CPU.Instructions
{
    public class AndHl : Instruction
    {
        public const byte OPCODE = 0xa6;
        public const string NAME = "and";

        public AndHl(Processor processor) : base(processor, OPCODE, 2, 8, NAME)
        {
        }

        public override void Execute(params byte[] operands)
        {
            this.processor.Registers.ResetFlags();
            
            var address = (ushort) (operands[0] + (operands[1] << 8));
            this.processor.Registers.AF.HigherByte &= this.processor.Memory.Content[address];

            this.processor.Registers.Zero.Value = this.processor.Registers.AF.HigherByte == 0;
            this.processor.Registers.HalfCarry.Value = true;

#if DEBUG
            Console.WriteLine($"{this.processor.Registers.PC.Value:X4}: {this.Name} (hl)");
#endif
        }
    }
}