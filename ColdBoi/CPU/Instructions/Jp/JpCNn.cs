using System;

namespace ColdBoi.CPU.Instructions
{
    public class JpCNn : Instruction
    {
        public const byte OPCODE = 0xda;
        public const string NAME = "jp";

        public JpCNn(Processor processor) : base(processor, OPCODE, 2, 12, NAME)
        {
        }

        public override void Execute(params byte[] operands)
        {
            var address = (ushort) (operands[0] + (operands[1] << 8));
#if DEBUG
            Console.WriteLine($"{this.processor.Registers.PC.Value:X4}: {this.Name} c, {address:X4}");
#endif

            if (!this.processor.Registers.Carry.Value)
                return;
            
            this.processor.Registers.PC.Value = (ushort) (address - this.Length);
            // need to subtract the length now because PC is incremented afterwards
        }
    }
}