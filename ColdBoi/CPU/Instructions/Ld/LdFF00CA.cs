using System;

namespace ColdBoi.CPU.Instructions
{
    public class LdFF00CA : Instruction
    {
        public const byte OPCODE = 0xe2;
        public const string NAME = "ld";
        private const ushort BASE_ADDRESS = 0xff00;

        public LdFF00CA(Processor processor) : base(processor, OPCODE, 0, 8, NAME)
        {
        }

        public override void Execute(params byte[] operands)
        {
            this.processor.Memory.Write((ushort) (BASE_ADDRESS + this.processor.Registers.BC.LowerByte),
                this.processor.Registers.AF.HigherByte);

#if DEBUG
            Console.WriteLine($"{this.processor.Registers.PC.Value:X4}: {this.Name} (FF00 + c), a");
#endif
        }
    }
}