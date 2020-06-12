using System;

namespace ColdBoi.CPU.Instructions
{
    public class Ei : Instruction
    {
        public const byte OPCODE = 0xfb;
        public const string NAME = "ei";

        public Ei(Processor processor) : base(processor, OPCODE, 0, 4, NAME)
        {
        }

        public override void Execute(params byte[] operands)
        {
            this.processor.Interrupts.Master = true;
            
#if DEBUG
            Console.WriteLine($"{this.processor.Registers.PC.Value:X4}: {this.Name}");
#endif
        }
    }
}