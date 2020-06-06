using System;

namespace ColdBoi.CPU.Instructions
{
    public class PopHl : Instruction
    {
        public const byte OPCODE = 0xe1;
        public const string NAME = "pop";

        public PopHl(Processor processor) : base(processor, OPCODE, 0, 12, NAME)
        {
        }

        public override void Execute(params byte[] operands)
        {
            this.processor.Registers.HL.Value = this.processor.Stack.Pop();
            
#if DEBUG
            Console.WriteLine($"{this.processor.Registers.PC.Value:X4}: {this.Name} hl");
#endif
        }
    }
}