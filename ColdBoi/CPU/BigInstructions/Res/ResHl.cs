using System;

namespace ColdBoi.CPU.BigInstructions
{
    public class ResHl : BigInstruction
    {
        public const string NAME = "res";

        private int bitNumber;

        public ResHl(Processor processor, ushort opCode, int bitNumber) : base(processor, opCode, 0, 8, NAME)
        {
            this.bitNumber = bitNumber;
        }

        public override void Execute(params byte[] operands)
        {
            var value = this.processor.Memory.Read(this.processor.Registers.HL.Value);

            value = global::ColdBoi.Bit.Set(value, this.bitNumber, false);

            this.processor.Memory.Write(this.processor.Registers.HL.Value, value);

#if DEBUG
            Console.WriteLine($"{this.processor.Registers.PC.Value:X4}: {this.Name} {this.bitNumber}, (hl)");
#endif
        }
    }
}