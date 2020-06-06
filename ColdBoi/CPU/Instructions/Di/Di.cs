using System;

namespace ColdBoi.CPU.Instructions
{
    public class Di : Instruction
    {
        public const byte OPCODE = 0xf3;
        public const string NAME = "di";

        public Di(Processor processor) : base(processor, OPCODE, 0, 4, NAME)
        {
        }

        public override void Execute(params byte[] operands)
        {
            this.processor.Memory.InterruptMaster = false;
            
#if DEBUG
            Console.WriteLine($"{this.processor.Registers.PC.Value:X4}: {this.Name}");
#endif
        }
    }
}