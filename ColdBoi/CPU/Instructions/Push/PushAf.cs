using System;

namespace ColdBoi.CPU.Instructions
{
    public class PushAf : Instruction
    {
        public const byte OPCODE = 0xf5;
        public const string NAME = "push";

        public PushAf(Processor processor) : base(processor, OPCODE, 0, 16, NAME)
        {
        }

        public override void Execute(params byte[] operands)
        {
            this.processor.Stack.Push(this.processor.Registers.AF.Value);
            
#if DEBUG
            Console.WriteLine($"{this.processor.Registers.PC.Value:X4}: {this.Name} af");
#endif
        }
    }
}