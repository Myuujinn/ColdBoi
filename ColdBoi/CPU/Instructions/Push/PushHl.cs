using System;

namespace ColdBoi.CPU.Instructions
{
    public class PushHl : Instruction
    {
        public const byte OPCODE = 0xe5;
        public const string NAME = "push";

        public PushHl(Processor processor) : base(processor, OPCODE, 0, 16, NAME)
        {
        }

        public override void Execute(params byte[] operands)
        {
            this.processor.Stack.Push(this.processor.Registers.HL.Value);
            
#if DEBUG
            Console.WriteLine($"{this.processor.Registers.PC.Value:X4}: {this.Name} hl");
#endif
        }
    }
}