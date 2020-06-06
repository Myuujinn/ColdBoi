using System;

namespace ColdBoi.CPU.BigInstructions
{
    public class SlaE : BigInstruction
    {
        public const ushort OPCODE = 0xcb23;
        public const string NAME = "sla";

        public SlaE(Processor processor) : base(processor, OPCODE, 0, 8, NAME)
        {
        }

        public override void Execute(params byte[] operands)
        {
            this.processor.Registers.ResetFlags();

            this.processor.Registers.Carry.Value = (this.processor.Registers.DE.LowerByte & 0x80) > 0;
            
            this.processor.Registers.DE.LowerByte <<= 1;

            this.processor.Registers.Zero.Value = this.processor.Registers.DE.LowerByte == 0;
#if DEBUG
            Console.WriteLine($"{this.processor.Registers.PC.Value:X4}: {this.Name} e");
#endif
        }
    }
}