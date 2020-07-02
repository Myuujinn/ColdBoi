using System;

namespace ColdBoi.CPU.BigInstructions
{
    public class SetHl : BigInstruction
    {
        public const string NAME = "set";

        private int bitNumber;

        public SetHl(Processor processor, ushort opCode, int bitNumber) : base(processor, opCode, 0, 16, NAME)
        {
            this.bitNumber = bitNumber;
        }

        public override void Execute(params byte[] operands)
        {
            var value = this.processor.Memory.Read(this.processor.Registers.HL.Value);
            
            value = global::ColdBoi.Bit.Set(value, this.bitNumber, true);
            
            this.processor.Memory.Write(this.processor.Registers.HL.Value, value);

#if DEBUG
            Console.WriteLine($"{this.processor.Registers.PC.Value:X4}: {this.Name} {this.bitNumber}, (hl)");
#endif
        }
    }
}