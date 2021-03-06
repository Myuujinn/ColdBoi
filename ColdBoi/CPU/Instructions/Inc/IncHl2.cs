using System;

namespace ColdBoi.CPU.Instructions
{
    public class IncHl2 : Instruction
    {
        public const byte OPCODE = 0x34;
        public const string NAME = "inc";

        public IncHl2(Processor processor) : base(processor, OPCODE, 0, 4, NAME)
        {
        }

        public override void Execute(params byte[] operands)
        {
            var value = this.processor.Memory.Read(this.processor.Registers.HL.Value);
            
            this.processor.Registers.HalfCarry.Value = (value & 0x0f) == 0x0f;

            value += 1;
            this.processor.Memory.Write(this.processor.Registers.HL.Value, value);

            this.processor.Registers.Zero.Value = value == 0;
            this.processor.Registers.Subtract.Value = false;

#if DEBUG
            Console.WriteLine($"{this.processor.Registers.PC.Value:X4}: {this.Name} (hl)");
#endif
        }
    }
}