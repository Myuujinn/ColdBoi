using System;

namespace ColdBoi.CPU.Instructions
{
    public class PopDe : Instruction
    {
        public const byte OPCODE = 0xd1;
        public const string NAME = "pop";

        public PopDe(Processor processor) : base(processor, OPCODE, 0, 12, NAME)
        {
        }

        public override void Execute(params byte[] operands)
        {
            this.processor.Registers.DE.Value = this.processor.Stack.Pop();
            
#if DEBUG
            Console.WriteLine($"{this.processor.Registers.PC.Value:X4}: {this.Name} de");
#endif
        }
    }
}