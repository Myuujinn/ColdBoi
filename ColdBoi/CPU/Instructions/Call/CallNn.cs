using System;

namespace ColdBoi.CPU.Instructions
{
    public class CallNn : Instruction
    {
        public const byte OPCODE = 0xcd;
        public const string NAME = "call";

        public CallNn(Processor processor) : base(processor, OPCODE, 2, 12, NAME)
        {
        }

        public override void Execute(params byte[] operands)
        {
            this.processor.Stack.Push((ushort) (this.processor.Registers.PC.Value + this.Length));
            
            var address = (ushort) (operands[0] + (operands[1] << 8));
            
#if DEBUG
            Console.WriteLine($"{this.processor.Registers.PC.Value:X4}: {this.Name} {address:X4}");
#endif
            
            this.processor.Registers.PC.Value =
                (ushort) (address - this.Length); // need to subtract the length now because PC is incremented afterwards
        }
    }
}