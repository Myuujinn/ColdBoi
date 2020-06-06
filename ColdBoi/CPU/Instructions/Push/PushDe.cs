using System;

namespace ColdBoi.CPU.Instructions
{
    public class PushDe : Instruction
    {
        public const byte OPCODE = 0xd5;
        public const string NAME = "push";

        public PushDe(Processor processor) : base(processor, OPCODE, 0, 16, NAME)
        {
        }

        public override void Execute(params byte[] operands)
        {
            this.processor.Stack.Push(this.processor.Registers.DE.Value);
            
#if DEBUG
            Console.WriteLine($"{this.processor.Registers.PC.Value:X4}: {this.Name} de");
#endif
        }
    }
}