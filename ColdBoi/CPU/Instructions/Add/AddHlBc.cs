using System;

namespace ColdBoi.CPU.Instructions
{
    public class AddHlBc : Instruction
    {
        public const byte OPCODE = 0x09;
        public const string NAME = "add";

        public AddHlBc(Processor processor) : base(processor, OPCODE, 0, 8, NAME)
        {
        }

        public override void Execute(params byte[] operands)
        {
            var value = this.processor.Registers.BC.Value;
            var result = this.processor.Registers.HL.Value + value;
            
            this.processor.Registers.HL.Value = (ushort) result;

            this.processor.Registers.Carry.Value = (result & 0xffff0000) > 0;
            this.processor.Registers.HalfCarry.Value = (result & 0x0f) + (value & 0x0f) > 0x0f;
            this.processor.Registers.Subtract.Value = false;

#if DEBUG
            Console.WriteLine($"{this.processor.Registers.PC.Value:X4}: {this.Name} hl, bc");
#endif
        }
    }
}