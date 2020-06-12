using System;

namespace ColdBoi.CPU.Instructions
{
    public class PopAf : Instruction
    {
        public const byte OPCODE = 0xf1;
        public const string NAME = "pop";

        public PopAf(Processor processor) : base(processor, OPCODE, 0, 12, NAME)
        {
        }

        public override void Execute(params byte[] operands)
        {
            this.processor.Registers.AF.Value = this.processor.Stack.Pop();
            
#if DEBUG
            Console.WriteLine($"{this.processor.Registers.PC.Value:X4}: {this.Name} af");
#endif
        }
    }
}