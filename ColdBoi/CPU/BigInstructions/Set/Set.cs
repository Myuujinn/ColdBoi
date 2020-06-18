using System;

namespace ColdBoi.CPU.BigInstructions
{
    public class Set : BigInstruction
    {
        public const string NAME = "set";

        private RegisterPair register;
        private bool isHigherByte;
        private int bitNumber;
        private string registerName;

        public Set(Processor processor, ushort opCode, RegisterPair register, bool isHigherByte, int bitNumber,
            string registerName) : base(processor, opCode, 0, 8, NAME)
        {
            this.register = register;
            this.isHigherByte = isHigherByte;
            this.bitNumber = bitNumber;
            this.registerName = registerName;
        }

        public override void Execute(params byte[] operands)
        {
            if (this.isHigherByte)
                this.register.HigherByte = global::ColdBoi.Bit.Set(this.register.HigherByte, this.bitNumber, true);
            else
                this.register.LowerByte = global::ColdBoi.Bit.Set(this.register.LowerByte, this.bitNumber, true);
            
#if DEBUG
            Console.WriteLine($"{this.processor.Registers.PC.Value:X4}: {this.Name} {this.bitNumber}, {this.registerName}");
#endif
        }
    }
}