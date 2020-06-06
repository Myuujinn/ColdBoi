using System;

namespace ColdBoi.CPU.BigInstructions
{
    public class SlaD : BigInstruction
    {
        public const ushort OPCODE = 0xcb22;
        public const string NAME = "sla";

        public SlaD(Processor processor) : base(processor, OPCODE, 0, 8, NAME)
        {
        }

        public override void Execute(params byte[] operands)
        {
            this.processor.Registers.ResetFlags();

            this.processor.Registers.Carry.Value = (this.processor.Registers.DE.HigherByte & 0x80) > 0;
            
            this.processor.Registers.DE.HigherByte <<= 1;

            this.processor.Registers.Zero.Value = this.processor.Registers.DE.HigherByte == 0;
#if DEBUG
            Console.WriteLine($"{this.processor.Registers.PC.Value:X4}: {this.Name} d");
#endif
        }
    }
}