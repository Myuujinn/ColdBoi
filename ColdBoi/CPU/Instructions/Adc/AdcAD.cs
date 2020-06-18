using System;

namespace ColdBoi.CPU.Instructions
{
    public class AdcAD : Instruction
    {
        public const byte OPCODE = 0x8a;
        public const string NAME = "adc";

        public AdcAD(Processor processor) : base(processor, OPCODE, 0, 4, NAME)
        {
        }

        public override void Execute(params byte[] operands)
        {
            var value = this.processor.Registers.DE.HigherByte + Convert.ToByte(this.processor.Registers.Carry.Value);
            var result = this.processor.Registers.AF.HigherByte + value;
            
            this.processor.Registers.Carry.Value = (result & 0xff00) > 0;

            this.processor.Registers.AF.HigherByte = (byte) (result & 0xff);
            
            this.processor.Registers.Zero.Value = this.processor.Registers.AF.HigherByte == 0;
            this.processor.Registers.HalfCarry.Value = (result & 0x0f) + (value & 0x0f) > 0x0f;
            this.processor.Registers.Subtract.Value = false;

#if DEBUG
            Console.WriteLine($"{this.processor.Registers.PC.Value:X4}: {this.Name} a, d");
#endif
        }
    }
}