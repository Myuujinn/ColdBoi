using System;

namespace ColdBoi.CPU.Instructions
{
    public class PopBc : Instruction
    {
        public const byte OPCODE = 0xc1;
        public const string NAME = "pop";

        public PopBc(Processor processor) : base(processor, OPCODE, 0, 12, NAME)
        {
        }

        public override void Execute(params byte[] operands)
        {
            this.processor.Registers.BC.Value = this.processor.Stack.Pop();
            
#if DEBUG
            Console.WriteLine($"{this.processor.Registers.PC.Value:X4}: {this.Name} bc");
#endif
        }
    }
}