using System;

namespace ColdBoi.CPU.Instructions
{
    public class AddAA : Instruction
    {
        public const byte OPCODE = 0x87;
        public const string NAME = "add";

        public AddAA(Processor processor) : base(processor, OPCODE, 0, 4, NAME)
        {
        }

        public override void Execute(params byte[] operands)
        {
            var value = this.processor.Registers.AF.HigherByte;
            var result = this.processor.Registers.AF.HigherByte + value;
            
            this.processor.Registers.Carry.Value = (result & 0xff00) > 0;

            this.processor.Registers.AF.HigherByte = (byte) (result & 0xff);
            
            this.processor.Registers.Zero.Value = this.processor.Registers.AF.HigherByte == 0;
            this.processor.Registers.HalfCarry.Value = (result & 0x0f) + (value & 0x0f) > 0x0f;
            this.processor.Registers.Subtract.Value = false;

#if DEBUG
            Console.WriteLine($"{this.processor.Registers.PC.Value:X4}: {this.Name} a, a");
#endif
        }
    }
}