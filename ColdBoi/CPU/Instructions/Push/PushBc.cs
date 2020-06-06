using System;

namespace ColdBoi.CPU.Instructions
{
    public class PushBc : Instruction
    {
        public const byte OPCODE = 0xc5;
        public const string NAME = "push";

        public PushBc(Processor processor) : base(processor, OPCODE, 0, 16, NAME)
        {
        }

        public override void Execute(params byte[] operands)
        {
            this.processor.Stack.Push(this.processor.Registers.BC.Value);
            
#if DEBUG
            Console.WriteLine($"{this.processor.Registers.PC.Value:X4}: {this.Name} bc");
#endif
        }
    }
}