using System;

namespace ColdBoi.CPU.BigInstructions
{
    public class SrlHl : BigInstruction
    {
        public const ushort OPCODE = 0xcb3e;
        public const string NAME = "sla";

        public SrlHl(Processor processor) : base(processor, OPCODE, 0, 16, NAME)
        {
        }

        public override void Execute(params byte[] operands)
        {
            this.processor.Registers.ResetFlags();
            
            var value = this.processor.Memory.Read(this.processor.Registers.HL.Value);
            
            this.processor.Registers.Carry.Value = (value & 0x01) > 0;
            
            value >>= 1;
            this.processor.Memory.Write(this.processor.Registers.HL.Value, value);

            this.processor.Registers.Zero.Value = value == 0;
            
#if DEBUG
            Console.WriteLine($"{this.processor.Registers.PC.Value:X4}: {this.Name} (hl)");
#endif
        }
    }
}