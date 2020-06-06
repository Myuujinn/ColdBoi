using System;

namespace ColdBoi.CPU.Instructions
{
    public class JpHl : Instruction
    {
        public const byte OPCODE = 0xe9;
        public const string NAME = "jp";

        public JpHl(Processor processor) : base(processor, OPCODE, 0, 4, NAME)
        {
        }

        public override void Execute(params byte[] operands)
        {
#if DEBUG
            Console.WriteLine($"{this.processor.Registers.PC.Value:X4}: {this.Name} (hl)");
#endif
            
            var address = this.processor.Memory.Content[this.processor.Registers.HL.Value];
            this.processor.Registers.PC.Value = (ushort) (address - this.Length);
            // need to subtract the length now because PC is incremented afterwards
        }
    }
}