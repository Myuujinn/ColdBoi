using System;

namespace ColdBoi.CPU.Instructions
{
    public class RetC : Instruction
    {
        public const byte OPCODE = 0xd8;
        public const string NAME = "ret";

        public RetC(Processor processor) : base(processor, OPCODE, 0, 8, NAME)
        {
        }

        public override void Execute(params byte[] operands)
        {
#if DEBUG
            Console.WriteLine($"{this.processor.Registers.PC.Value:X4}: {this.Name} c");
#endif
            if (!this.processor.Registers.Carry.Value)
                return;

            var address = this.processor.Stack.Pop();
            this.processor.Registers.PC.Value = (ushort) (address - this.Length);
            // need to subtract the length now because PC is incremented afterwards
        }
    }
}