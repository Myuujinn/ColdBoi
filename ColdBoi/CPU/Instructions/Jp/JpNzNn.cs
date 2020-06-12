using System;

namespace ColdBoi.CPU.Instructions
{
    public class JpNzNn : Instruction
    {
        public const byte OPCODE = 0xc2;
        public const string NAME = "jp";

        public JpNzNn(Processor processor) : base(processor, OPCODE, 2, 12, NAME)
        {
        }

        public override void Execute(params byte[] operands)
        {
            var address = (ushort) (operands[0] + (operands[1] << 8));
#if DEBUG
            Console.WriteLine($"{this.processor.Registers.PC.Value:X4}: {this.Name} nz, {address:X4}");
#endif

            if (this.processor.Registers.Zero.Value)
                return;
            
            this.processor.Registers.PC.Value = (ushort) (address - this.Length);
            // need to subtract the length now because PC is incremented afterwards
        }
    }
}