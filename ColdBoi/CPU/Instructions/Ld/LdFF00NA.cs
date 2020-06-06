using System;

namespace ColdBoi.CPU.Instructions
{
    public class LdFF00NA : Instruction
    {
        public const byte OPCODE = 0xe0;
        public const string NAME = "ld";
        private const ushort BASE_ADDRESS = 0xff00;

        public LdFF00NA(Processor processor) : base(processor, OPCODE, 1, 12, NAME)
        {
        }

        public override void Execute(params byte[] operands)
        {
            var offset = operands[0];
            this.processor.Memory.Write(BASE_ADDRESS + offset, this.processor.Registers.AF.HigherByte);

#if DEBUG
            Console.WriteLine($"{this.processor.Registers.PC.Value:X4}: {this.Name} (FF00 + {offset:X2}), a");
#endif
        }
    }
}