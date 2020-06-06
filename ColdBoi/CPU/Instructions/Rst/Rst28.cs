using System;

namespace ColdBoi.CPU.Instructions
{
    public class Rst28 : Instruction
    {
        public const byte OPCODE = 0xef;
        public const string NAME = "rst";

        public Rst28(Processor processor) : base(processor, OPCODE, 0, 32, NAME)
        {
        }

        public override void Execute(params byte[] operands)
        {
            this.processor.Stack.Push((ushort) (this.processor.Registers.PC.Value + this.Length));
            // push address after this instruction on the stack

#if DEBUG
            Console.WriteLine($"{this.processor.Registers.PC.Value:X4}: {this.Name} 28");
#endif

            this.processor.Registers.PC.Value = (ushort) (0x0028 - this.Length);
            // need to subtract the length now because PC is incremented afterwards
        }
    }
}