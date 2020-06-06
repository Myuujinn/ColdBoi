using System;

namespace ColdBoi.CPU.Instructions
{
    public class LdAFF00N : Instruction
    {
        public const byte OPCODE = 0xf0;
        public const string NAME = "ld";
        private const ushort BASE_ADDRESS = 0xff00;

        public LdAFF00N(Processor processor) : base(processor, OPCODE, 1, 12, NAME)
        {
        }

        public override void Execute(params byte[] operands)
        {
            var offset = operands[0];
            this.processor.Registers.AF.HigherByte = this.processor.Memory.Content[BASE_ADDRESS + offset];

#if DEBUG
            Console.WriteLine($"{this.processor.Registers.PC.Value:X4}: {this.Name} a, (FF00 + {offset:X2})");
#endif
        }
    }
}