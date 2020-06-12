using System;

namespace ColdBoi.CPU.Instructions
{
    public class LdBcA : Instruction
    {
        public const byte OPCODE = 0x02;
        public const string NAME = "ld";

        public LdBcA(Processor processor) : base(processor, OPCODE, 0, 8, NAME)
        {
        }

        public override void Execute(params byte[] operands)
        {
            this.processor.Memory.Write(this.processor.Memory.Content[this.processor.Registers.BC.Value],
                this.processor.Registers.AF.HigherByte);

#if DEBUG
            Console.WriteLine($"{this.processor.Registers.PC.Value:X4}: {this.Name} (bc), a");
#endif
        }
    }
}