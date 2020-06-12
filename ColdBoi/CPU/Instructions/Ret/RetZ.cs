using System;

namespace ColdBoi.CPU.Instructions
{
    public class RetZ : Instruction
    {
        public const byte OPCODE = 0xc8;
        public const string NAME = "ret";

        public RetZ(Processor processor) : base(processor, OPCODE, 0, 8, NAME)
        {
        }

        public override void Execute(params byte[] operands)
        {
#if DEBUG
            Console.WriteLine($"{this.processor.Registers.PC.Value:X4}: {this.Name} z");
#endif
            if (!this.processor.Registers.Zero.Value)
                return;

            var address = this.processor.Stack.Pop();
            this.processor.Registers.PC.Value = (ushort) (address - this.Length);
            // need to subtract the length now because PC is incremented afterwards
        }
    }
}